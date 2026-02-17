using Hada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocadoArgs
    {
        public string Nombre { get; }
        public Coordenada CoordenadaImpacto { get; }

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto)
        {
            Nombre = nombre;
            CoordenadaImpacto = coordenadaImpacto;
        }
    }

    public class HundidoArgs
    {
        public string Nombre { get; }

        public HundidoArgs(string nombre)
        {
            Nombre = nombre;
        }
    }
}
