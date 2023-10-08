using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contabilizacao.Data.Mappings;

public class ProductMapping: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("produto");
        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code).HasColumnName("CB");
        builder.Property(p => p.Name).HasColumnName("Nome");
        builder.Property(p => p.Price).HasColumnName("Valor");
        builder.Property(p => p.Measurement).HasColumnName("Medida");
        builder.Property(p => p.Unit).HasColumnName("Unidade");
        builder.Property(p => p.Brand).HasColumnName("Marca");
        builder.Property(p => p.Amount).HasColumnName("Quantidade");
    }
}