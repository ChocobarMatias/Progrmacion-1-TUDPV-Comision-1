using UnityEngine;

public class BloqueDestructible : MonoBehaviour
{
    [SerializeField] private int vida = 3; 
    [SerializeField] private GameObject prefabDrop; 

    [SerializeField] private int cantidadDrops = 1;

    private bool estaRoto;

    public void RecibirGolpe(int danio)
    {
        if (estaRoto || danio <= 0)
        {
            return;
        }

        vida -= danio;

        if (vida <= 0)
        {
            Romper();
        }
    }

    private void Romper()
    {
        if (estaRoto)
        {
            return;
        }

        estaRoto = true;
        //CrearDrops();

        Destroy(gameObject);
    }

    private void CrearDrops()
    {
        if (prefabDrop == null)
        {
            return;
        }

        for (int i = 0; i < cantidadDrops; i++)
        {
            Vector2 posicionDrop = (Vector2)transform.position
                + Random.insideUnitCircle * 0.2f;

            Instantiate(prefabDrop, posicionDrop, Quaternion.identity);
        }
    }
}
