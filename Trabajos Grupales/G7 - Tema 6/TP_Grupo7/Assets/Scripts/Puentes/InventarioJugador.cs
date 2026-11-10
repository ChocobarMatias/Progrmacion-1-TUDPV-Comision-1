using UnityEngine;
using TP_Grupo6;

public class InventarioJugador : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoRecoger;

    public Inventario Modelo { get; private set; }

    private void Awake()
    {
        Modelo = new Inventario();
    }

    public void Recoger(string item)
    {
        Modelo.Agregar(item);

        if (audioSource != null && sonidoRecoger != null)
            audioSource.PlayOneShot(sonidoRecoger);

        RefrescarUI();
    }

    private void RefrescarUI()
    {
        if (uiController == null) return;

        string resumen = $"\nPiedra: {Modelo.Cantidad("Piedra")}\nDiamante: {Modelo.Cantidad("Diamante")}\nObsidiana: {Modelo.Cantidad("Obsidiana")}";
        uiController.ActualizarInventarioTexto(resumen);
    }
}