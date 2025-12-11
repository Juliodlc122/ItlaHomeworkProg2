using System;
using System.ComponentModel.DataAnnotations;

namespace OutletRopa.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public string Talla { get; set; }
        public string Marca { get; set; }
        public int Stock { get; set; }

       
        public int? TemporadaId { get; set; }

        public string Tipo { get; set; }
        public string Material { get; set; }
        public string TipoProducto { get; set; }

       
        public decimal Precio { get; set; }

        public decimal? PrecioOriginal { get; set; }
        public decimal? DescuentoPorcentaje { get; set; }

       

        public decimal CalcularPrecioFinal()
        {
           
            decimal descuento = DescuentoPorcentaje.GetValueOrDefault(0);

            if (descuento > 0)
            {
                return Precio - (Precio * (descuento / 100m));
            }
            return Precio;
        }

       
        public decimal CalcularPrecioFinal(decimal descuentoExterno)
        {
            if (descuentoExterno > 0)
            {
                return Precio - (Precio * (descuentoExterno / 100m));
            }
            return Precio;
        }

        public string ObtenerEtiqueta()
        {
            return $"{Nombre} - {Marca} ({Talla})";
        }
    }
}