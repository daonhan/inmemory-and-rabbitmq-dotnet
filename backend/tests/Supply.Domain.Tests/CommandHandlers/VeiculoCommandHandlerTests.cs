using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using Supply.Domain.CommandHandlers;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Xunit;

namespace Supply.Domain.Tests.CommandHandlers
{
    public class VeiculoCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly VeiculoCommandHandler _veiculoCommandHandler;

        public VeiculoCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _veiculoCommandHandler = _autoMocker.CreateInstance<VeiculoCommandHandler>();
        }

        #region AddVeiculoCommand
        [Fact]
        public async Task Handle_AddVeiculoCommand_ShouldFailValidation_WhenEmptyPlaca()
        {
            // Arrange
            var command = new AddVeiculoCommand("");

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoCommand_ShouldFailValidation_WhenInvalidPlaca()
        {
            // Arrange
            var command = new AddVeiculoCommand("ABCDEFG");

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.InvalidFormat.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoCommand_ShouldFailValidation_WhenPlacaAlreadyInUse()
        {
            // Arrange
            var command = new AddVeiculoCommand("PLA1234");

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetByPlaca(It.Is<string>(placa => placa.Equals(command.Placa))))
                .ReturnsAsync(new Veiculo(command.Placa));

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("ABC1D23")]
        [InlineData("ABC1234")]
        public async Task Handle_AddVeiculoCommand_ShouldAddAndCommit_WhenValid(string placa)
        {
            // Arrange
            var command = new AddVeiculoCommand(placa);

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetByPlaca(It.Is<string>(placa => placa.Equals(command.Placa))))
                .ReturnsAsync((Veiculo)null);

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoRepository>()
                .Verify(x => x.Add(It.IsAny<Veiculo>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion
    }
}
