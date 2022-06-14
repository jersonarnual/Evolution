using Evolution.Infraestructure.DTO;
using System;
using System.Collections.Generic;

namespace Evolution.UI.Models.ViewModel
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string ProDesc { get; set; }
        public decimal ProValor { get; set; }
        public List<ProductoDTO> ListProducto { get; set; }
    }
}
