using System;

public class JefeHusk : CriaturaHollowNestBase
{
    public JefeHusk() : base(200, "Jefe Husk", 25)
    {
    }
    
    private new bool estaVivo = true;
    public bool EstaVivo
    {
        get { return estaVivo; }
        set { estaVivo = value; }
    }

    public override void ReaccionarGolpe()
    {
        Console.WriteLine("¡El Jefe Husk ha recibido un golpe!");
        Salud -= danio;
    }
    public override void Atacar()
    {
        Console.WriteLine("¡El Jefe Husk ataca!");
    }
}
