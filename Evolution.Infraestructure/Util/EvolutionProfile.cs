using AutoMapper;
using Evolution.Data.models;
using Evolution.Infraestructure.DTO;

namespace Evolution.Infraestructure.Util
{
    public class EvolutionProfile : Profile
    {
        public EvolutionProfile()
        {
            CreateMap<PedidoDTO, Pedido>();
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<ProductoDTO, Producto>();
            CreateMap<Producto, ProductoDTO>();
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}
