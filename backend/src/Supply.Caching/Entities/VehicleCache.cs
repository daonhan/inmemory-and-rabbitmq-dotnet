using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Supply.Caching.Entities
{
    public class VehicleCache
    {
        [BsonId]
        public string Id { get; set; }

        public string Plate { get; set; }

        public VehicleCache(Guid id, string plate)
        {
            Id = id.ToString();
            Plate = plate;
        }
    }
}
