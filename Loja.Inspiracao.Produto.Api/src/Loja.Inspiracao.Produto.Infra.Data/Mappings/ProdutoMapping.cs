using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;

namespace Loja.Inspiracao.Produto.Infra.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<LojaInspiracao.Produto>
    {
        public void Configure(EntityTypeBuilder<LojaInspiracao.Produto> builder)
        {
            builder.ToTable("Produto");

            builder.Property(c => c.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.Preco)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.Estoque)
                .IsRequired();

            builder.Property(c => c.Imagem)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.Ativo)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
