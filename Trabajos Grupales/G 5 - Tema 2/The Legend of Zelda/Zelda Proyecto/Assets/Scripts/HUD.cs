using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UI del Canvas: Slider de vida sincronizado con Link.Corazones + texto TextMeshProUGUI.
public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Slider sliderSalud;
    [SerializeField] private TextMeshProUGUI textoEstado;

    private Link modelo;

    private void Start()
    {
        if (player == null) player = FindFirstObjectByType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("HUD: no se encontró el PlayerController en la escena.");
            return;
        }

        modelo = player.Modelo;
        modelo.SaludCambiada += OnSaludCambiada;
        if (GameManager.Instance != null) GameManager.Instance.EnemigosCambiaron += OnEnemigosCambiaron;

        if (sliderSalud != null)
        {
            sliderSalud.minValue = 0;
            sliderSalud.maxValue = modelo.MaxCorazones;
            sliderSalud.wholeNumbers = true;
            sliderSalud.interactable = false;
        }

        OnSaludCambiada(modelo.Corazones, modelo.MaxCorazones);
    }

    private void OnDestroy()
    {
        if (modelo != null) modelo.SaludCambiada -= OnSaludCambiada;
        if (GameManager.Instance != null) GameManager.Instance.EnemigosCambiaron -= OnEnemigosCambiaron;
    }

    private void OnSaludCambiada(int actual, int maximo)
    {
        if (sliderSalud != null) sliderSalud.value = actual;
        ActualizarTexto();
    }

    private void OnEnemigosCambiaron(int cantidad)
    {
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (textoEstado == null || modelo == null) return;

        int enemigos = GameManager.Instance != null ? GameManager.Instance.EnemigosRestantes : 0;
        string rayo = modelo.TieneSaludMaxima ? "<color=#4CFF4C>LISTO</color>" : "<color=#FF5050>BLOQUEADO</color>";

        textoEstado.text = $"Vida: {modelo.Corazones}/{modelo.MaxCorazones}   Enemigos: {enemigos}\nRayo de espada: {rayo}";
    }
}
