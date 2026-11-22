using UnityEngine;

// Puente MonoBehaviour <-> EnemigoModelo (clase pura creada con new en Awake).
// Mantiene la misma API pública que antes (RecibirDanio, Vida, EstaMuerto, Nombre)
// para no romper SwordHitbox ni nada de la Semana 2.
public class Enemigo : MonoBehaviour
{
    [SerializeField] private string nombreEnemigo = "Octorok";
    [SerializeField] private int vidaInicial = 30;
    [SerializeField] private int danioContacto = 1;
    [SerializeField] private AudioClip sonidoMuerte;

    public EnemigoModelo Modelo { get; private set; }

    public string Nombre => Modelo.Nombre;
    public int Vida => Modelo.Vida;
    public bool EstaMuerto => Modelo.EstaMuerto;

    private void Awake()
    {
        Modelo = new EnemigoModelo(nombreEnemigo, vidaInicial, danioContacto);
    }

    private void Start()
    {
        if (GameManager.Instance != null) GameManager.Instance.RegistrarEnemigo(this);
    }

    public void RecibirDanio(int cantidad)
    {
        if (EstaMuerto) return;

        Modelo.RecibirDanio(cantidad);
        Debug.Log($"{Nombre} recibió {cantidad} de daño. Vida: {Vida}/{Modelo.VidaMaxima}");

        if (EstaMuerto) Morir();
    }

    private void Morir()
    {
        GameManager.Sonar(sonidoMuerte);
        if (GameManager.Instance != null) GameManager.Instance.QuitarEnemigo(this);
        Destroy(gameObject);
    }

    // Daño por contacto a Link. La invulnerabilidad de Link evita golpes dobles.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (EstaMuerto) return;

        PlayerController player = collision.collider.GetComponentInParent<PlayerController>();
        if (player != null) player.RecibirGolpe(Modelo.DanioContacto);
    }
}
