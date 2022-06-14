using Evolution.Data.models.Base;

namespace Evolution.Data.models
{
    public class Usuario: BaseEntity
    {
        public string UsuNombre { get; set; }
        public string UsuPass { get; set; }

    }
}
