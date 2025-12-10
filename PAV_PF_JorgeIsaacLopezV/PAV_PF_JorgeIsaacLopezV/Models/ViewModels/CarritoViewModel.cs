using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models.ViewModels
{
    public class CarritoItemVM
    {
        public int IdCancion { get; set; }
        public string NombreCancion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public decimal Subtotal
        {
            get { return Precio * Cantidad; }
        }
    }
    public class CarritoViewModel
    {
        public List<CarritoItemVM> Items { get; set; } = new List<CarritoItemVM>();

        // Subtotal del carrito
        public decimal Subtotal
        {
            get { return Items.Sum(i => i.Subtotal); }
        }

        // IVA 
        public decimal Iva
        {
            get { return Math.Round(Subtotal * 0.13m, 2); }
        }

        // Total = Subtotal + IVA
        public decimal Total
        {
            get { return Subtotal + Iva; }
        }
    }

    
}