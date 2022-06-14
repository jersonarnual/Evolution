using Evolution.Infraestructure.DTO;
using Evolution.Infraestructure.Util;
using System;

namespace Evolution.Buiness.Interface
{
    public interface IUsuarioBusiness
    {
        Result GetAll();
        Result GetById(int id);
        Result Insert(UsuarioDTO entity);
        Result Update(UsuarioDTO entity);
        Result Delete(int id);
    }
}
