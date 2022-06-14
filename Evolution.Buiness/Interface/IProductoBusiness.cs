using Evolution.Infraestructure.DTO;
using Evolution.Infraestructure.Util;
using System;

namespace Evolution.Buiness.Interface
{
    public interface IProductoBusiness
    {
        Result GetAll();
        Result GetById(int  id);
        Result Insert(ProductoDTO entity);
        Result Update(ProductoDTO entity);
        Result Delete(int id);
    }
}
