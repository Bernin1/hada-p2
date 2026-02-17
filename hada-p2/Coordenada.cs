using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        // Atributos
        private int fila;
        private int columna;

        int Fila
        {
            get { return fila; }
            set
            {
                if (value < 0 || value > 9)
                    throw new ArgumentException("La fila no puede ser negativa.");
                fila = value;
            }
        }
        int Columna
        {
            get { return columna; }
            set
            {
                if (value < 0 || value > 9)
                    throw new ArgumentException("La fila no puede ser negativa.");
                columna = value;
            }
        }
        // Constructores
        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }

        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        public Coordenada(string fila, string columna)
        {
            Fila = int.Parse(fila);
            Columna = int.Parse(columna);
        }

        public Coordenada(Coordenada coordenada)
        {
            Fila = coordenada.Fila;
            Columna = coordenada.Columna;
        }
        // Métodoss
        public override string ToString()
        {
            return $"({Fila}, {Columna})";
        }

        public int getHashCode()
        {
            return Fila.GetHashCode() ^ Columna.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordenada c)
                return Fila == c.Fila && Columna == c.Columna;

            return false;
        }
        public bool Equals(Coordenada c)
        {
            if (c == null) return false;
            return Fila == c.Fila && Columna == c.Columna;
        }
    }
}
