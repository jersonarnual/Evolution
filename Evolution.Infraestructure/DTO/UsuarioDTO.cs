using System;

namespace Evolution.Infraestructure.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UsuNombre { get; set; }
        public string UsuPass { get; set; }
    }
}
