using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LivroMap : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable("Livro");  // Mapeia para a tabela Livros

        builder.HasKey(l => l.CodLivro);  // Chave primária

        builder.Property(l => l.CodLivro)
            .HasColumnName("Codl")
            .IsRequired();

        builder.Property(l => l.Titulo)
            .HasColumnName("Titulo")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(l => l.Editora)
            .HasColumnName("Editora")
            .HasMaxLength(40);

        builder.Property(l => l.Edicao)
            .HasColumnName("Edicao")
            .IsRequired();

        builder.Property(l => l.AnoPublicacao)
            .HasColumnName("AnoPublicacao")
            .HasMaxLength(4);

        // Relacionamento muitos-para-muitos
        builder.HasMany(l => l.LivroAutores)
               .WithOne(la => la.Livro)
               .HasForeignKey(la => la.CodLivro);

        builder.HasMany(l => l.LivroAssuntos)
               .WithOne(la => la.Livro)
               .HasForeignKey(la => la.CodLivro);
    }
}
