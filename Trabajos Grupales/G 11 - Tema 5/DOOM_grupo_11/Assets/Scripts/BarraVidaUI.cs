using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con componentes de UI

public class BarraVidaUI : MonoBehaviour
{
    [Header("Referencias")]
    public Slider sliderVida;
    public PlayerController playerController;

    void Start()
    {
        // Si no asignaste el player en el inspector, lo buscamos solo
        if (playerController == null)
        {
            playerController = Object.FindFirstObjectByType<PlayerController>();
        }

        if (sliderVida != null)
        {
            sliderVida.maxValue = 100; // Vida máxima de Doomguy
            sliderVida.value = 100;
        }
    }

    void Update()
    {
        if (playerController != null && playerController.JugadorLogico != null && sliderVida != null)
        {
            // Actualizamos el valor del slider con la vida actual de la clase lógica
            sliderVida.value = playerController.JugadorLogico.Vida;
        }
    }
}
