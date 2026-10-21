int[] flechas = new int[6];
for (int i = 0; i < flechas.Length; i++)
{
    Console.WriteLine("ingrese damage de flechas");
    flechas[i] = int.Parse(Console.ReadLine());
}

bool continuar = true;

while (continuar)
{
    Console.WriteLine("Damage de referencia?");
   int referencia = int.Parse(Console.ReadLine());

    int suma = 0;

    for (int i = 0; i < flechas.Length; i++)
    {
        if (flechas[i] > referencia)
        {
            suma += flechas[i];
        }
        else
        {
            Console.WriteLine("Impacto descartado");
        }
    }
    Console.WriteLine("la suma del damage es de " + suma);
    Console.WriteLine("Desea seguir o salir?");
    string respuesta = Console.ReadLine();
   
    if(respuesta == "seguir")
    {
     
    }
    else { continuar = false; }
}