using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contabilizacao.Data.Mappings;

public class UserMapping: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("usuario");
        builder.HasKey(p => p.Idn);

        builder.Property(p => p.Idn).HasColumnName("Idn");
        builder.Property(p => p.Name).HasColumnName("Nome");
    }
}