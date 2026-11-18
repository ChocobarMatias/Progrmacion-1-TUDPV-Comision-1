using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Componentes de UI")]
    [SerializeField] private Slider sliderSalud;
    [SerializeField] private Slider sliderDurabilidad;
    [SerializeField] private TextMeshProUGUI textInventario;
    [SerializeField] private GameObject panelDerrota;

    public void ActualizarSalud(int actual, int maxima)
    {
        if (sliderSalud != null)
        {
            sliderSalud.maxValue = maxima;
            sliderSalud.value = actual;
        }
    }

    public void ActualizarDurabilidad(int actual, int maxima)
    {
        if (sliderDurabilidad != null)
        {
            sliderDurabilidad.maxValue = maxima;
            sliderDurabilidad.value = actual;
        }
    }

    public void ActualizarInventarioTexto(string texto)
    {
        if (textInventario != null)
        {
            textInventario.text = texto;
        }
    }

    public void MostrarDerrota(bool mostrar)
    {
        if (panelDerrota != null)
        {
            panelDerrota.SetActive(mostrar);
        }
    }
}
