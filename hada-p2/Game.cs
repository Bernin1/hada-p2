using System;
using System.Collections.Generic;

namespace Hada
{
    public class Game
    {
        private bool finPartida;

        public Game()
        {
            GameLoop();
        }

        private void GameLoop()
        {
            var barcos = new List<Barco>()
            {
                new Barco("THOR", 1, 'h', new Coordenada(0,0)),
                new Barco("LOKI", 2, 'v', new Coordenada(1,2)),
                new Barco("MAYA", 3, 'h', new Coordenada(3,1))
            };

            Tablero tablero = new Tablero(9);
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (!finPartida)
            {
                Console.WriteLine(tablero);

                Console.WriteLine("Introduce coordenada FILA,COLUMNA ('s' para salir):");
                string input = Console.ReadLine();

                if (input.ToLower() == "s")
                {
                    finPartida = true;
                    break;
                }

                if (!input.Contains(","))
                {
                    Console.WriteLine("Formato incorrecto.");
                    continue;
                }

                string[] partes = input.Split(',');
                if (partes.Length != 2)
                {
                    Console.WriteLine("Formato incorrecto.");
                    continue;
                }

                try
                {
                    int fila = int.Parse(partes[0]);
                    int columna = int.Parse(partes[1]);

                    tablero.Disparar(new Coordenada(fila, columna));
                }
                catch
                {
                    Console.WriteLine("Entrada inválida.");
                }
            }
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
