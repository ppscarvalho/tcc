using AutoMapper;
using Loja.Inspiracao.MQ.Models.Categoria;
using WebApp.Models;

namespace WebApp.Integracao.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Categoria
            CreateMap<CategoriaViewModel, ResponseCategoriaOut>().ReverseMap();
        }
    }
}
