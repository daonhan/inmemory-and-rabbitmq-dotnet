﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using Supply.Domain.CommandHandlers;
using Supply.Domain.Commands.VehicleCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Xunit;

namespace Supply.Domain.Tests.CommandHandlers
{
    public class VehicleCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly VehicleCommandHandler _vehicleCommandHandler;

        public VehicleCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _vehicleCommandHandler = _autoMocker.CreateInstance<VehicleCommandHandler>();
        }

        #region AddVehicleCommand
        [Fact]
        public async Task Handle_AddVehicleCommand_ShouldFailValidation_WhenEmptyPlate()
        {
            // Arrange
            var command = new AddVehicleCommand("");

            // Act
            var validationResult = await _vehicleCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Plate").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVehicleCommand_ShouldFailValidation_WhenInvalidPlate()
        {
            // Arrange
            var command = new AddVehicleCommand("ABCDEFG");

            // Act
            var validationResult = await _vehicleCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.InvalidFormat.Format("Plate").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVehicleCommand_ShouldFailValidation_WhenPlateAlreadyInUse()
        {
            // Arrange
            var command = new AddVehicleCommand("PLA1234");

            _autoMocker.GetMock<IVehicleRepository>()
                .Setup(x => x.GetByPlate(It.Is<string>(plate => plate.Equals(command.Plate))))
                .ReturnsAsync(new Vehicle(command.Plate));

            // Act
            var validationResult = await _vehicleCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Plate").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("ABC1D23")]
        [InlineData("ABC1234")]
        public async Task Handle_AddVehicleCommand_ShouldAddAndCommit_WhenValid(string plate)
        {
            // Arrange
            var command = new AddVehicleCommand(plate);

            _autoMocker.GetMock<IVehicleRepository>()
                .Setup(x => x.GetByPlate(It.Is<string>(plate => plate.Equals(command.Plate))))
                .ReturnsAsync((Vehicle)null);

            _autoMocker.GetMock<IVehicleRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _vehicleCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVehicleRepository>()
                .Verify(x => x.Add(It.IsAny<Vehicle>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion
    }
}