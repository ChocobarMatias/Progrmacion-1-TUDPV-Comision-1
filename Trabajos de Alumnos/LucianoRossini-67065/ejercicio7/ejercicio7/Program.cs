bool[] trampas = {false, true, false, false, true, false };
int casillero = 0;

while (!trampas[casillero] && casillero != 5)
{
    casillero = casillero + 1;
    if (trampas[casillero] == true)
    {
        Console.WriteLine("Perdiste, pisaste una trampa");  
    }
    else
    {
        if (casillero == 5)
        {
            Console.WriteLine("se completo el recorrido");
        }
    }
}

//me re costo el ejercicio este porque no entendia si hice mal pero en realidad la consigna pide que al primer error se cancele el while y pierda pero en el vector nro 1 ya es trampa, al no llegar al final o tener mas opciones la dude toda
