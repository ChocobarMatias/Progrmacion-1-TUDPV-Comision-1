int[] fase = new int[3];

for (int i = 0; i < fase.Length; i++)
{
    Console.WriteLine("Indique damage de las fases");
    fase[i] = int.Parse(Console.ReadLine());
}

Console.WriteLine("Seleccione una opción:");
Console.WriteLine("1 - Calcular promedio de daño");
Console.WriteLine("2 - Identificar la fase más destructiva");
int opcion = int.Parse(Console.ReadLine());

switch (opcion)
{
    case 1:
        int suma = 0;

        for (int i = 0; i < fase.Length; i++)
        {
            suma = suma + fase[i];
        }

        float promedio = suma / 3f;

        Console.WriteLine("El promedio de daño es: " + promedio);
        break;

    case 2:
        int DamageMax = fase[0];
        int FaseMax = 0;

        for (int i = 0; i < fase.Length; i++)
        {
            if (fase[i] >= DamageMax)
            {
                DamageMax = fase[i];
                FaseMax = i;
            }
        }

        Console.WriteLine("La fase más destructiva fue la fase " + (FaseMax + 1));
        Console.WriteLine("Daño recibido: " + DamageMax);
        break;

    default:
        Console.WriteLine("Opción inválida");
        break;
}