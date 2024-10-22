using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LivroAssuntoMap : IEntityTypeConfiguration<LivroAssunto>
{
    public void Configure(EntityTypeBuilder<LivroAssunto> builder)
    {
        builder.ToTable("Livro_Assunto");

        // Definindo a chave primária composta
        builder.HasKey(la => new { la.CodLivro, la.CodAssunto });

        builder.Property(la => la.CodLivro)
            .HasColumnName("Livro_Codl")
            .IsRequired();

        builder.Property(la => la.CodAssunto)
            .HasColumnName("Assunto_CodAs")
            .IsRequired();

        // Configurando relacionamento muitos para muitos
        builder.HasOne(la => la.Livro)
            .WithMany(l => l.LivroAssuntos)
            .HasForeignKey(la => la.CodLivro);

        builder.HasOne(la => la.Assunto)
            .WithMany(a => a.LivroAssuntos)
            .HasForeignKey(la => la.CodAssunto);
    }
}
