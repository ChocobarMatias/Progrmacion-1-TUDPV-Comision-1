using System;

public class Program
{
    public static void Main()
    {
        Protagonista caballero = new Protagonista(50);
        JefeHusk jefe = new JefeHusk();

        Console.WriteLine("=== ENFRENTAMIENTO ===");
        Console.WriteLine("El Caballero contra el Jefe Husk");
        Console.WriteLine();

        while (caballero.EstaVivo && jefe.EstaVivo)
        {
            Console.WriteLine($"Vida del Caballero: {caballero.Salud}");
            Console.WriteLine($"Vida del Jefe: {jefe.Salud}");
            Console.WriteLine();

            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Concentrar alma");
            Console.WriteLine("3. Usar amuleto");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    caballero.Atacar();
                    break;

                case "2":
                    caballero.ConcentrarAlma();
                    break;

                case "3":
                    caballero.UsarAmuleto();
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    continue;
            }

            if (!jefe.EstaVivo)
            {
                break;
            }

            Console.WriteLine();
            Console.WriteLine("El Jefe Husk contraataca.");
            jefe.Atacar();

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.Clear();

        if (caballero.EstaVivo)
        {
            Console.WriteLine("¡El Caballero derrotó al Jefe Husk!");
        }
        else
        {
            Console.WriteLine("El Caballero fue derrotado.");
        }
    }
}
