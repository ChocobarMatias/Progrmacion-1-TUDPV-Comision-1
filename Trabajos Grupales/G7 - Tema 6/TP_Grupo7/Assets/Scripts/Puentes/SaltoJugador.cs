using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SaltoJugador : MonoBehaviour
{
    [SerializeField] private float fuerzaSalto = 12f;

    private Rigidbody2D rb;
    private bool enSuelo;
    private bool saltoPedido;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && enSuelo) saltoPedido = true; // Solo registra la intención
    }

    private void FixedUpdate()
    {
        if (!saltoPedido) return;
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse); // Impulso instantáneo
        saltoPedido = false;
        enSuelo = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contacto in collision.contacts)
        {
            if (contacto.normal.y > 0.6f) // Superficie debajo de los pies (Regla 5)
            {
                enSuelo = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enSuelo = false;
    }
}