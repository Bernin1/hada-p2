using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        // PROPIEDADES
        public int Tamanyo { get; private set; }
        private List<Barco> barcos;
        private List<Coordenada> disparos; // Historial de disparos realizados

        // Constructor que recibe el tamaño
        public Tablero(int tamanyo)
        {
            if (tamanyo < 5) throw new ArgumentException("El tablero debe ser al menos de 5x5");

            this.Tamanyo = tamanyo;
            this.barcos = new List<Barco>();
            this.disparos = new List<Coordenada>();
        }

        // Método para añadir un barco con sus respectivas dimensiones sin que se junte con otro
        public void AnyadirBarco(Barco nuevoBarco)
        {
            // 1 Validar que el barco cabe dentro del tablero
            foreach (var coord in nuevoBarco.CoordenadasBarco.Keys)
            {
                if (coord.Fila < 0 || coord.Fila >= Tamanyo ||
                    coord.Columna < 0 || coord.Columna >= Tamanyo)
                {
                    throw new ArgumentOutOfRangeException("El barco se sale de los límites del tablero.");
                }
            }

            // 2 Validar que no choca con otro barco ya existente
            foreach (var barcoExistente in barcos)
            {
                var colision = barcoExistente.CoordenadasBarco.Keys.Intersect(nuevoBarco.CoordenadasBarco.Keys);

                if (colision.Any())
                {
                    throw new Exception($"Colisión detectada con el barco {barcoExistente.Nombre} en {colision.First()}");
                }
            }

            // 3 Si pasa las validaciones, lo añadimos
            this.barcos.Add(nuevoBarco);
        }

        // Método para disparar a una parte del tablero
        public void Disparar(Coordenada c)
        {
            // Mirar que el disparo se haga dentro del tablero
            if (c.Fila < 0 || c.Fila >= Tamanyo || c.Columna < 0 || c.Columna >= Tamanyo)
            {
                Console.WriteLine("Disparo fuera de rango.");
                return;
            }

            // Validar si ya se disparó ahí
            if (disparos.Contains(c))
            {
                Console.WriteLine("YA DISPARADO: Ya habías disparado en esta coordenada.");
                return;
            }

            // Añadir al historial de disparos
            disparos.Add(c);

            // Comprobar si le damos a algún barco
            foreach (var barco in barcos)
            {
                if (barco.CoordenadasBarco.ContainsKey(c))
                {
                    barco.Disparar(c);
                    return; 
                }
            }

            Console.WriteLine("AGUA: No has tocado ningún barco.");
        }

        // Método que devuelve un string con la info del tablero resultante
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Cabecera de columnas (0 1 2 ...)
            sb.Append("   ");
            for (int k = 0; k < Tamanyo; k++) sb.Append($"{k} ");
            sb.AppendLine();

            for (int i = 0; i < Tamanyo; i++)
            {
                // Número de fila
                sb.Append($"{i} |");

                for (int j = 0; j < Tamanyo; j++)
                {
                    Coordenada actual = new Coordenada(i, j);
                    string representacion = "~ "; 

                    // Miramos si hay disparo
                    if (disparos.Contains(actual))
                    {
                        // Si hay disparo, comprobamos si era barco o agua
                        bool esTocado = false;
                        foreach (var b in barcos)
                        {
                            if (b.CoordenadasBarco.ContainsKey(actual))
                            {
                                representacion = "X "; // Tocado
                                esTocado = true;
                                break;
                            }
                        }
                        if (!esTocado) representacion = "o "; // Agua disparada
                    }
                    
                    sb.Append(representacion);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        // Método para saber si quedan barcos vivos
        public bool QuedanBarcos()
        {
            return barcos.Any(b => b.NumDanyos < b.CoordenadasBarco.Count);
        }
    }
}
