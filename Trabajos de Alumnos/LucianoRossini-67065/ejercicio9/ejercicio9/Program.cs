int[] municion = { 30, 15, 8 };

int opcion = -1;

while (opcion != 0)
{
    Console.WriteLine("Seleccione un arma:");
    Console.WriteLine("1 - Rifle");
    Console.WriteLine("2 - Pistola");
    Console.WriteLine("3 - Escopeta");
    Console.WriteLine("0 - Salir");

    opcion = int.Parse(Console.ReadLine());

    switch (opcion)
    {
        case 1:
            if (municion[0] > 0)
            {
                municion[0]--;
                Console.WriteLine("Disparo realizado. Balas restantes: " + municion[0]);
            }
            else
            {
                Console.WriteLine("El Rifle está vacío.");
            }
            break;

        case 2:
            if (municion[1] > 0)
            {
                municion[1]--;
                Console.WriteLine("Disparo realizado. Balas restantes: " + municion[1]);
            }
            else
            {
                Console.WriteLine("La Pistola está vacía.");
            }
            break;

        case 3:
            if (municion[2] > 0)
            {
                municion[2]--;
                Console.WriteLine("Disparo realizado. Balas restantes: " + municion[2]);
            }
            else
            {
                Console.WriteLine("La Escopeta está vacía.");
            }
            break;

        case 0:
            Console.WriteLine("Fin del sistema de disparo.");
            break;

        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
}