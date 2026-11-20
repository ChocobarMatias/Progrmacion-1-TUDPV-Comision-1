using System;

public abstract class CriaturaHollowNestBase
{
    public CriaturaHollowNestBase(int Salud, string nombre, int danio)
    {
        this.salud = Salud;
        this.nombre = nombre;
        this.danio = danio;
    }

    protected int salud;
    public int Salud
    {
        get { return salud; }
        set { salud = value; }
    }

    protected string nombre;
    protected int danio;
    protected bool estaVivo = true;

    public abstract void ReaccionarGolpe();
    public abstract void Atacar();
}

