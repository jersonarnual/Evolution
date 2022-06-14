using Evolution.Data.models.Base;
using System.Collections.Generic;

namespace Evolution.Data.Interface
{
    public interface IDefaultRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
