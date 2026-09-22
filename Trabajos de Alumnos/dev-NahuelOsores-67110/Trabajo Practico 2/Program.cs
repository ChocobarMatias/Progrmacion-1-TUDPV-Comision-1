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
                        int[] slimes = { 30, 40, 50, 60 };
                        bool hordaViva = true;
                        bool retirada = false;

                        while (hordaViva && !retirada)
                        {
                            Console.Clear();
                            Console.WriteLine("======= ¡COMBATE CONTRA LA HORDA DE SLIMES! =======");
                            Console.WriteLine("Estado actual de la horda:");

                            for (int i = 0; i < slimes.Length; i++)
                            {
                                if (slimes[i] > 0)
                                {
                                    Console.WriteLine($"Slime [{i}]: {slimes[i]} HP.");
                                }
                                else
                                {
                                    Console.WriteLine($"Slime [{i}]: DERROTADO.");
                                }
                            }
                            Console.WriteLine("====================================================");
                            Console.Write("Elige a qué slime atacar (0 al 3) o presiona 4 para retirarte: ");
                            int objetivo = int.Parse(Console.ReadLine());

                            switch (objetivo)
                            {
                                case 0:
                                case 1:
                                case 2:
                                case 3:
                                    if (slimes[objetivo] <= 0)
                                    {
                                        Console.WriteLine($"¡El Slime [{objetivo}] ya está derrotado! No desperdicies tu energía.");
                                    }
                                    else
                                    {
                                        slimes[objetivo] -= 20;
                                        Console.WriteLine($"¡Atacaste al Slime [{objetivo}]! Le restaste 20 HP.");

                                        if (slimes[objetivo] <= 0)
                                        {
                                            slimes[objetivo] = 0;
                                            Console.WriteLine($"¡El Slime [{objetivo}] ha sido eliminado!");
                                        }
                                    }
                                    break;
                                case 4:
                                    retirada = true;
                                    Console.WriteLine("Te has retirado del combate.");
                                    Console.ReadKey();
                                    break;
                                default:
                                    Console.WriteLine("Objetivo invalido. Elige un numero del 0 al 3.");
                                    break;
                            }
                            if (!retirada)
                            {
                                int slimesVivos = 0;

                                for (int i = 0; i < slimes.Length; i++)
                                {
                                    if (slimes[i] > 0)
                                    {
                                        slimesVivos++;
                                    }
                                }

                                if (slimesVivos == 0)
                                {
                                    hordaViva = false;
                                    Console.WriteLine("¡Felicidades derrotaste a la horda de slimes!");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Presiona cualquier tecla para el siguiente turno.");
                                    Console.ReadKey();
                                }
                            }
                        }
                        break;
                    case 4:
                        Console.Clear();
                        int[] cartasCostos = { 10, 25, 50, 80, 120 };
                        string[] cartasNombres = { "Rayo Cortante", "Espejo Rojo", "Bola de Fuego", "Juicio Solemne", "Agujero Negro" };

                        Console.Write("Introduce tus Gemas disponibles: ");
                        int gemasUsuario = int.Parse(Console.ReadLine());
                        bool continuar4 = true;

                        while (continuar4)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Mostrar cartas que puede pagar");
                            Console.WriteLine("2. Identificar la carta mas cara del catalogo");
                            Console.WriteLine("3. Salir");

                            int opcion3 = int.Parse(Console.ReadLine());

                            switch (opcion3)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("==========Cartas Disponibles==========");

                                    for (int i = 0; i < cartasCostos.Length; i++)
                                    {
                                        if (cartasCostos[i] <= gemasUsuario)
                                        {
                                            Console.WriteLine($"{cartasNombres[i]}: {cartasCostos[i]} G.");
                                        }
                                    }
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.Clear();
                                    int costoMasAlto = 0;
                                    string nombreCartaAlta = "";
                                    for (int i = 0; i < cartasCostos.Length; i++)
                                    {
                                        if (cartasCostos[i] > costoMasAlto)
                                        {
                                            costoMasAlto = cartasCostos[i];
                                            nombreCartaAlta = cartasNombres[i];
                                        }
                                    }
                                    Console.WriteLine($"La carta mas alta es: {nombreCartaAlta} {costoMasAlto} G.");
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    continuar4 = false;
                                    break;
                                default:
                                    Console.WriteLine("Opcion invalida.");
                                    break;
                            }
                        }
                        break;
                    case 5:
                        Console.Clear();
                        int[] dañoFlechas = new int[6];

                        for (int i = 0; i < dañoFlechas.Length; i++)
                        {
                            Console.Write($"Ingresar daño de la flecha [{i + 1}]: ");
                            dañoFlechas[i] = int.Parse(Console.ReadLine());
                        }

                        bool continuarFiltrando = true;

                        while (continuarFiltrando)
                        {
                            Console.Clear();
                            Console.WriteLine("=== FILTRADO DE IMPACTOS CRÍTICOS ===");
                            Console.Write("Ingrese el daño de referencia (o un numero negativo para salir): ");

                            int dañoReferencia = int.Parse(Console.ReadLine());

                            if (dañoReferencia < 0)
                            {
                                continuarFiltrando = false;
                                Console.WriteLine("Saliendo del registro de rafaga...");
                                Console.ReadKey();
                            }
                            else
                            {
                                //Console.Clear();
                                int acumuladorDaño = 0;
                                int cantidadImpactos = 0;

                                Console.WriteLine($"Impactos que superaron los {dañoReferencia} puntos de daño:");
                                Console.WriteLine("--------------------------------------------------");

                                for(int i = 0; i < dañoFlechas.Length; i++)
                                {
                                    if (dañoFlechas[i] > dañoReferencia)
                                    {
                                        Console.WriteLine($"Flecha [{i + 1}]: {dañoFlechas[i]} de daño.");
                                        acumuladorDaño += dañoFlechas[i];
                                        cantidadImpactos++;
                                    }
                                }

                                if (cantidadImpactos > 0)
                                {
                                    Console.WriteLine("--------------------------------------------------");
                                    Console.WriteLine($"Total de impactos filtrados: {cantidadImpactos}.");
                                    Console.WriteLine($"Suma total del daño filtrado: {acumuladorDaño} pts.");
                                }
                                else
                                {
                                    Console.WriteLine($"Ninguna flecha superó la referencia de {dañoReferencia} de daño.");
                                }

                                Console.WriteLine("Presione cualquier tecla para probar otra referencia...");
                                Console.ReadKey();
                            }
                        }
                        break;
                    case 6:
                        Console.Clear();
                        string[] gemas = { "Fuego", "Hielo", "Rayo", "Veneno"};
                        int[] cargas = new int[4];
                        bool continuar6 = true;
                        bool cargaValida;

                        Console.Clear();
                        for (int i = 0; i < cargas.Length; i++)
                        {
                            cargaValida = false;

                            while (!cargaValida)
                            {
                                Console.Write($"Carga de la gema de {gemas[i]} (de 0 a 100): ");
                                int carga = int.Parse(Console.ReadLine());

                                if (carga > 100 || carga < 0)
                                {
                                    cargaValida = false;
                                    Console.WriteLine("Carga invalida.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    cargaValida = true;
                                    cargas[i] = carga;
                                }
                            }
                        }

                        while (continuar6)
                        {
                            Console.Clear();
                            Console.WriteLine("================ GEMAS ================");
                            for (int i = 0; i < gemas.Length; i++)
                            {
                                Console.WriteLine($"Gema: {gemas[i]} Cargas: {cargas[i]}/100");
                            }
                            Console.WriteLine("=======================================");
                            Console.WriteLine("1: Recargar todas las gemas (+5 cargas)");
                            Console.WriteLine("2: Buscar si hay alguna gema agotada");
                            Console.WriteLine("3: Salir");

                            int opcion6 = int.Parse(Console.ReadLine());
                            switch (opcion6)
                            {
                                case 1:
                                    for (int i = 0; i < cargas.Length; i++)
                                    {
                                        if (cargas[i] > 95)
                                        {
                                            cargas[i] = 100;
                                        }
                                        else
                                        {
                                            cargas[i] += 5;
                                        }                             
                                    }
                                    Console.WriteLine("Gemas cargadas presione cualquier tecla para continuar.");
                                    Console.ReadKey();
                                    break;

                                case 2:
                                    int gemasAgotadas = 0;
                                    for (int i = 0; i < cargas.Length; i++)
                                    {
                                        if (cargas[i] <= 0)
                                        {
                                            gemasAgotadas++;
                                        }
                                    }
                                    Console.WriteLine($"Gemas agotadas: {gemasAgotadas}.");
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    continuar6 = false;
                                    break;
                            }
                        }

                        break;
                    case 7:
                        Console.Clear();

                        bool[] trampas = { false, true, false, false, true, false};
                        int posicion = 0;
                        bool Vivo = true;

                        Console.WriteLine("=== ¡PASILLO DE LA MAZMORRA! ===");
                        Console.WriteLine("Comienzas en la baldosa 0. La meta es la baldosa 5.");
                        Console.WriteLine("¡Cuidado con las trampas ocultas!");

                        //Console.ReadKey();

                        while (Vivo && posicion < 5)
                        {
                            Console.WriteLine($"Te encuentras en la baldosa: {posicion}");
                            Console.Write("Elige tu movimiento (1 o 2 para avanzar, 0 o negativo para retroceder 1): ");
                            int paso = int.Parse(Console.ReadLine());

                            if (paso <= 0)
                            {
                                posicion -= 1;
                            }
                            else
                            {
                                posicion += paso;
                            }
                            if (posicion < 0)
                            {
                                posicion = 0;
                                Console.WriteLine("Te chocaste con la pared del inicio. Sigues en la baldosa 0.");
                                continue;
                            }

                            if (posicion >= 5)
                            {
                                Console.WriteLine("¡Llegaste a la meta! ¡Felicidades!");    
                                break;
                            }

                            if (trampas[posicion])
                            {
                                Console.WriteLine($"Perdiste en la baldosa: {posicion} FIN DE LA PARTIDA.");
                                Vivo = false;
                            }
                            else
                            {
                                Console.WriteLine($"Seguis vivo pisaste una balsoda segura {posicion}.");
                            }
                            
                        }
                        if (Vivo)
                        {
                            Console.WriteLine("¡Felicidades! Has completado el recorrido.");
                            
                        }
                        Console.WriteLine("Presione cualquier tecla para regresar al menú...");
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();
                        int[] dañoFases = new int[3];
                        bool continuar8 = true;

                        for (int i = 0; i < dañoFases.Length; i++)
                        {
                            Console.Write($"Daño recibido de la Fase {i + 1}: ");
                            int daño = int.Parse(Console.ReadLine());

                            if (daño < 0)
                            {
                                Console.WriteLine("Daño negativo ingrese un numero positivo o 0.");
                                Console.ReadKey();
                                i--;
                            }
                            else
                            {
                                dañoFases[i] = daño;
                            }
                        }

                        while (continuar8)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Calcular promedio de daño entre las 3 fases.");
                            Console.WriteLine("2. Identificar la fase más destructiva.");
                            Console.WriteLine("3. Salir.");

                            int opcion8 = int.Parse(Console.ReadLine());

                            switch (opcion8)
                            {
                                case 1:
                                    Console.Clear();
                                    int dañoAcumulado = 0;

                                    for(int i = 0; i < dañoFases.Length; i++)
                                    {
                                        dañoAcumulado += dañoFases[i];
                                    }
                                    dañoAcumulado /= 3;
                                    Console.WriteLine($"El promedio de daño entre fases es de: {dañoAcumulado}.");
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    int dañoMasAlto = 0;
                                    for (int i = 0; i < dañoFases.Length; i++)
                                    {
                                        if (dañoFases[i] > dañoMasAlto)
                                        {
                                            dañoMasAlto = dañoFases[i];
                                        }
                                    }
                                    Console.WriteLine($"El daño mas destructivo es de: {dañoMasAlto}.");
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    continuar8 = false;
                                    break;
                                default:
                                    Console.WriteLine("Opcion invalida.");
                                    Console.ReadKey();
                                    break;
                            }
                        }

                        
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
