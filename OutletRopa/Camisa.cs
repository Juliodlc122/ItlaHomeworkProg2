using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRopa.Domain.Entities
{
    public class Camisa : PrendaBase
    {
        public bool EsMangaLarga { get; set; }

        public Camisa()
        {
        }

        public Camisa(string nombre, decimal precio, string talla, bool esMangaLarga, string imagenUrl)
             : base(nombre, precio, talla, imagenUrl)
        {
            
        }

        public override string ObtenerEtiqueta() => $"Camisa: {Nombre} - Talla {Talla}";
    }
}