using Supply.Domain.Core.Messaging.Domain;
using System;

namespace Supply.Domain.Entities
{
    public class Vehicle : Entity
    {
        public string Plate { get; private set; }

        public Vehicle(string plate)
        {
            Plate = plate;
        }

        public Vehicle(Guid id, string plate) : base(id)
        {
            Plate = plate;
        }

        public void UpdatePlate(string plate)
        {
            Plate = plate;
        }
    }
}
