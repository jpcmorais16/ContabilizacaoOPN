using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contabilizacao.Data.Mappings;

public class ShiftMapping: IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("Turno");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Codigo");
        builder.Property(p => p.Start).HasColumnName("Inicio");
        builder.Property(p => p.End).HasColumnName("Fim");
    }
}