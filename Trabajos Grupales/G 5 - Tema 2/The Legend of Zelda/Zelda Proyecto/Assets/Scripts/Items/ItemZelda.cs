using UnityEngine;
using System;

// Clase pura abstracta (Semana 1).
// Refactor Fase 3: se agregan validaciones en el constructor, igual que en Link.
public abstract class ItemZelda
{
    public string Nombre { get; protected set; }
    public int Durabilidad { get; protected set; }

    public ItemZelda(string nombre, int durabilidad)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El ítem necesita un nombre.", nameof(nombre));
        if (durabilidad <= 0)
            throw new ArgumentOutOfRangeException(nameof(durabilidad), "La durabilidad debe ser mayor que cero.");

        this.Nombre = nombre;
        this.Durabilidad = durabilidad;
    }

    public abstract void Usar(Link usuario);

    protected void ReducirDurabilidad(int cantidad)
    {
        this.Durabilidad = Mathf.Max(0, this.Durabilidad - cantidad);
        if (this.Durabilidad == 0)
        {
            Debug.Log($"¡El objeto {Nombre} se ha roto!");
        }
    }
}
