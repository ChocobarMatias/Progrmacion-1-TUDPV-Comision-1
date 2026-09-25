namespace TrabajoPractico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] armas = { "Espada", "Hacha", "Arco", "Daga" };
            int[] durabilidad = new int[4];

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Ingrese la durabilidad de " + armas[i] + ":");
                durabilidad[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Carga completa. la durabilidad de las armas es:");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(armas[i] + ": " + durabilidad[i]);
            }

            Console.WriteLine("¿Qué deseas hacer?");
            Console.WriteLine("1: Inspeccionar armas críticas");
            Console.WriteLine("2: Ver arsenal completo");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Armas en estado crítico (durabilidad <= 20):");
                    for (int i = 0; i < 4; i++)
                    {
                        if (durabilidad[i] <= 20)
                        {
                            Console.WriteLine(armas[i] + " -> ¡ALERTA! Durabilidad crítica: " + durabilidad[i]);
                        }
                    }
                    break;

                case 2:
                    Console.WriteLine("Arsenal completo:");
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine(armas[i] + ": " + durabilidad[i]);
                    }
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        }
    }
}
