using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRopa.Domain.Entities
{
    public class Zapato : Producto
    {
        public string Material { get; set; } = string.Empty;

        public Zapato() { }

        public Zapato(string nombre, decimal precio, string talla, string material, string imagenUrl)
            : base(nombre, precio, talla, imagenUrl)
        {
            Material = material;
        }

        public override string ObtenerEtiqueta() => $"Zapato de {Material} - {Nombre}";
    }
}