using Supply.Domain.Core.Messaging.Domain;

namespace Supply.Domain.Entities
{
    public class Veiculo : Entity
    {
        public string Placa { get; private set; }

        public Veiculo(string placa)
        {
            Placa = placa;
        }
    }
}
