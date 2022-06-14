using Evolution.Data.models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evolution.Data.models
{
    public class Pedido : BaseEntity
    {
        public decimal PedVrUnit { get; set; }
        public float PedCant { get; set; }
        public decimal PedSubTot { get; set; }
        public float PedIVA { get; set; }
        public decimal PedTotal { get; set; }
        [ForeignKey("Usuario")]
        public int PedUsu { get; set; }
        [ForeignKey("Producto")]
        public int PedProd { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
