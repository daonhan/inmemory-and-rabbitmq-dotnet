using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Infra.Data.Mappings;

namespace Supply.Infra.Data.Context
{
    public class SupplyDataContext : DbContext, IUnitOfWork
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public SupplyDataContext(DbContextOptions<SupplyDataContext> options) : base(options) { }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
