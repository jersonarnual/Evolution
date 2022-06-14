using Evolution.Infraestructure.DTO;
using System;
using System.Collections.Generic;

namespace Evolution.UI.Models.ViewModel
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UsuNombre { get; set; }
        public string UsuPass { get; set; }
        public List<UsuarioDTO> ListUsuario { get; set; }
    }
}
