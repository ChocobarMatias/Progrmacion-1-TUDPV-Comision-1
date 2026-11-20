
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f; // movimiento horizontal
    public float jumpForce = 12f; // salto

    [Header("Detección de suelo")]
    public Transform groundCheck; // deteccion del suelo
    public LayerMask groundLayer;
    public float groundRadius = 0.15f;

    private Rigidbody2D rb;
    private float move; // declarar movimiento
    private bool jumpRequested; // solicitar salto

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        move = 0f;

        if (Keyboard.current.aKey.isPressed) // izquierda
            move = -1f;

        if (Keyboard.current.dKey.isPressed) // derecha
            move = 1f;

        bool grounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        if (Keyboard.current.spaceKey.wasPressedThisFrame
            && grounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            move * speed,
            rb.linearVelocity.y
        );

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            jumpRequested = false;
        }
    }
}
