using UnityEngine;

// Prefab del rayo de espada. Requiere: Rigidbody2D (Dynamic, Gravity 0) + Collider2D con Is Trigger.
[RequireComponent(typeof(Rigidbody2D))]
public class RayoEspada : MonoBehaviour
{
    [SerializeField] private float velocidad = 9f;
    [SerializeField] private float tiempoVidaMaximo = 2f;
    [SerializeField] private AudioClip sonidoImpacto;
    [SerializeField] private GameObject efectoImpacto; // opcional (chispa, partícula)

    private Rigidbody2D rb;
    private int danio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Start()
    {
        // Destrucción programada: si no choca con nada, igual se borra de memoria
        Destroy(gameObject, tiempoVidaMaximo);
    }

    public void Lanzar(Vector2 direccion, int danioRayo)
    {
        danio = danioRayo;
        Vector2 dir = direccion.sqrMagnitude > 0f ? direccion.normalized : Vector2.down;

        rb.linearVelocity = dir * velocidad;

        // El sprite del prefab tiene que apuntar hacia la DERECHA
        float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignora sensores (hitbox de espada, zona del cofre, etc.) y al propio Link
        if (other.isTrigger) return;
        if (other.GetComponentInParent<PlayerController>() != null) return;

        Enemigo enemigo = other.GetComponentInParent<Enemigo>();
        if (enemigo != null) enemigo.RecibirDanio(danio);

        // Choca contra enemigo o pared: sonido y se destruye
        Impactar();
    }

    private void Impactar()
    {
        GameManager.Sonar(sonidoImpacto);

        if (efectoImpacto != null)
        {
            GameObject fx = Instantiate(efectoImpacto, transform.position, Quaternion.identity);
            Destroy(fx, 1f);
        }

        Destroy(gameObject);
    }
}
