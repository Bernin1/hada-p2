using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hada
{
    public class Tablero
    {
        // PROPIEDADES
        public int TamTablero { get; private set; }
        
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;

        // Constructor que recibe el tamaño y la lista de barcos ya creados
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            if (tamTablero < 4 || tamTablero > 9)
            {
                throw new ArgumentException("El tamaño del tablero debe ser entre 4 y 9");
            }

            this.TamTablero = tamTablero;
            this.barcos = barcos;
            this.coordenadasDisparadas = new List<Coordenada>();
            this.coordenadasTocadas = new List<Coordenada>();
            this.barcosEliminados = new List<Barco>();
            this.casillasTablero = new Dictionary<Coordenada, string>();

            // Para cada barco, inicializar los eventos de tocado y hundido
            foreach (var barco in this.barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

            // Inicializar las casillas del tablero
            inicializaCasillasTablero();
        }

        // Método privado para inicializar el tablero con AGUA o nombres de los barcos
        private void inicializaCasillasTablero()
        {
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada c = new Coordenada(i, j);
                    casillasTablero.Add(c, "AGUA");
                }
            }

            foreach (var barco in barcos)
            {
                foreach (var coord in barco.CoordenadasBarco)
                {
                    if (casillasTablero.ContainsKey(coord.Key))
                    {
                        casillasTablero[coord.Key] = coord.Value;
                    }
                }
            }
        }

        // Método para disparar a una parte del tablero
        public void Disparar(Coordenada c)
        {
            // Mirar que el disparo se haga dentro del tablero
            if (c.Fila < 0 || c.Fila >= TamTablero || c.Columna < 0 || c.Columna >= TamTablero)
            {
                Console.WriteLine($"La coordenada {c.ToString()} está fuera de las dimensiones del tablero.");
                return;
            }

            // Añadir al historial de disparos
            coordenadasDisparadas.Add(c);

            // Comprobar si le damos a algún barco
            foreach (var barco in barcos)
            {
                barco.Disparo(c);
            }
        }

        // Método para dibujar el estado visual del tablero
        public string DibujarTablero()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada c = new Coordenada(i, j);
                    if (casillasTablero.ContainsKey(c))
                    {
                        sb.Append($"[{casillasTablero[c]}]");
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        // Método que devuelve un string con la info del tablero resultante
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // La información de cada uno de los Barcos
            foreach (var barco in barcos)
            {
                sb.AppendLine(barco.ToString());
            }
            sb.AppendLine();

            // La lista de 'Coordenadas Disparadas' y 'Tocadas'
            sb.Append("Coordenadas disparadas: ");
            sb.AppendLine(string.Join(" ", coordenadasDisparadas.Select(c => c.ToString())));

            sb.Append("Coordenadas tocadas: ");
            sb.AppendLine(string.Join(" ", coordenadasTocadas.Select(c => c.ToString())));
            sb.AppendLine();

            // El tablero
            sb.AppendLine("CASILLAS TABLERO");
            sb.AppendLine("-------");
            sb.Append(DibujarTablero());

            return sb.ToString();
        }

        // Manejador privado del evento cuando un barco es tocado
        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            // Actualizar la casilla en el tablero
            if (casillasTablero.ContainsKey(e.coordenadaImpacto))
            {
                casillasTablero[e.coordenadaImpacto] = e.nombre + "_T";
            }

            // Añadir a coordenadas tocadas verificando que no haya repetidos
            if (!coordenadasTocadas.Contains(e.coordenadaImpacto))
            {
                coordenadasTocadas.Add(e.coordenadaImpacto);
            }

            Console.WriteLine($"TABLERO: Barco {e.nombre} tocado en Coordenada: [{e.coordenadaImpacto.ToString()}]");
        }

        // Manejador privado del evento cuando un barco es hundido
        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine($"TABLERO: Barco {e.nombre} hundido!!");

            Barco b = sender as Barco;
            if (b != null && !barcosEliminados.Contains(b))
            {
                barcosEliminados.Add(b);
            }

            // Comprobar si todos los barcos están hundidos
            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
