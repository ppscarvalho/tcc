using Loja.Inspiracao.Produto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Inspiracao.Produto.Infra.Data.Mappings
{
    public class CategoriMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");

            builder.Property(c => c.Descricao).HasMaxLength(250)
                .IsRequired();

            // 1 : N => Categorias : Produtos
            builder.HasMany(c => c.Produto)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}
