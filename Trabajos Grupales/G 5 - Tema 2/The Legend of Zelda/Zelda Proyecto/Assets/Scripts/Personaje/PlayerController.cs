using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;

    [Header("Espada (Semana 2)")]
    [SerializeField] private GameObject swordHitbox;
    [SerializeField] private float swordDistance = 0.8f;
    [SerializeField] private float swordDuration = 0.2f;

    [Header("Rayo de espada (Fase 3) - solo con salud máxima")]
    [SerializeField] private RayoEspada prefabRayo;
    [SerializeField] private Transform firePoint;

    [Header("Daño e invulnerabilidad")]
    [SerializeField] private float duracionInvulnerabilidad = 1.5f;
    [SerializeField] private float intervaloParpadeo = 0.1f;

    [Header("Audio y Game Juice")]
    [SerializeField] private AudioClip[] sonidosEspada; // variantes: se elige una al azar en cada ataque
    [SerializeField] private AudioClip sonidoRayo;
    [SerializeField] private AudioClip sonidoDanio;
    [SerializeField] private float shakeDuracion = 0.2f;
    [SerializeField] private float shakeMagnitud = 0.15f;

    [Header("Alerta de vida baja (estilo Zelda)")]
    [SerializeField] private AudioClip sonidoVidaBaja;
    [Range(0f, 1f)]
    [SerializeField] private float umbralVidaBaja = 0.3f;   // 0.3 = 30% de la vida máxima (3 de 10 corazones)
    [SerializeField] private float intervaloAlerta = 0.8f;   // cada cuántos segundos suena el aviso

    public Link Modelo { get; private set; }
    public bool EsInvulnerable { get; private set; }

    // Refactor: una espada rota (durabilidad 0) ya no hace daño.
    public int DanioEspada
    {
        get
        {
            var espada = Modelo.ObtenerItem(0) as EspadaMaestra;
            return (espada != null && espada.Durabilidad > 0) ? espada.DanioSagrado : 0;
        }
    }

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    private Vector2 input;
    private Vector2 facing = Vector2.down;
    private bool attacking;
    private bool muerto;
    private int ultimoSonidoEspada = -1;
    private Coroutine rutinaAlerta;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        // Modelo puro instanciado con new dentro de Awake (regla de la cátedra)
        Modelo = new Link("Link", 10);
        Modelo.Equipar(new EspadaMaestra("Espada Maestra", 100, 30), 0);
        Modelo.Equipar(new ArcoFuerza("Arco de Fuerza", 50, 3), 1);
        Modelo.SaludCambiada += OnSaludCambiada;

        if (swordHitbox != null) swordHitbox.SetActive(false);
    }

    void OnDestroy()
    {
        if (Modelo != null) Modelo.SaludCambiada -= OnSaludCambiada;
    }

    // Se ejecuta cada vez que cambia la vida en el modelo: prende o apaga la alerta
    private void OnSaludCambiada(int actual, int maximo)
    {
        bool vidaBaja = actual > 0 && (float)actual / maximo <= umbralVidaBaja;

        if (vidaBaja && rutinaAlerta == null)
        {
            rutinaAlerta = StartCoroutine(RutinaAlertaVidaBaja());
        }
        else if (!vidaBaja && rutinaAlerta != null)
        {
            StopCoroutine(rutinaAlerta);
            rutinaAlerta = null;
        }
    }

    // Repite el aviso mientras la vida siga baja, sin bloquear el juego
    private IEnumerator RutinaAlertaVidaBaja()
    {
        while (true)
        {
            if (audioSource != null && sonidoVidaBaja != null) audioSource.PlayOneShot(sonidoVidaBaja);
            yield return new WaitForSeconds(intervaloAlerta);
        }
    }

    void Update()
    {
        if (muerto || attacking)
        {
            input = Vector2.zero;
            return;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.magnitude > 0)
        {
            input.Normalize();
            facing = input;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

    private IEnumerator Attack()
    {
        attacking = true;
        Modelo.AccionarItem(0);

        // Sonido de espada en cada ataque (con o sin rayo)
        ReproducirSonidoEspada();

        if (swordHitbox != null)
        {
            swordHitbox.transform.localPosition = facing * swordDistance;

            // Refactor: antes la hitbox solo se movía y quedaba siempre horizontal
            float angulo = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
            swordHitbox.transform.localRotation = Quaternion.Euler(0f, 0f, angulo);

            swordHitbox.SetActive(true);
        }

        // Mecánica clásica de Zelda: el rayo de espada solo sale con todos los corazones
        if (Modelo.TieneSaludMaxima && DanioEspada > 0)
        {
            DispararRayo();
        }

        yield return new WaitForSeconds(swordDuration);

        if (swordHitbox != null) swordHitbox.SetActive(false);
        attacking = false;
    }

    // Elige una variante al azar, sin repetir la misma dos veces seguidas
    private void ReproducirSonidoEspada()
    {
        if (audioSource == null || sonidosEspada == null || sonidosEspada.Length == 0) return;

        int indice = UnityEngine.Random.Range(0, sonidosEspada.Length);
        if (sonidosEspada.Length > 1 && indice == ultimoSonidoEspada)
            indice = (indice + 1) % sonidosEspada.Length;

        ultimoSonidoEspada = indice;
        if (sonidosEspada[indice] != null) audioSource.PlayOneShot(sonidosEspada[indice]);
    }

    private void DispararRayo()
    {
        if (prefabRayo == null) return;

        // El firePoint se ubica hacia donde mira Link, igual que la hitbox de la espada
        if (firePoint != null) firePoint.localPosition = facing * swordDistance;
        Vector3 origen = firePoint != null
            ? firePoint.position
            : transform.position + (Vector3)(facing * swordDistance);

        RayoEspada rayo = Instantiate(prefabRayo, origen, Quaternion.identity);
        rayo.Lanzar(facing, DanioEspada);

        if (audioSource != null && sonidoRayo != null) audioSource.PlayOneShot(sonidoRayo);
    }

    // Lo llaman los enemigos al tocar a Link
    public void RecibirGolpe(int cantidad)
    {
        if (muerto || EsInvulnerable || cantidad <= 0) return;

        Modelo.RecibirDanio(cantidad);

        if (audioSource != null && sonidoDanio != null) audioSource.PlayOneShot(sonidoDanio);
        if (CameraShake.Instance != null) CameraShake.Instance.Sacudir(shakeDuracion, shakeMagnitud);

        if (!Modelo.EstaVivo)
        {
            Morir();
            return;
        }

        StartCoroutine(RutinaInvulnerabilidad());
    }

    // Invulnerabilidad no bloqueante con parpadeo del SpriteRenderer
    private IEnumerator RutinaInvulnerabilidad()
    {
        EsInvulnerable = true;
        float tiempoLimite = Time.time + duracionInvulnerabilidad;

        while (Time.time < tiempoLimite)
        {
            CambiarAlpha(0.2f);
            yield return new WaitForSeconds(intervaloParpadeo);
            CambiarAlpha(1f);
            yield return new WaitForSeconds(intervaloParpadeo);
        }

        CambiarAlpha(1f);
        EsInvulnerable = false;
    }

    private void CambiarAlpha(float alpha)
    {
        if (sr == null) return;
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }

    private void Morir()
    {
        muerto = true;
        StopAllCoroutines();   // también corta la alerta de vida baja
        rutinaAlerta = null;
        attacking = false;
        EsInvulnerable = false;
        CambiarAlpha(1f);
        if (swordHitbox != null) swordHitbox.SetActive(false);

        input = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        if (GameManager.Instance != null) GameManager.Instance.GameOver();
    }
}
