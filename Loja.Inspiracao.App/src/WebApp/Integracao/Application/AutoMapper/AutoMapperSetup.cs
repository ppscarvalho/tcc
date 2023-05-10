namespace WebApp.Integracao.Application.AutoMapper
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()), typeof(object));
        }
    }
}
