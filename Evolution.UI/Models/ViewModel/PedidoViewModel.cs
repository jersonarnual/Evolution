using Evolution.Infraestructure.DTO;
using System;
using System.Collections.Generic;

namespace Evolution.UI.Models.ViewModel
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int PedUsu { get; set; }
        public int PedProd { get; set; }
        public decimal PedVrUnit { get; set; }
        public float PedCant { get; set; }
        public decimal PedSubTot { get; set; }
        public float PedIVA { get; set; }
        public decimal PedTotal { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public ProductoDTO Producto { get; set; }
        public List<PedidoDTO> ListPedido { get; set; }
    }
}
