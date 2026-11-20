using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerPacman : MonoBehaviour
{
    public static GameManagerPacman Instancia { get; private set; }

    [Header("Referencias del Jugador")]
    [SerializeField] private PacManController jugador;

    [Header("Configuración de Prefabs y Mapa")]
    [SerializeField] private GameObject pacDotPrefab;
    [SerializeField] private Transform[] posicionesPacDots;

    [Header("Referencias de UI")]
    [SerializeField] private TextMeshProUGUI textoPuntaje;
    [SerializeField] private TextMeshProUGUI textoVidas;
    [SerializeField] private GameObject cartelVictoria;
    [SerializeField] private GameObject cartelDerrota;

    private List<GameObject> listaPacDots = new List<GameObject>();
    private int puntajeActual = 0;
    private bool juegoTerminado = false;


    private void Awake()
    {
        if (Instancia == null) 
            Instancia = this;

        else 
            Destroy(gameObject);
    }

    void Start()
    {
        cartelVictoria.SetActive(false);
        cartelDerrota.SetActive(false);

        GameObject[] puntos = GameObject.FindGameObjectsWithTag("Punto");
        listaPacDots.AddRange(puntos);

        ActualizarInterfazVisual();
    }

    private void GenerarCuadriculaPuntos()
    {
        if (posicionesPacDots == null || posicionesPacDots.Length == 0) return;

        foreach (Transform punto in posicionesPacDots)
        {
            GameObject nuevoDot = Instantiate(pacDotPrefab, punto.position, Quaternion.identity);
            listaPacDots.Add(nuevoDot);
        }
    }

    public void RegistrarPuntoComido(GameObject dotComido)
    {
        if (juegoTerminado) return;

        if (listaPacDots.Contains(dotComido))
        {
            listaPacDots.Remove(dotComido); // Se elimina de la lista dinámica
            Destroy(dotComido); // Se limpia de la memoria RAM

            puntajeActual += 10; // Suma puntos
            ActualizarInterfazVisual();

            // Condición de Victoria: Si la lista se queda vacía
            if (listaPacDots.Count == 0)
                GanarPartida();
        }
    }
    public void ActualizarInterfazVisual()
    {
        if (jugador == null || jugador.ModeloLogico == null) return;

        textoPuntaje.text = "Puntaje: " + jugador.ModeloLogico.Puntuacion.ToString();
        textoVidas.text = "Vidas: " + jugador.ModeloLogico.Vidas.ToString();
    }

    private void ActualizarTextoUI() => textoPuntaje.text = "Puntaje" + puntajeActual.ToString();

    private void GanarPartida()
    {   
        juegoTerminado = true;
        cartelVictoria.SetActive(true);
    }

    public void ActivarDerrota()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        cartelDerrota.SetActive(true);

        // Cumplimos con el Paso 7: Reiniciar tras 2 segundos con Corrutina
        StartCoroutine(RutinaReiniciarNivel());
    }

    private IEnumerator RutinaReiniciarNivel()
    {
        yield return new WaitForSeconds(2f);
        // Sintaxis estricta moderna pedida en el PDF
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
