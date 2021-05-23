using MongoDB.Driver;
using Supply.Caching.Entities;

namespace Supply.Infra.Data.Context
{
    public class SupplyCacheContext
    {
        private readonly IMongoDatabase Database;

        public SupplyCacheContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            Database = client.GetDatabase("Supply");
        }

        public IMongoCollection<VeiculoCache> VeiculosCache
        {
            get
            {
                return Database.GetCollection<VeiculoCache>("Veiculos");
            }
        }
    }
}
