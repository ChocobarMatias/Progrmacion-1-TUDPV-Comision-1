string[] elementoGema = {"fuego", "hielo", "rayo","veneno"};
int[] cargas = new int[4];

for (int i = 0; i < cargas.Length; i++)
{
    //Console.WriteLine(elementoGema[i]);//
    Console.WriteLine("Cuantas cargas le pondras a la gema " + elementoGema[i] + "?");
    cargas[i] = int.Parse(Console.ReadLine());      
}
Console.WriteLine("Elija opcion");
Console.WriteLine("Opcion 1 recargar todas las gemas");
Console.WriteLine("Opcion 2 buscar gema sin carga");
int menu = int.Parse(Console.ReadLine());

switch (menu)
{
    case 1:
        for (int i = 0; i < cargas.Length; i++)
        {
            cargas[i] = cargas[i] + 5;
            Console.WriteLine("La gema " + elementoGema[i] + " quedo con " + cargas[i] + " cargas ");
        }
        break;

        case 2:

        for (int i = 0; i < cargas.Length; i++)
        {
            if (cargas[i] == 0)
            {
                Console.WriteLine("La gema agotada es: " + elementoGema[i] + " con " + cargas[i] + " cargas");
            }
        }
        break;
}