int[] HpSlime = { 30, 40, 50, 60 };

while (HpSlime[0] > 0 || HpSlime[1] > 0 || HpSlime[2] > 0 || HpSlime[3] > 0)
{
    Console.WriteLine("Elija que slime atacar entre el 0 y 3    ");
    int ataque = int.Parse(Console.ReadLine());

    switch (ataque)
    {
        case 0:
            if (HpSlime[0] <= 0)
            {
                Console.WriteLine("Murio ");

            }

            else
            {
                HpSlime[0] = HpSlime[0] - 20;
            }
            
        break;

        case 1:
            if (HpSlime[1] <= 0)
            {
                Console.WriteLine("Murio ");

            }

            else
            {
                HpSlime[1] = HpSlime[1] - 20;
            }
            break;

        case 2:
            if (HpSlime[2] <= 0)
            {
                Console.WriteLine("Murio ");

            }

            else
            {
                HpSlime[2] = HpSlime[2] - 20;
            }
            break;

        case 3:
            if (HpSlime[3] <= 0)
            {
                Console.WriteLine("Murio ");

            }

            else
            {
                HpSlime[3] = HpSlime[3] - 20;
            }
            break;

        default:
            Console.WriteLine("Elegí un slime válido.");
            break;
    }
        

}