namespace Trabajo_Practico_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("==========Ejercicios==========");
                Console.WriteLine();
                Console.WriteLine("1. Sistema de inventario y Durabilidad.");
                Console.WriteLine("2. Marcador TOP 5 de Speedrun.");
                Console.WriteLine("3. Combate contra hordas de slimes.");
                Console.WriteLine("4. Canje de recompensas en Tienda de cartas.");
                Console.WriteLine("5. Registro y Filtrado de daño por rafaga.");
                Console.WriteLine("6. Selector de Elementos y Cargas Magicas.");
                Console.WriteLine("7. Deteccion de trampas en pasillo de Mazmorra.");
                Console.WriteLine("8. Estadisticas de fases del Boss.");
                Console.WriteLine("9. Sistema de municion y Disparo por cargador.");
                Console.WriteLine("10. Asignador y Verificador de bonificacion de EXP.");
                Console.WriteLine("11. Salir");
                Console.WriteLine();
                Console.WriteLine("==============================");
                Console.WriteLine();

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        string[] armas = { "Espada", "Hacha", "Arco", "Daga" };
                        int[] durabilidades = new int[4];

                        for (int i = 0; i < durabilidades.Length; i++)
                        {
                            Console.Write($"Durabilidad de {armas[i]}: ");
                            durabilidades[i] = int.Parse(Console.ReadLine());
                        }

                        Console.Clear();
                        Console.WriteLine("1. Inspeccionar armas criticas.");
                        Console.WriteLine("2. Ver arsenal completo.");
                        int opcion1 = int.Parse(Console.ReadLine());

                        switch (opcion1)
                        {
                            case 1:
                                for (int i = 0; i < durabilidades.Length; i++)
                                {
                                    if (durabilidades[i] <= 20)
                                    {
                                        Console.WriteLine($"ALERTA: {armas[i]} DETERIORADA. (Durabilidad: {durabilidades[i]}).");
                                    }
                                }
                                Console.ReadKey();
                                break;

                            case 2:
                                Console.WriteLine("========ARMAS========");
                                for (int i = 0; i < armas.Length; i++)
                                {
                                    Console.WriteLine($"Arma: {armas[i]}    Durabilidad:{durabilidades[i]}");
                                }
                                Console.ReadKey();
                                break;

                            default:
                                Console.WriteLine("Opcion invalida.");
                                break;
                        }

                        break;
                    case 2:
                        bool continuar = true;
                        while (continuar)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Cargar tiempos y tiempo objetivo.");
                            Console.WriteLine("2. Salir.");

                            int opcion2 = int.Parse(Console.ReadLine());

                            switch (opcion2)
                            {
                                case 1:
                                    float[] tiempos = new float[5];
                                    float tiempoObjetivo;

                                    for (int i = 0; i < tiempos.Length; i++)
                                    {
                                        Console.Write($"Tiempo {i + 1}: ");
                                        tiempos[i] = float.Parse(Console.ReadLine());
                                    }

                                    bool evaluando = true;
                                    while (evaluando)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("=== EVALUACIÓN DE TIEMPO OBJETIVO ===");
                                        Console.Write("Ingrese el tiempo objetivo a superar (o ingresar -1 para salir): ");

                                        tiempoObjetivo = float.Parse(Console.ReadLine());

                                        if (tiempoObjetivo == -1)
                                        {
                                            evaluando = false;
                                        }
                                        else
                                        {
                                            int clasificados = 0;

                                            for (int i = 0; i < tiempos.Length; i++)
                                            {
                                                if (tiempos[i] <= tiempoObjetivo)
                                                {
                                                    clasificados++;
                                                }
                                            }

                                            if (clasificados > 0)
                                            {
                                                Console.WriteLine($"¡Éxito! Un total de {clasificados} corredor(es) superaron la prueba (tiempo <= {tiempoObjetivo}s).");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Ningún corredor logró bajar o igualar la marca de {tiempoObjetivo}s.");
                                            }

                                            Console.WriteLine("Presione cualquier tecla para evaluar otro tiempo...");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                                case 2:
                                    continuar = false;
                                    break;
                                default:
                                    Console.WriteLine("Opcion invalida.");
                                    break;
                            }
                        }
                        break;

                    case 3:
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    case 6:
                        Console.Clear();
                        break;
                    case 7:
                        Console.Clear();
                        break;
                    case 8:
                        Console.Clear();
                        break;
                    case 9:
                        Console.Clear();
                        break;
                    case 10:
                        Console.Clear();
                        break;
                    case 11:
                        Console.WriteLine("Saliendo de la aplicacion......");
                        Console.ReadKey();
                        Environment.Exit(0);

                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        Console.ReadKey();
                        break;
                }

            }
            while (true);
        }
    }
}
