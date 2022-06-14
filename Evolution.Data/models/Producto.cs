using Evolution.Data.models.Base;

namespace Evolution.Data.models
{
    public class Producto : BaseEntity
    {
        public string ProDesc { get; set; }
        public decimal ProValor { get; set; }
    }
}
