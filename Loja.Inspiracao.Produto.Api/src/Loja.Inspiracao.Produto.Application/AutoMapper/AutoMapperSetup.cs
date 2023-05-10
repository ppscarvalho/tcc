using AutoMapper;
using Loja.Inspiracao.MQ.Models.Categoria;
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
            //Adicionar Categoria
            CreateMap<CategoriaViewModel, AdicionarCategoriaCommand>().ReverseMap();
            CreateMap<AdicionarCategoriaCommand, Categoria>().ReverseMap();

            //Alterar Categoria
            CreateMap<CategoriaViewModel, AlterarCategoriaCommand>().ReverseMap();
            CreateMap<AlterarCategoriaCommand, Categoria>().ReverseMap();

            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();

            //Response Categoria Out
            CreateMap<CategoriaViewModel, ResponseCategoriaOut>().ReverseMap();
            CreateMap<ResponseCategoriaOut, AdicionarCategoriaCommand>().ReverseMap();
            CreateMap<ResponseCategoriaOut, AlterarCategoriaCommand>().ReverseMap();

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
