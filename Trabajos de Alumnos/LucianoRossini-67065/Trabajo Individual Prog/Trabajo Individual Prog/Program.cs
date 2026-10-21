


  String[] armas = {"espada", "hacha", "arco", "daga"};
int[] durabilidad = new int[4];

for (int i = 0; i < armas.Length; i++)
{
    Console.WriteLine("Ingrese nro de durabilidad del arma: " + armas[i]);
    durabilidad[i] = int.Parse(Console.ReadLine()); 
}

    Console.WriteLine("Ver armas criticas");
    Console.WriteLine("Ver arsenal completo");
    int menu = int.Parse(Console.ReadLine());

switch (menu)
{
    case 1:
        for (int i = 0; i < armas.Length; i++)
        {
            if (durabilidad[i] <= 20)
                Console.WriteLine("tu arma " + armas[i] + " esta critica");
        }
        break;

        case 2:
        for (int i = 0; i < armas.Length; i++)
        {
            Console.WriteLine(armas[i] + " - " + durabilidad[i]);
        }
        break;

        default:
        Console.WriteLine("opcion invalida");
        break;
}

