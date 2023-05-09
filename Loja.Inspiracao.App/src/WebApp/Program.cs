using Polly;
using WebApp.Integracao.Categoria;
using WebApp.Utils.Extensions;
using WebApp.Utils.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoriaApiClient, CategoriaApiClient>();

const int timeWait = 30;

builder.Services.AddScoped<HttpCategoriaDelegatingHandler>();

builder.Services.AddHttpClient<ICategoriaApiClient, CategoriaApiClient>()
      .AddHttpMessageHandler<HttpCategoriaDelegatingHandler>()
      .AddPolicyHandler(PollyExtensions.GetRetryPolicy())
      .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(2, TimeSpan.FromSeconds(timeWait)));

builder.Services.Configure<APIsOptions>(builder.Configuration.GetSection(nameof(APIsOptions)));

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
