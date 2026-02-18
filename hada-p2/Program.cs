using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Instanciamos el juego
            Juego juego = new Juego();

            // 2. Arrancamos el bucle principal
            juego.Jugar();

            // 3. Pausa final para ver el resultado antes de cerrar
            Console.WriteLine("\nPulsa cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
