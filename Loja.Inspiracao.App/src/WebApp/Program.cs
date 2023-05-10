#nullable disable

using Loja.Inspiracao.MQ.Configuration;
using Loja.Inspiracao.MQ.Extensions;
using Loja.Inspiracao.MQ.Models;
using Loja.Inspiracao.Util.Extensions;
using Loja.Inspiracao.Util.Options;
using Polly;
using WebApp.Integracao.Application.AutoMapper;
using WebApp.Integracao.Application.Http.Categoria;
using WebApp.Integracao.Application.Interfaces;
using WebApp.Integracao.Application.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoriaApiClient, CategoriaApiClient>();

const int timeWait = 30;

builder.Services.AddScoped<HttpCategoriaDelegatingHandler>();
builder.Services.AddScoped<ICategoriaAppService, CategoriaAppService>();

builder.Services.AddHttpClient<ICategoriaApiClient, CategoriaApiClient>()
      .AddHttpMessageHandler<HttpCategoriaDelegatingHandler>()
      .AddPolicyHandler(PollyExtensions.GetRetryPolicy())
      .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(2, TimeSpan.FromSeconds(timeWait)));

builder.Services.Configure<APIsOptions>(builder.Configuration.GetSection(nameof(APIsOptions)));

var bdMQ = new BuilderBus(builder.Configuration["RabbitMq:ConnectionString"])
{
    Publishers = new HashSet<IPublisher> {
        new Publisher<RequestIn>(
                queue: builder.Configuration["RabbitMq:ConsumerCategoria"]
            )},
    Retry = new Retry(retryCount: 1, interval: TimeSpan.FromSeconds(10))
};

builder.Services.AddEventBus(bdMQ);

// AutoMapping
builder.Services.AddAutoMapperSetup();

builder.Services.AddHealthChecks()
            .AddRabbitMQ(
               builder.Configuration["RabbitMq:ConnectionString"],
               name: "RabbitMQ");


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
