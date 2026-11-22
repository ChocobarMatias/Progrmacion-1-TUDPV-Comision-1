using System;

// Clase pura de C#: la lógica de vida del enemigo, sin depender de Unity.
// Refactor Fase 3: antes esta lógica vivía dentro de Enemigo : MonoBehaviour ("God Class").
public class EnemigoModelo
{
    public string Nombre { get; }
    public int VidaMaxima { get; }
    public int Vida { get; private set; }
    public int DanioContacto { get; }
    public bool EstaMuerto => Vida <= 0;

    public EnemigoModelo(string nombre, int vidaMaxima, int danioContacto)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El enemigo necesita nombre.", nameof(nombre));
        if (vidaMaxima <= 0)
            throw new ArgumentOutOfRangeException(nameof(vidaMaxima), "La vida debe ser mayor que cero.");
        if (danioContacto < 0)
            throw new ArgumentOutOfRangeException(nameof(danioContacto), "El daño de contacto no puede ser negativo.");

        Nombre = nombre;
        VidaMaxima = vidaMaxima;
        Vida = vidaMaxima;
        DanioContacto = danioContacto;
    }

    public void RecibirDanio(int cantidad)
    {
        if (cantidad < 0)
            throw new ArgumentOutOfRangeException(nameof(cantidad), "El daño no puede ser negativo.");
        Vida = Math.Max(0, Vida - cantidad);
    }
}
