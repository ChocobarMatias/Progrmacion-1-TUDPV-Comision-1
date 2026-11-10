using UnityEngine;
using TP_Grupo6;

[RequireComponent(typeof(SpriteRenderer))]
public class BloqueJuego : MonoBehaviour
{
    [SerializeField] private string tipo = "Piedra";
    [SerializeField] private int dureza = 50;

    private int durezaMaxima;
    private Transform grietas;

    public Bloque Modelo { get; private set; } // Otros scripts lo leen, nadie lo reemplaza

    private void Awake()
    {
        Modelo = new Bloque(tipo, dureza); // Crea el modelo con los datos del Inspector
        durezaMaxima = Modelo.Dureza; // Referencia para calcular el progreso
        CrearGrietas();
        Debug.Log($"[BloqueJuego] {name} creado: {Modelo.Tipo}, dureza {Modelo.Dureza}");
    }

    public void ActualizarVisual()
    {
        float progreso = 1f - (float)Modelo.Dureza / durezaMaxima; // 0 = intacto, 1 = roto
        grietas.localScale = Vector3.one * progreso; // La capa oscura crece con el daño
        Debug.Log($"[BloqueJuego] {name}: dureza {Modelo.Dureza}/{durezaMaxima} | rotura {Mathf.RoundToInt(progreso * 100)}%");
    }

    private void CrearGrietas()
    {
        SpriteRenderer baseSr = GetComponent<SpriteRenderer>();

        GameObject capa = new GameObject("Grietas");
        capa.transform.SetParent(transform, false); // Hijo centrado en el bloque
        capa.transform.localScale = Vector3.zero; // Invisible hasta el primer golpe

        SpriteRenderer sr = capa.AddComponent<SpriteRenderer>();
        sr.sprite = baseSr.sprite; // Misma forma que el bloque
        sr.color = new Color(0f, 0f, 0f, 0.6f); // Negro semitransparente
        sr.sortingLayerID = baseSr.sortingLayerID;
        sr.sortingOrder = baseSr.sortingOrder + 1; // Se dibuja encima

        grietas = capa.transform;
    }
}