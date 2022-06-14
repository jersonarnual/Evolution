using Evolution.Data.models;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Data.Context
{
    public class EvolutionContext : DbContext
    {
        #region Ctor
        public EvolutionContext(DbContextOptions<EvolutionContext> options) : base(options)
        {

        }
        public EvolutionContext()
        {

        }
        #endregion

        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
