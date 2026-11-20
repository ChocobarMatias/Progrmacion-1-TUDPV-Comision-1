using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PJControllerRB : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float saltoFuerza = 10f;
    [SerializeField] private Transform puntoSuelo;
    [SerializeField] private float radioSuelo = 0.15f;
    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool quiereSaltar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            quiereSaltar = true;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * velocidad, rb.linearVelocity.y);

        if (quiereSaltar && EstaEnElSuelo())
        {
            rb.AddForce(Vector2.up * saltoFuerza, ForceMode2D.Impulse);
        }

        quiereSaltar = false;
    }

    private bool EstaEnElSuelo()
    {
        return Physics2D.OverlapCircle(puntoSuelo.position, radioSuelo, capaSuelo) != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoSuelo == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(puntoSuelo.position, radioSuelo);
    }
}
