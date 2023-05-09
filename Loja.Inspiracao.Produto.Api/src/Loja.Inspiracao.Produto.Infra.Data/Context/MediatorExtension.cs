#nullable disable

using Loja.Inspiracao.Resources.Communication.Mediator;
using Loja.Inspiracao.Resources.DomainObject;

namespace Loja.Inspiracao.Produto.Infra.Data.Context
{
    public static class MediatorExtension
    {
        public static async Task PublishEvent(this IMediatorHandler mediator, ProdutoDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
