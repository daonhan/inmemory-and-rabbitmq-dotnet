using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supply.Domain.Entities;

namespace Supply.Infra.Data.Mappings
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Placa)
                .HasColumnName("Placa")
                .IsRequired();
        }
    }
}
