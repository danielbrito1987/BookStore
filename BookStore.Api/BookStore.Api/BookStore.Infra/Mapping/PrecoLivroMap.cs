using BookStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infra.Mapping
{
    public class PrecoLivroMap : IEntityTypeConfiguration<PrecoLivro>
    {
        public void Configure(EntityTypeBuilder<PrecoLivro> builder)
        {
            builder.ToTable("Preco_Livro");

            // Definindo a chave primária composta
            builder.HasKey(la => la.CodPreco);

            builder.Property(la => la.CodLivro)
                .HasColumnName("Livro_Codl")
                .IsRequired();

            builder.Property(p => p.CodPreco)
                .HasColumnName("CodPreco")
                .IsRequired();

            builder.Property(p => p.TipoCompra)
                .HasColumnName("TipoCompra")
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnName("Valor")
                .IsRequired();

            builder.HasOne(la => la.Livro)
            .WithMany(l => l.Precos)
            .HasForeignKey(la => la.CodLivro);
        }
    }
}
