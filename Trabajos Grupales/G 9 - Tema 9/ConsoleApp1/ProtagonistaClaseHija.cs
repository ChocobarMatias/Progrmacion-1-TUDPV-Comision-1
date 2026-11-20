using System;

public class Protagonista : CriaturaHollowNestBase
{
    public Protagonista(int ReservaAlma) : base(100, "Protagonista", 40)
    {
        this.reservaAlma = ReservaAlma;
    }

    private new bool estaVivo = true;
    public bool EstaVivo
    {
        get { return estaVivo; }
        set { estaVivo = value; }
    }

    public void UsarAmuleto()
    {
        Console.WriteLine("¡El protagonista ha usado un amuleto!");
        danio += 20;
        ReservaAlma -= 10;
    }
    private int reservaAlma;

    public int ReservaAlma
    {
        get { return reservaAlma; }
        set 
        { 
            if (value < 0) //Si el valor es menor a 0, se asigna 0 a reservaAlma
            {
                reservaAlma = 0;
            }
            else
            {
                reservaAlma = value;
            }

            if (value > 99) //Si el valor es mayor a 99, se asigna 99 a reservaAlma
            {
                reservaAlma = 99;
            }
            else
            {
                reservaAlma = value;
            }
        }
    }
    public override void ReaccionarGolpe()
    {
        Console.WriteLine("¡El protagonista ha recibido un golpe!");
        Salud -= danio;
    }
    public void ConcentrarAlma()
    {
        Console.WriteLine("¡El protagonista se cura!");
        Salud += 35;
        ReservaAlma -= 20;
    }
    public override void Atacar()
    {
        Console.WriteLine("¡El protagonista ataca!");
        ReservaAlma += 10;
    }
}
