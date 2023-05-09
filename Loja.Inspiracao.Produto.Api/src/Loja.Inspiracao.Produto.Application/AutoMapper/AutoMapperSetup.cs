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
            //Categoria
            CreateMap<CategoriaViewModel, AdicionarCategoriaCommand>().ReverseMap();
            CreateMap<AdicionarCategoriaCommand, Categoria>().ReverseMap();
            CreateMap<Categoria, CategoriaViewModel>().ForMember(
                dest => dest.ProdutosViewModel,
                opt => opt.MapFrom(prop => prop.Produto)).ReverseMap();


            //Produto
            CreateMap<ProdutoViewModel, AdicionarProdutoCommand>().ReverseMap();
            CreateMap<AdicionarProdutoCommand, LojaInspiracao.Produto>().ReverseMap();

            CreateMap<LojaInspiracao.Produto, ProdutoViewModel>().ForMember(
                dest => dest.CategoriaViewModel,
                opt => opt.MapFrom(b => b.Categoria)
                ).ReverseMap();
        }
    }
}
