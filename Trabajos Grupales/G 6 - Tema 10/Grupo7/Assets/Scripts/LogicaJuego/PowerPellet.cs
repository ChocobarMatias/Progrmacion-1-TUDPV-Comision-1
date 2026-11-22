using UnityEngine;

using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    [SerializeField] private float duracion = 6f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        FantasmaController[] fantasmas = FindObjectsByType<FantasmaController>(FindObjectsSortMode.None);
        foreach (FantasmaController f in fantasmas)
        {
            f.ActivarVulnerable(duracion);
        }

        Destroy(gameObject);
    }
}

