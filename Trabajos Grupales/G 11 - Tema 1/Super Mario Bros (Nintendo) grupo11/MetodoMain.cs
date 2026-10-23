// See https://aka.ms/new-console-template for more information

using System;

namespace Grupo_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine(" //////DEMOSTRACIÓN TEMA 1: SUPER MARIO BROS////////  ");
            Console.WriteLine("==================================================\n");

            // Instanciación de personajes
            Mario mario = new Mario("Mario", 3, 0, "Super");
            Luigi luigi = new Luigi("Luigi", 3, 0);

            // Arreglo polimórfico de PersonajeMario
            PersonajeMario[] hermanos = new PersonajeMario[] { mario, luigi };

            // Creación de bloque de interrogación
            BloqueInterrogacion bloque = new BloqueInterrogacion();

            // Demostración de salto polimórfico mediante bucle
            Console.WriteLine("--- DEMOSTRACIÓN DE SALTO POLIMÓRFICO ---");
            foreach (PersonajeMario pj in hermanos)
            {
                pj.Saltar();
            }

            Console.WriteLine("\n--- INTERACCIÓN CON BLOQUE DE INTERROGACIÓN ---");
            // Mario golpea el bloque la primera vez
            bloque.Golpear(mario);

            // Luigi intenta golpear el mismo bloque ya abierto
            bloque.Golpear(luigi);

            Console.WriteLine("\n--- DEMOSTRACIÓN DE DAÑO Y VALIDACIÓN DE VIDAS ---");
            mario.RecibirDanio();
            mario.RecibirDanio();
            mario.RecibirDanio(); // Llega a 0 vidas y dispara GameOver mediante la propiedad Vidas

            Console.WriteLine("\nPresione cualquier tecla para finalizar...");
            Console.ReadKey();
        }
    }
}