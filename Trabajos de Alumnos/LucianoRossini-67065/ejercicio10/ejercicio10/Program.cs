int[] experiencia = new int[5];

for (int i = 0; i < experiencia.Length; i++)
{
    Console.WriteLine("Ingrese la EXP obtenida en la misión " + (i + 1));
    experiencia[i] = int.Parse(Console.ReadLine());

    if (experiencia[i] > 100)
    {
        experiencia[i] = experiencia[i] + (experiencia[i] * 20 / 100);
    }
    else
    {
        Console.WriteLine("La misión no recibe bonificación.");
    }
}

int total = 0;

Console.WriteLine("Tabla de experiencia actualizada:");

for (int i = 0; i < experiencia.Length; i++)
{
    Console.WriteLine("Misión " + (i + 1) + ": " + experiencia[i] + " EXP");
    total = total + experiencia[i];
}

Console.WriteLine("Experiencia total acumulada: " + total);
