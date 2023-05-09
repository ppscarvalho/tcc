using FluentValidation.Results;
using Loja.Inspiracao.Produto.Domain.Entities;
using Loja.Inspiracao.Resources.Communication.Mediator;
using Loja.Inspiracao.Resources.Data;
using Loja.Inspiracao.Resources.DomainObject;
using Loja.Inspiracao.Resources.Messagens;
using Microsoft.EntityFrameworkCore;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;
namespace Loja.Inspiracao.Produto.Infra.Data.Context
{
    public class ProdutoDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<LojaInspiracao.Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutoDbContext).Assembly);

            foreach (var type in modelBuilder.Model.GetEntityTypes().Where(e => typeof(Entity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(type.ClrType)
                    .HasKey("Id");

                modelBuilder
                    .Entity(type.ClrType)
                    .Property<DateTime>("DataCadastro")
                    .IsRequired();

                modelBuilder
                    .Entity(type.ClrType)
                    .Property<DateTime?>("DataAlteracao");
            }
        }

        private void SetDefaultValues()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                }
            }
        }

        public async Task<bool> Commit()
        {
            SetDefaultValues();
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublishEvent(this);

            return sucesso;
        }
    }
}
