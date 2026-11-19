using UnityEngine;

public class Herramienta : MonoBehaviour
{
    [SerializeField] private int durabilidadMaxima = 20; 
    [SerializeField] private int danioPorGolpe = 1;

    public bool puedeUsarse;

    public int Durabilidad { get; private set; }
    public int DanioPorGolpe => danioPorGolpe;

    private void Awake()
    {
        Durabilidad = durabilidadMaxima;
    }

    public bool PuedeUsarse()
    {
        return Durabilidad > 0;
    }

    public void Usar()
    {
        if (Durabilidad <= 0)
        {
            return;
        }

        Durabilidad--;

        Debug.Log($"Durabilidad restante: {Durabilidad}");

        if (Durabilidad == 0)
        {
            Debug.Log("La herramienta se rompió.");
        }
    }
}

