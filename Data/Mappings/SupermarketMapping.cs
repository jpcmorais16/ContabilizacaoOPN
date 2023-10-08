using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contabilizacao.Data.Mappings;

public class SupermarketMapping: IEntityTypeConfiguration<Supermarket>
{
    public void Configure(EntityTypeBuilder<Supermarket> builder)
    {
        builder.ToTable("supermercado");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Codigo");
        builder.Property(p => p.Name).HasColumnName("Nome");
    }
}