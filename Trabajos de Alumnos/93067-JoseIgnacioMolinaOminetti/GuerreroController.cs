using UnityEngine;

// Componente de Unity (Controlador / Físicas) que USA la clase pura
[RequireComponent(typeof(Rigidbody2D))]
public class GuerreroController : MonoBehaviour
{
    [Header("Datos iniciales del modelo")]
    [SerializeField] private string nombre = "Aragorn";
    [SerializeField] private int vidaMax = 100;
    [SerializeField] private int escudoInicial = 30;

    [Header("Movimiento")]
    [SerializeField] private float velocidad = 5f;

    [Header("Daño / Curación por trigger")]
    [SerializeField] private int danioEnemigo = 20;
    [SerializeField] private int curacionPocion = 15;

    private GuerreroLogico guerrero;   // instancia del modelo POO
    private Rigidbody2D rb;
    private Vector2 direccion;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;          // vista top-down
        guerrero = new GuerreroLogico(nombre, vidaMax, escudoInicial);
    }

    void Start()
    {
        MostrarEstado("Inicio");
    }

    // Lectura de teclado
    void Update()
    {
        if (!guerrero.EstaVivo)
        {
            direccion = Vector2.zero;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");   // A/D o flechas
        float v = Input.GetAxisRaw("Vertical");     // W/S o flechas
        direccion = new Vector2(h, v).normalized;
    }

    // Movimiento físico
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);
    }

    // Punto de entrada público para cualquier fuente de daño (enemigo, lava, etc.)
    public void RecibirDanio(int cantidad, string origen = "Golpe recibido")
    {
        if (!guerrero.EstaVivo) return;

        guerrero.RecibirDanio(cantidad);   // la lógica (escudo → vida) vive en la clase pura
        MostrarEstado(origen);

        if (!guerrero.EstaVivo)
        {
            Debug.Log(guerrero.Nombre + " ha muerto.");
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    // Detección de daño / curación
    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Enemigo"))
        {
            RecibirDanio(danioEnemigo);
        }
        else if (otro.CompareTag("Pocion"))
        {
            guerrero.Curar(curacionPocion);
            MostrarEstado("Curación");
            Destroy(otro.gameObject);
        }
    }

    private void MostrarEstado(string evento)
    {
        Debug.Log($"[{evento}] {guerrero.Nombre} | Vida: {guerrero.VidaActual} | " +
                  $"Escudo: {guerrero.Escudo} | Vivo: {guerrero.EstaVivo}");
    }
}
