using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float velocidad = 7f;

    private Rigidbody2D rb;
    private float movimientoX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Referencia al cuerpo físico
    }

    private void Update()
    {
        movimientoX = Input.GetAxisRaw("Horizontal"); // Input solo en Update
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y); // Conserva la caída
    }
}
