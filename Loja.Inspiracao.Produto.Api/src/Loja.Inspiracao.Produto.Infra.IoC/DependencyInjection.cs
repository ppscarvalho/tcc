using Loja.Inspiracao.Core.Communication.Mediator;
using Loja.Inspiracao.Core.Data;
using Loja.Inspiracao.Core.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Core.Util;
using Loja.Inspiracao.Produto.Application.AutoMapper;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.Handler;
using Loja.Inspiracao.Produto.Application.Queries.Categoria;
using Loja.Inspiracao.Produto.Domain.Interfaces;
using Loja.Inspiracao.Produto.Infra.Data.Context;
using Loja.Inspiracao.Produto.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Loja.Inspiracao.Produto.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // AutoMapping
            services.AddAutoMapperSetup();

            // Command
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, DefaultResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarCategoriaCommand, DefaultResult>, CategoriaCommandHandler>();

            //Queries
            services.AddScoped<ICategoriaQueries, CategoriaQueries>();

            // Repository
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            // Context
            services.AddScoped<ProdutoDbContext>();
            services.AddScoped<IUnitOfWork, ProdutoDbContext>();

            //RabbitMQ
            //services.AddRabbitMq();
        }

        //public static void AddRabbitMq(this IServiceCollection services)
        //{
        //    services.AddTransient<IMQConnectionFactory, MQConnectionFactory>();
        //    services.AddTransient<IMQPublisher, MQPublisher>();
        //}
    }
}