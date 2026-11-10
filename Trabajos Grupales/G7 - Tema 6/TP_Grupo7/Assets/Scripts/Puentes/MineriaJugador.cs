using System.Collections;
using UnityEngine;
using TP_Grupo6; // Namespace del modelo de minería heredado

public class MineriaJugador : MonoBehaviour
{
    [Header("Configuración Minería")]
    [SerializeField] private float alcance = 3f;
    [SerializeField] private float tiempoEntreGolpes = 0.4f;
    [SerializeField] private GameObject prefabDrop;
    [SerializeField] private UIController uiController;

    [Header("Audio y Juiciness")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoPicar;
    [SerializeField] private AudioClip sonidoRomper;

    private Herramienta[] herramientas;
    private int actual;
    private Camera camara;
    private Coroutine corrutinaMinado;
    private BloqueJuego bloqueObjetivoActual;

    private Herramienta HerramientaActual => herramientas[actual];

    private void Awake()
    {
        camara = Camera.main;
        herramientas = new Herramienta[] { new PicoHierro(), new PicoDiamante() };
    }

    private void Update()
    {
        // Cambio de herramientas
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equipar(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Equipar(1);

        // Minado continuo con corrutina (Tema 6 Consigna)
        if (Input.GetMouseButtonDown(0))
        {
            IniciarMinado();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DetenerMinado();
        }
    }

    private void Equipar(int indice)
    {
        actual = indice;
        ActualizarUIHerramienta();
    }

    private void IniciarMinado()
    {
        if (corrutinaMinado != null) StopCoroutine(corrutinaMinado);
        corrutinaMinado = StartCoroutine(RutinaMinadoContinuo());
    }

    private void DetenerMinado()
    {
        if (corrutinaMinado != null)
        {
            StopCoroutine(corrutinaMinado);
            corrutinaMinado = null;
        }
        bloqueObjetivoActual = null;
    }

    private IEnumerator RutinaMinadoContinuo()
    {
        while (true)
        {
            PicarUnaVez();
            yield return new WaitForSeconds(tiempoEntreGolpes);
        }
    }

    private void PicarUnaVez()
    {
        Vector2 origen = transform.position;
        Vector2 destino = camara.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = (destino - origen).normalized;

        foreach (RaycastHit2D impacto in Physics2D.RaycastAll(origen, direccion, alcance))
        {
            if (impacto.collider.gameObject == gameObject) continue;
            if (impacto.collider.TryGetComponent(out DropBloque _)) continue;

            if (impacto.collider.TryGetComponent(out BloqueJuego bloqueJuego))
            {
                PicarBloque(bloqueJuego);
            }
            return; // Solo impacta el primer bloque bloqueante
        }
    }

    private void PicarBloque(BloqueJuego bloqueJuego)
    {
        Bloque bloque = bloqueJuego.Modelo;
        int durezaAntes = bloque.Dureza;
        HerramientaActual.PicarBloque(bloque);
        bloqueJuego.ActualizarVisual(); // Avisa al bloque que recibió daño
        ActualizarUIHerramienta();

        HerramientaActual.PicarBloque(bloque);
        ActualizarUIHerramienta();

        // Audio y Feedback al golpear
        if (bloque.Dureza < durezaAntes)
        {
            if (audioSource != null && sonidoPicar != null)
                audioSource.PlayOneShot(sonidoPicar);

            if (CameraShake.Instancia != null)
                CameraShake.Instancia.Sacudir(0.08f, 0.08f);
        }

        // Si se rompe
        if (bloque.EstaRoto)
        {
            if (audioSource != null && sonidoRomper != null)
                audioSource.PlayOneShot(sonidoRomper);

            if (CameraShake.Instancia != null)
                CameraShake.Instancia.Sacudir(0.2f, 0.25f);

            SoltarDrop(bloqueJuego);
            Destroy(bloqueJuego.gameObject);
            DetenerMinado();
        }
    }

    private void SoltarDrop(BloqueJuego bloqueJuego)
    {
        if (prefabDrop == null) return;

        // Instanciación dinámica del Prefab (Paso 3 y Consigna Tema 6)
        GameObject drop = Instantiate(prefabDrop, bloqueJuego.transform.position, Quaternion.identity);

        if (drop.TryGetComponent(out DropBloque scriptDrop))
        {
            scriptDrop.Configurar(bloqueJuego.Modelo.Tipo);
        }
    }

    private void ActualizarUIHerramienta()
    {
        if (uiController != null)
        {
            // Muestra durabilidad actual/maxima en el Slider
            uiController.ActualizarDurabilidad(HerramientaActual.Durabilidad, HerramientaActual.DurabilidadMaxima);
        }
    }
}