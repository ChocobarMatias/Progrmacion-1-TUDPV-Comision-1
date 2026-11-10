using UnityEngine;


public class SelectorArmasVisual : MonoBehaviour
{
    [Header("Referencias a los SpriteRenderers de las armas hijas")]
    public SpriteRenderer renderMotosierra;
    public SpriteRenderer renderPistola;
    public SpriteRenderer renderEscopeta;
    public SpriteRenderer renderPlasma; // O SuperShotgun si corresponde

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("No se encontró el PlayerController en este objeto.");
        }
    }

    void Update()
    {
        if (playerController == null || playerController.JugadorLogico == null)
            return;

        // Obtenemos el nombre del arma equipada actualmente en la lógica
        string nombreArma = playerController.JugadorLogico.ArmaEquipada.Nombre;

        // Activamos o desactivamos el SpriteRenderer según el arma activa
        ActualizarVisibilidadArmas(nombreArma);
    }

    void ActualizarVisibilidadArmas(string armaActiva)
    {
        // Apagamos o encendemos cada render según corresponda
        if (renderMotosierra != null) renderMotosierra.enabled = (armaActiva == "Motosierra");
        if (renderPistola != null) renderPistola.enabled = (armaActiva == "Pistola");

        // La escopeta y la super shotgun pueden compartir el sprite o tener el suyo
        if (renderEscopeta != null) renderEscopeta.enabled = (armaActiva == "Escopeta" || armaActiva == "SuperShotgun");

        if (renderPlasma != null) renderPlasma.enabled = (armaActiva == "BFG9000");
    }
}
