int[] soldadoMunicion = { 30, 15, 8 };
string[] soldadoArmas = { "Rifle", "Pistola", "Escopeta" };

int soldadoArmasVacias = 0;
bool continuarPrograma = true;

while (continuarPrograma)
{
    for (int i = 0; i > soldadoMunicion.Length; i++)
    {
        if (soldadoMunicion[i] == 0)
        {
            soldadoArmasVacias++;
        }
    }
    if (soldadoArmasVacias == 3)
    {
        continuarPrograma = false;
        break;
    }
    else
    {
        soldadoArmasVacias = 0;
    }

    Console.WriteLine("Elija un arma para disparar:");
    Console.WriteLine("- - - - - - - - - - - - - -");
    Console.WriteLine($"1. Rifle (municion: {soldadoMunicion[0]})");
    Console.WriteLine($"2. Pistola (municion: {soldadoMunicion[1]}");
    Console.WriteLine($"3. Escopeta (municion: {soldadoMunicion[2]}");
}
