using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TP_Grupo7;

public class JugadorSaludPuente : MonoBehaviour
{
    [SerializeField] private int vidaInicial = 100;
    [SerializeField] private UIController uiController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoDanio;

    private SpriteRenderer sr;
    private bool esInvulnerable = false;

    public SaludJugador Modelo { get; private set; }

    private void Awake()
    {
        Modelo = new SaludJugador(vidaInicial);
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (uiController != null)
        {
            uiController.ActualizarSalud(Modelo.VidaActual, Modelo.VidaMaxima);
            uiController.MostrarDerrota(false);
        }
    }

    public void RecibirGolpe(int cantidadDanio)
    {
        if (esInvulnerable || Modelo.EstaMuerto) return;

        Modelo.RecibirDanio(cantidadDanio);

        if (uiController != null)
            uiController.ActualizarSalud(Modelo.VidaActual, Modelo.VidaMaxima);

        if (audioSource != null && sonidoDanio != null)
            audioSource.PlayOneShot(sonidoDanio);

        if (CameraShake.Instancia != null)
            CameraShake.Instancia.Sacudir(0.2f, 0.3f);

        if (Modelo.EstaMuerto)
        {
            StartCoroutine(RutinaMuerte());
        }
        else
        {
            StartCoroutine(RutinaInvulnerabilidad(1.5f));
        }
    }

    private IEnumerator RutinaInvulnerabilidad(float duracionTotal)
    {
        esInvulnerable = true;
        float tiempoLimite = Time.time + duracionTotal;

        while (Time.time < tiempoLimite)
        {
            sr.color = new Color(1f, 1f, 1f, 0.2f); // Casi transparente
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white; // Opaco
            yield return new WaitForSeconds(0.1f);
        }

        sr.color = Color.white;
        esInvulnerable = false;
    }

    private IEnumerator RutinaMuerte()
    {
        if (uiController != null)
            uiController.MostrarDerrota(true);

        // Desactivar físicas o control si aplica
        enabled = false;

        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}