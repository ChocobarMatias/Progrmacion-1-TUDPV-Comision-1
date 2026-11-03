using UnityEngine;

public class GuerreroLogico
{
    private string nombre;
    private int vidaMax;
    private int vidaActual;
    private int escudo;

    public string Nombre { get; }

    public int VidaActual { get { return vidaActual; }
        private set
        {
            if (value < 0)
                value = 0;

            if (value > vidaMax)
                value = vidaMax;

            vidaActual = value;
        }
    }

    public int Escudo { get; }
    public bool EstaVivo { get { return vidaActual > 0; } }

    public GuerreroLogico(string nombre, int vidaMax, int escudoInicial)
    {
        this.nombre = nombre;
        this.vidaMax = vidaMax;
        vidaActual = this.vidaMax;
        this.escudo = escudoInicial;
    }

    public void RecibirDanio(int cantidad)
    {
        if (EstaVivo) return;

        if (escudo >= cantidad)
            escudo -= cantidad;

        if(escudo < cantidad)
        {
            int restante = cantidad - escudo;
            escudo = 0;
            VidaActual -= restante;
        }
    }

    public void Curar(int cantidad)
    {
        if (EstaVivo)
            VidaActual += cantidad;
    }
}
