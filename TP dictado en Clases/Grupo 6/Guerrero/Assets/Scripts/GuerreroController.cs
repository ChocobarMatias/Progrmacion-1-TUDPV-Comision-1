using UnityEngine;

public class GuerreroController : MonoBehaviour
{
    private GuerreroLogico Guerrero;

    [Header("Configuracion de Personaje")]
    [SerializeField] private string Nombre = "Nick";
    [SerializeField] private int VidaMaxima = 100;
    [SerializeField] private int EscudoInicial = 30;
    [Range(2f, 8f)]
    [SerializeField] private float VelocidadMovimiento = 5f;

    private Rigidbody2D RG2;
    private Vector2 direccionMovimiento;

    void Start()
    {
        RG2 = GetComponent<Rigidbody2D>();
        Guerrero = new GuerreroLogico(Nombre, VidaMaxima, EscudoInicial);
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        direccionMovimiento = new Vector2(inputX, inputY).normalized;
    }

    private void FixedUpdate()
    {
        if (Guerrero.EstaVivo)
            RG2.linearVelocity = VelocidadMovimiento * direccionMovimiento;

        else
            RG2.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Guerrero.EstaVivo) return;

        if (collision.CompareTag("Enemigo"))
        {
            int danioRecibido = 25;

            Guerrero.RecibirDanio(danioRecibido);

            Debug.Log($"¡Golpe recibido! Daño: {danioRecibido}");
        }
        else if (collision.CompareTag("Pocion"))
        {
            int cantidadCuracion = 25;

            Guerrero.Curar(cantidadCuracion);

            Debug.Log($"¡Poción consumida! Cura: {cantidadCuracion}.");

            Destroy(collision.gameObject);
        }
    }
}
