using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AutorMap : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autor");

        // Definindo a chave primária
        builder.HasKey(a => a.CodAutor);

        builder.Property(a => a.CodAutor)
            .HasColumnName("CodAu")
            .IsRequired();

        // Mapeando propriedades
        builder.Property(a => a.Nome)
            .HasColumnType("varchar(40)")
            .IsRequired();
    }
}