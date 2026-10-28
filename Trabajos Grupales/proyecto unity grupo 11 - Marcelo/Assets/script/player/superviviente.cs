
using UnityEngine;

public class Superviviente
{
    private string nombre;
    private int salud;
    private bool envenenado;
    private ConsumibleSurvival[] inventario;

    public string Nombre
    {
        get { return nombre; }
        private set
        {
            if (!string.IsNullOrWhiteSpace(value))
                nombre = value;
        }
    }

    public int Salud
    {
        get { return salud; }
        set
        {
            if (value < 0)
                salud = 0;
            else if (value > 100)
                salud = 100;
            else
                salud = value;
        }
    }

    public bool Envenenado
    {
        get { return envenenado; }
        set { envenenado = value; }
    }

    public Superviviente(string nombre, int salud, bool envenenado)
    {
        this.Nombre = nombre;
        this.Salud = salud;
        this.Envenenado = envenenado;
        this.inventario = new ConsumibleSurvival[4];
    }

    public void RecuperarSalud(int cantidad)
    {
        if (cantidad > 0)
            Salud += cantidad;
    }


    public void RecibirDano(int cantidad)
    {
        if (cantidad > 0)
            Salud -= cantidad;

        Debug.Log("Salud actual: " + Salud);
    }


    public void RestaurarSaludCompleta()
    {
        Salud = 100;
    }

    public void CurarEnvenenamiento()
    {
        Envenenado = false;
    }

    public void AgregarItem(ConsumibleSurvival item)
    {
        if (item == null)
            return;

        for (int i = 0; i < inventario.Length; i++)
        {
            if (inventario[i] == null)
            {
                inventario[i] = item;
                Debug.Log(item.Nombre + " agregado al inventario.");
                return;
            }
        }

        Debug.Log("Inventario lleno.");
    }


    public bool UsarItem(int slot)
    {
        if (slot < 1 || slot > inventario.Length)
        {
            Debug.Log("Espacio inválido.");
            return false;
        }

        if (inventario[slot - 1] == null)
        {
            Debug.Log("Ese espacio está vacío.");
            return false;
        }

        inventario[slot - 1].Consumir(this);
        inventario[slot - 1] = null;

        return true;
    }


    public void MostrarInventario()
    {
        Debug.Log("Inventario:");

        for (int i = 0; i < inventario.Length; i++)
        {
            if (inventario[i] != null)
                Debug.Log((i + 1) + ". " + inventario[i].Nombre);
            else
                Debug.Log((i + 1) + ". Vacío");
        }
    }

    public void MostrarEstado()
    {
        Debug.Log("Superviviente: " + Nombre);
        Debug.Log("Salud: " + Salud);
        Debug.Log("Envenenado: " + (Envenenado ? "Sí" : "No"));
    }
}

