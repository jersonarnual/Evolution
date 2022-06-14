using Evolution.Infraestructure.DTO;
using Evolution.Infraestructure.Util;

namespace Evolution.Buiness.Interface
{
    public interface IPedidoBusiness
    {
        Result GetAll();
        Result GetById(int id);
        Result Insert(PedidoDTO entity);
        Result Update(PedidoDTO entity);
        Result Delete(int id);
    }
}
