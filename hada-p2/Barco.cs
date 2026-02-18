using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Text;

namespace Hada
{
    public class Barco
    {
        //Diccionario que almacena las coordenadas del barco y su estado (nombre o nombre_T)
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; set; }
        public int NumDanyos { get; set; }

        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        //Constructor que recibe el nombre del barco, su longitud, orientación y coordenada
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            this.Nombre = nombre;
            this.NumDanyos = 0;
            this.CoordenadasBarco = new Dictionary<Coordenada, string>();

            //Generar las coordenadas del barco según orientación y longitud
            for (int i = 0; i < longitud; i++)
            {
                int nuevaFila = coordenadaInicio.Fila;
                int nuevaColumna = coordenadaInicio.Columna;

                if (orientacion == 'h')
                {
                    nuevaColumna += i;
                }
                else if (orientacion == 'v')
                {
                    nuevaFila += i;
                }

                Coordenada nuevaCoord = new Coordenada(nuevaFila, nuevaColumna);
                this.CoordenadasBarco.Add(nuevaCoord, nombre);
            }
        }

        //Método que recibe una coord de disparo, verifica impacto en barco y actlz estado
        public void Disparar(Coordenada c)
        {
            if (this.CoordenadasBarco.ContainsKey(c))
            {
                string etiquetaActual = this.CoordenadasBarco[c];

                if (!etiquetaActual.EndsWith("_T"))
                {
                    this.CoordenadasBarco[c] = this.Nombre + "_T";
                    this.NumDanyos++;

                    eventoTocado?.Invoke(this, new TocadoArgs(this.Nombre, c));

                    if (Hundido())
                    {
                        eventoHundido?.Invoke(this, new HundidoArgs(this.Nombre));
                    }
                }
            }
        }


        //Método que verifica si el barco está hundido
        public bool Hundido()
        {
            foreach (var etiqueta in this.CoordenadasBarco.Values)
            {
                if (etiqueta == this.Nombre)
                {
                    return false;
                }
            }
            return true;
        }

        //Método que devuelve un string con la info del barco
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{this.Nombre}]");
            sb.AppendLine($"DAÑOS: [{this.NumDanyos}]");
            sb.AppendLine($"HUNDIDO: [{Hundido()}]");

            sb.Append("COORDENADAS: ");
            foreach (var item in this.CoordenadasBarco)
            {
                sb.Append($"[{item.Key.ToString()} :{item.Value}] ");
            }

            return sb.ToString();
        }
    }
}
