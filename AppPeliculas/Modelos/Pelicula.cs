using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPeliculas.Modelos
{
    public class Pokemon
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string genero { get; set; }
        public string tipo { get; set; }
        public string portada_url { get; set; }
        public string debilidad { get; set; }

        public override string ToString()
        {
            return nombre + " - " + genero;
        }
    }
}
