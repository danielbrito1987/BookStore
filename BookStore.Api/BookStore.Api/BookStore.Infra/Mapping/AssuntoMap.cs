using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AssuntoMap : IEntityTypeConfiguration<Assunto>
{
    public void Configure(EntityTypeBuilder<Assunto> builder)
    {
        builder.ToTable("Assunto");

        // Definindo a chave primária
        builder.HasKey(a => a.CodAssunto);

        builder.Property(a => a.CodAssunto)
            .HasColumnName("CodAs")
            .IsRequired();

        // Mapeando propriedades
        builder.Property(a => a.Descricao)
            .HasColumnType("varchar(20)")
            .IsRequired();
    }
}
