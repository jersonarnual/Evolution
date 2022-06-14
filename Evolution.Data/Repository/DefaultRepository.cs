using Evolution.Data.Context;
using Evolution.Data.Interface;
using Evolution.Data.models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Data.Repository
{
    public class DefaultRepository<T> : IDefaultRepository<T> where T : BaseEntity
    {
        #region Members
        private readonly EvolutionContext _context;
        private readonly DbSet<T> table;
        #endregion

        #region Ctor
        public DefaultRepository(EvolutionContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        #endregion

        #region Methods
        public IEnumerable<T> GetAll()
        {
            return table.AsQueryable();
        }

        public T GetById(int id)
        {
            return table.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public bool Insert(T entity)
        {
            try
            {
                entity.CreateTime = DateTime.Now;
                table.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                entity.UpdateTime = DateTime.Now;
                table.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                table.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
