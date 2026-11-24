using UnityEngine;

// Va en el Prefab de chispas. Se autodestruye para no dejar basura en memoria.
public class Chispas : MonoBehaviour
{
    [SerializeField] private float tiempoDeVida = 0.5f;

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }
}
