float[] timeRun = new float [5];
for (int i = 0; i < timeRun.Length; i++)
{
    Console.WriteLine ("Ingrese timepo de participante");
    timeRun[i] = float.Parse(Console.ReadLine());
}

bool continuar = true;

while (continuar)
{
    Console.WriteLine("Ingrese la marca objetivo:");
    float marcas = float.Parse(Console.ReadLine());

    int contador = 0;

    for (int i = 0; i < timeRun.Length; i++) 
    {
        if (timeRun[i] <= marcas)
        {
            contador++;  

        }

    }
    Console.WriteLine ("corredores que superaron la prueba: " + contador);
    Console.WriteLine("Deseas seguir?");
    Console.WriteLine("Aprete S/N");
    string respuesta = Console.ReadLine();
    if (respuesta == "n")
    {
        continuar = false;
    }
    else 
    { 
        continuar = true;
    }

}


