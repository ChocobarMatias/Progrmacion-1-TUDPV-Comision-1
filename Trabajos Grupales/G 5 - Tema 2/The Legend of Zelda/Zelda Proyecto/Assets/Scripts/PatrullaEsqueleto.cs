using UnityEngine;

// Patrulla por un array fijo de puntos: A -> B -> C -> A ...
// Requiere Rigidbody2D (recomendado: Kinematic). Los puntos NO deben ser hijos del esqueleto.
[RequireComponent(typeof(Rigidbody2D))]
public class PatrullaEsqueleto : MonoBehaviour
{
    [SerializeField] private Transform[] puntosPatrulla; // arrastrar PuntoA, PuntoB, PuntoC en orden
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float distanciaLlegada = 0.1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int indiceActual;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (puntosPatrulla == null || puntosPatrulla.Length == 0 || puntosPatrulla[indiceActual] == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 destino = puntosPatrulla[indiceActual].position;
        Vector2 hacia = destino - rb.position;

        if (hacia.magnitude <= distanciaLlegada)
        {
            indiceActual = (indiceActual + 1) % puntosPatrulla.Length;
            return;
        }

        Vector2 dir = hacia.normalized;
        rb.linearVelocity = dir * velocidad;

        if (sr != null && Mathf.Abs(dir.x) > 0.01f) sr.flipX = dir.x < 0f;
    }

    // Dibuja el recorrido en la vista Scene para configurarlo fácil
    private void OnDrawGizmos()
    {
        if (puntosPatrulla == null || puntosPatrulla.Length < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < puntosPatrulla.Length; i++)
        {
            Transform a = puntosPatrulla[i];
            Transform b = puntosPatrulla[(i + 1) % puntosPatrulla.Length];
            if (a == null || b == null) continue;
            Gizmos.DrawLine(a.position, b.position);
            Gizmos.DrawWireSphere(a.position, 0.15f);
        }
    }
}
