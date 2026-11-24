using System.Collections;
using UnityEngine;

// Tema 9 - Hollow Knight 2D
// Pogo Jump: estando en el aire, mantener ABAJO + ATACAR sobre un enemigo o pincho.
// Al conectar el golpe: se instancian chispas (Prefab), suena el aguijón
// y el personaje sale disparado hacia arriba.
[RequireComponent(typeof(Rigidbody2D))]
public class PogoAtaque : MonoBehaviour
{
    [Header("Detección del golpe hacia abajo")]
    [SerializeField] private Transform puntoAtaqueAbajo;   // Objeto hijo ubicado debajo de los pies
    [SerializeField] private float radioAtaque = 0.5f;
    [SerializeField] private LayerMask capasPogo;           // Capa de enemigos y pinchos

    [Header("Rebote")]
    [SerializeField] private float fuerzaRebote = 14f;
    [SerializeField] private int danio = 1;
    [SerializeField] private float tiempoEntreAtaques = 0.25f;

    [Header("Game Juice")]
    [SerializeField] private GameObject prefabChispas;
    [SerializeField] private AudioClip sonidoAguijon;

    [Header("Controles")]
    [SerializeField] private KeyCode teclaAbajo = KeyCode.S;
    [SerializeField] private KeyCode teclaAbajoAlt = KeyCode.DownArrow;
    [SerializeField] private KeyCode teclaAtaque = KeyCode.X;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool puedeAtacar = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        bool apuntaAbajo = Input.GetKey(teclaAbajo) || Input.GetKey(teclaAbajoAlt);

        if (apuntaAbajo && Input.GetKeyDown(teclaAtaque) && puedeAtacar)
        {
            AtacarHaciaAbajo();
        }
    }

    private void AtacarHaciaAbajo()
    {
        StartCoroutine(RutinaCooldown());

        // El sonido del aguijón suena siempre al cortar, conecte o no
        if (audioSource != null && sonidoAguijon != null)
        {
            audioSource.PlayOneShot(sonidoAguijon);
        }

        Collider2D golpeado = Physics2D.OverlapCircle(puntoAtaqueAbajo.position, radioAtaque, capasPogo);
        if (golpeado == null) return;

        // 1) Chispas en el punto de contacto (Prefab temporal)
        if (prefabChispas != null)
        {
            Vector2 puntoContacto = golpeado.ClosestPoint(puntoAtaqueAbajo.position);
            Instantiate(prefabChispas, puntoContacto, Quaternion.identity);
        }

        // 2) Rebote hacia arriba: se reemplaza la velocidad vertical (siempre linearVelocity, nunca velocity)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaRebote);

        // 3) Si lo golpeado puede recibir daño (un enemigo), se le aplica. Los pinchos no lo implementan.
        if (golpeado.TryGetComponent(out IGolpeable objetivo))
        {
            objetivo.RecibirGolpe(danio);
        }
    }

    // Corrutina: evita spamear el ataque y rebotar varias veces en el mismo frame
    private IEnumerator RutinaCooldown()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    // Dibuja el área del golpe en la Scene para poder acomodarla
    private void OnDrawGizmosSelected()
    {
        if (puntoAtaqueAbajo == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(puntoAtaqueAbajo.position, radioAtaque);
    }
}

// Cualquier enemigo que quiera recibir daño del pogo implementa esta interfaz:
// public class Enemigo : MonoBehaviour, IGolpeable { public void RecibirGolpe(int d) { ... } }
public interface IGolpeable
{
    void RecibirGolpe(int cantidad);
}
