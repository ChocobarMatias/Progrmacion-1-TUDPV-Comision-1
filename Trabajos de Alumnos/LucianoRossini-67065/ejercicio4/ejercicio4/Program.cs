int[] costoGema = { 10, 25, 50, 80, 120 };

Console.WriteLine("Ingrese su saldo de gemas");

int saldo = int.Parse(Console.ReadLine());

Console.WriteLine("Bienvenido a la tienda");
Console.WriteLine("1 - Ver cartas que puedo pagar");
Console.WriteLine("2 - Ver carta más cara");
Console.WriteLine("Que opcion elegis?");

int menu = int.Parse(Console.ReadLine());

switch (menu)
{
    case 1:
        for (int i = 0; i < costoGema.Length; i++)
        {
            if (saldo >= costoGema[i])
            {
                Console.WriteLine("Te alcanza para esto " + costoGema[i]);

            }
        }
    
    break;

    case 2:
        int mayor = 0;
        for (int i = 0; i < costoGema.Length; i++)
        {
            if (costoGema[i] > mayor)
            {
                mayor = costoGema[i];
            }
        }
        Console.WriteLine("La carta mas cara cuesta " + mayor + " gemas");
        break;

}



