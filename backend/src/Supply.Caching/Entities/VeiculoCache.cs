using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Supply.Caching.Entities
{
    public class VeiculoCache
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        public string Placa { get; set; }

        public VeiculoCache(Guid id, string placa)
        {
            Id = id;
            Placa = placa;
        }
    }
}
