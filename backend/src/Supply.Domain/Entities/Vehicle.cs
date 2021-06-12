using Supply.Domain.Core.Messaging.Domain;

namespace Supply.Domain.Entities
{
    public class Vehicle : Entity
    {
        public string Plate { get; private set; }

        public Vehicle(string plate)
        {
            Plate = plate;
        }
    }
}
