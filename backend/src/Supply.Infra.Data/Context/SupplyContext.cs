using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Infra.Data.Mappings;

namespace Supply.Infra.Data.Context
{
    public class SupplyContext : DbContext, IUnitOfWork
    {
        public DbSet<Veiculo> Veiculos { get; set; }

        public SupplyContext(DbContextOptions<SupplyContext> options) : base(options) { }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VeiculoMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
