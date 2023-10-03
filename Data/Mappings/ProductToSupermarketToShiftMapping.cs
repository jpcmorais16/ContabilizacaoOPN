using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contabilizacao.Data.Mappings;

public class ProductToSupermarketToShiftMapping: IEntityTypeConfiguration<ProductToSupermarketToShift>
{
    public void Configure(EntityTypeBuilder<ProductToSupermarketToShift> builder)
    {
        builder.ToTable("ProdutoToSupermercadoToTurno");
        builder.HasKey(p => new { p.ProductCode, p.SupermarketId, p.ShiftId });

        builder.Property(p => p.ProductCode).HasColumnName("Produto_cb");
        builder.Property(p => p.SupermarketId).HasColumnName("Supermercado_id");
        builder.Property(p => p.ShiftId).HasColumnName("Turno_id");
        builder.Property(p => p.Amount).HasColumnName("Quantidade");

        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductCode);
    }
}