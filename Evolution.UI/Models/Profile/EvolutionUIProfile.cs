using AutoMapper;
using Evolution.Infraestructure.DTO;
using Evolution.UI.Models.ViewModel;

namespace Evolution.UI.Models
{
    public class EvolutionUIProfile : Profile
    {
        public EvolutionUIProfile()
        {
            CreateMap<PedidoDTO, PedidoViewModel>();
            CreateMap<PedidoViewModel, PedidoDTO>();
            CreateMap<ProductoDTO, ProductoViewModel>();
            CreateMap<ProductoViewModel, ProductoDTO>();
            CreateMap<UsuarioDTO, UsuarioViewModel>();
            CreateMap<UsuarioViewModel, UsuarioDTO>();
        }

    }
}
