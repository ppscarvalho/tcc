using AutoMapper;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.ViewModels;
using Loja.Inspiracao.Produto.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;
namespace Loja.Inspiracao.Produto.Application.AutoMapper
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()), typeof(object));
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdicionarProdutoCommand, LojaInspiracao.Produto>().ReverseMap();
            CreateMap<ProdutoViewModel, LojaInspiracao.Produto>().ReverseMap();

            CreateMap<CategoriaViewModel, AdicionarCategoriaCommand>().ReverseMap();
            CreateMap<AdicionarCategoriaCommand, Categoria>().ReverseMap();
            CreateMap<CategoriaViewModel, Categoria>().ReverseMap();
        }
    }
}
