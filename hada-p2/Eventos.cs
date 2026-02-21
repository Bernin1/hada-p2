using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    //Evento cuando un barco es tocado
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

    //Evento cuando un barco es hundido
    public class HundidoArgs
    {
        public string Nombre { get; }

        public HundidoArgs(string nombre)
        {
            Nombre = nombre;
        }
    }
}
