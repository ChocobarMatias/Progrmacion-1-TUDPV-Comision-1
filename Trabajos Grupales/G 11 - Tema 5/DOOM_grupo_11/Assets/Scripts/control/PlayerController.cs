using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 3f;

    private Rigidbody2D rb;
    private Vector2 movimiento;
    private DoomSimple.Jugador jugadorLogico;


    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoDaño;

    private CameraShake cameraShake;
    public DoomSimple.Jugador JugadorLogico
    {
        get { return jugadorLogico; }
    }




    public Transform puntoDisparo;
    public Vector2 DireccionApuntado { get; private set; }

    void Awake()
    {
        // Crear primero el jugador lógico
        jugadorLogico = new DoomSimple.Jugador(1, 1);
        

        Debug.Log("Jugador lógico creado correctamente: " + jugadorLogico.ArmaEquipada.Nombre);

        // Buscar el Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Falta Rigidbody2D en el jugador.");
            enabled = false;
            return;
        }

        Debug.Log("Jugador lógico creado correctamente: "
            + jugadorLogico.ArmaEquipada.Nombre);
    }

    public void RecuperarMunicionEspecifica(DoomSimple.TipoMunicion tipo, int cantidad)
    {
        switch (tipo)
        {
            case DoomSimple.TipoMunicion.Balas:
                jugadorLogico.Municion += cantidad;
                Debug.Log("Munición de pistola recuperada: +" + cantidad);
                break;
            case DoomSimple.TipoMunicion.Cartuchos:
                jugadorLogico.Cartuchos += cantidad;
                Debug.Log("Cartuchos recuperados: +" + cantidad);
                break;
            case DoomSimple.TipoMunicion.Plasma:
                jugadorLogico.Plasma += cantidad;
                Debug.Log("Plasma recuperado: +" + cantidad);
                break;
        }
    }


    void Update()
    {

    


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(x, y).normalized;

        if (Camera.main != null)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(
                Input.mousePosition
            );

            Vector2 direccion = mouse - transform.position;

            if (direccion.sqrMagnitude > 0.01f)
                DireccionApuntado = direccion.normalized;
        }

        if (puntoDisparo != null)
        {
            float angulo = Mathf.Atan2(
                DireccionApuntado.y,
                DireccionApuntado.x
            ) * Mathf.Rad2Deg;

            puntoDisparo.rotation = Quaternion.Euler(0, 0, angulo);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movimiento * velocidad;
    }

    public void RecuperarSalud(int cantidad)
    {
        jugadorLogico.Curar(cantidad);

        Debug.Log("Salud actual: " + jugadorLogico.Vida);
    }

    public void RecuperarMunicion(int cantidad)
    {
        jugadorLogico.Municion += cantidad;

        Debug.Log("Municion actual: " + jugadorLogico.Municion);
    }

    public bool IntentarUsarMunicion(DoomSimple.Arma arma)
    {
        // Si el arma es del arsenal especial DoomDoom (SuperShotgun, BFG, Uzi, Bazooka)
        if (arma is DoomSimple.ArmaDoom armaDoom)
        {
            switch (armaDoom.TipoMunicion)
            {
                case DoomSimple.TipoMunicion.Balas:
                    if (jugadorLogico.Municion < arma.CostoMunicion)
                        return false;

                    jugadorLogico.Municion -= arma.CostoMunicion;
                    return true;

                case DoomSimple.TipoMunicion.Cartuchos:
                    if (jugadorLogico.Cartuchos < arma.CostoMunicion)
                        return false;

                    jugadorLogico.Cartuchos -= arma.CostoMunicion;
                    return true;

                case DoomSimple.TipoMunicion.Plasma:
                    if (jugadorLogico.Plasma < arma.CostoMunicion)
                        return false;

                    jugadorLogico.Plasma -= arma.CostoMunicion;
                    return true;
            }
        }
        // NUEVO: Si el arma hereda de la clase base Arma (como Pistola o Escopeta) y usa munición normal
        else if (arma.UsaMunicion)
        {
            if (jugadorLogico.Municion < arma.CostoMunicion)
                return false;

            jugadorLogico.Municion -= arma.CostoMunicion;
            return true;
        }

        return !arma.UsaMunicion;
    }


    [Header("Invulnerabilidad")]
    public float tiempoInvulnerable = 1f;
    private bool esInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // ... (tu código anterior de Start)
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (Camera.main != null)
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }
    }

    // Método que llaman los enemigos al atacar
    public void RecibirDanioJugador(int danio)
    {
        if (esInvulnerable) return;

        // Llamamos al método lógico de C#
        jugadorLogico.RecibirDanio(danio);
        Debug.Log("¡Jugador herido! Vida restante: " + jugadorLogico.Vida);

        if (audioSource != null && sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }

        if (jugadorLogico.EstaVivo())
        {
            // Disparamos la corrutina de invulnerabilidad
            StartCoroutine(RutinaInvulnerabilidad());
        }

        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Sacudir(0.15f, 0.2f)); // Duración 0.15s, intensidad 0.2
        }


        if (jugadorLogico.EstaVivo())
        {
            // Disparamos la corrutina de invulnerabilidad (¡Exigencia de cátedra!)
            StartCoroutine(RutinaInvulnerabilidad());
        }
    }

    private System.Collections.IEnumerator RutinaInvulnerabilidad()
    {
        esInvulnerable = true;

        // Efecto visual opcional: parpadeo rápido
        if (spriteRenderer != null)
        {
            for (int i = 0; i < 5; i++)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f); // Semi transparente
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            yield return new WaitForSeconds(tiempoInvulnerable);
        }

        esInvulnerable = false;
    }


}