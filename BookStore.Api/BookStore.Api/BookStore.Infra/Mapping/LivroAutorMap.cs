using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LivroAutorMap : IEntityTypeConfiguration<LivroAutor>
{
    public void Configure(EntityTypeBuilder<LivroAutor> builder)
    {
        // Configura o nome da tabela
        builder.ToTable("LivroAutor");

        // Configura a chave primária
        builder.HasKey(la => new { la.CodLivro, la.CodAutor });

        // Configura as relações
        builder.HasOne(la => la.Livro)
            .WithMany(l => l.LivroAutores)
            .HasForeignKey(la => la.CodLivro);

        builder.HasOne(la => la.Autor)
            .WithMany(a => a.LivroAutores)
            .HasForeignKey(la => la.CodAutor);
    }
}
