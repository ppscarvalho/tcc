#nullable disable

using Loja.Inspiracao.Produto.Application.AutoMapper;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.Handler;
using Loja.Inspiracao.Produto.Application.Queries.Categoria;
using Loja.Inspiracao.Produto.Application.Queries.Produto;
using Loja.Inspiracao.Produto.Domain.Interfaces;
using Loja.Inspiracao.Produto.Infra.Data.Context;
using Loja.Inspiracao.Produto.Infra.Data.Repository;
using Loja.Inspiracao.Resources.Communication.Mediator;
using Loja.Inspiracao.Resources.Data;
using Loja.Inspiracao.Resources.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Resources.Util;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            services.AddScoped<IRequestHandler<AdicionarCategoriaCommand, DefaultResult>, CategoriaCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, DefaultResult>, ProdutoCommandHandler>();

            //Queries
            services.AddScoped<ICategoriaQueries, CategoriaQueries>();
            services.AddScoped<IProdutoQueries, ProdutoQueries>();

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

    public static class ProdutoContextInitialize
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProdutoDbContext>(options =>
                options.UseMySQL(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)
                ),
                ServiceLifetime.Scoped);

            return services;
        }
    }
}