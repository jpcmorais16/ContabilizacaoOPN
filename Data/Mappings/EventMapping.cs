using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contabilizacao.Data.Mappings;

public class EventMapping: IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("evento");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Codigo");
        builder.Property(p => p.UserIdn).HasColumnName("Usuario_idn");
        builder.Property(p => p.Type).HasColumnName("Tipo");
        builder.Property(p => p.Amount).HasColumnName("Quantidade");
        builder.Property(p => p.Time).HasColumnName("Horario");
    }
}