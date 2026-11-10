using UnityEngine;
using Pac_Man_Main;
using UnityEngine.SceneManagement;
using System.Collections;
public class PacManController : MonoBehaviour
{
    private Pac_Man_Main.PacMan modeloLogico;

    //Colocamos variable de velocidad y la variable de vida privada, pero con el "SerializeField" se puede ver desde el inspector
    [SerializeField] private float velocidad = 100f;
    [SerializeField] private int vida = 3;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float rayDistance = 0.5f;
    // Variable para el SpriteRenderer del Pac-Man
    private SpriteRenderer sr;
    private bool esInvulnerable = false;

    //Al tocar WASD el player cambia a esa direccion
    private Vector2 currentDirection = Vector2.zero;
    private Vector2 nextDirection = Vector2.zero;
    private Rigidbody2D rb;

    void Start()
    {
        //Con esto agarramos el componente "Rigidbody2D" y el componente "SpriteRenderer" del objeto al que este script esta atado
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        modeloLogico = new Pac_Man_Main.PacMan("Pac-Man");
    }

    //En el void update se encuntra todos los comandos que vinculan una tecla específica del teclado a la direccion del player
    //Importante tener configurado el Input Manager a "Both"
    void Update()
    {
        Vector2 inputDir = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputDir = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputDir = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputDir = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputDir = Vector2.right;
        }

        // Si presionamos una tecla, guardamos la intención en nextDirection
        if (inputDir != Vector2.zero)
        {
            nextDirection = inputDir;
        }

        // Si hay una dirección deseada, chequeamos con Raycast si el camino está libre antes de doblar
        if (nextDirection != Vector2.zero)
        {
            RaycastHit2D hitNext = Physics2D.Raycast(transform.position, nextDirection, rayDistance, wallLayer);
            if (hitNext.collider == null)
            {
                currentDirection = nextDirection; // ¡Camino libre para doblar!
            }
        }

        // Verificamos si la dirección actual se choca con una pared de frente para detenerlo
        if (currentDirection != Vector2.zero)
        {
            RaycastHit2D hitFront = Physics2D.Raycast(transform.position, currentDirection, rayDistance, wallLayer);
            if (hitFront.collider != null)
            {
                currentDirection = Vector2.zero; // Se frena si choca de frente contra la pared
            }
        }
    }

    //Mueve al player a la velocidad fija y en la direccion que esta mirando
    //El Time.deltaTime evita que la velocidad cambie de acuerdo al rendimiento de otros disposistivos
    private void FixedUpdate()
    {
        rb.linearVelocity = currentDirection * velocidad * Time.deltaTime;
    }

    //public void ReiniciarEscena()
    {
        // Recarga la escena actual desde cero
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void RecibirGolpe()
    {
        if (esInvulnerable) return;

        modeloLogico.PerderVida();

        GameManagerPacman.Instancia.ActualizarInterfazVisual();

        if (modeloLogico.Vidas <= 0)
        {
            Debug.Log("¡Game Over!");
            currentDirection = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
            this.enabled = false;

            GameManagerPacman.Instancia.ActivarDerrota();
            return;
        }

        StartCoroutine(RutinaInvulnerabilidad(1.5f));
    }

    private IEnumerator RutinaInvulnerabilidad(float duracionTotal)
    {
        esInvulnerable = true;
        float tiempoLimite = Time.time + duracionTotal;

        while (Time.time < tiempoLimite)
        {
            sr.color = new Color(1f, 1f, 1f, 0.2f);
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        sr.color = Color.white;
        esInvulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Si choca contra un punto / pellet
        if (otro.CompareTag("Punto"))
        {
            // Llamamos al método de tu clase lógica de C# (le sumamos 10 puntos por ejemplo)
            modeloLogico.ComerPunto(10);
            GameManagerPacman.Instancia.RegistrarPuntoComido(otro.gameObject);
        }

        // Si choca contra un fantasma
        else if (otro.CompareTag("Fantasma"))
        {
            FantasmaController fantasma = otro.GetComponent<FantasmaController>();

            if (fantasma != null && fantasma.EsVulnerable)
            {
                modeloLogico.Puntuacion += 200;
                GameManagerPacman.Instancia.ActualizarInterfazVisual();
                fantasma.Reaparecer();
            }
            else
                RecibirGolpe();
        }
    }
}

