using System.Collections;
using UnityEngine;

// Va en la Main Camera. Sacude la posición local por unos instantes y la restaura.
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private Vector3 posicionOriginal;
    private Coroutine rutinaActual;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void Sacudir(float duracion, float magnitud)
    {
        if (rutinaActual != null)
        {
            StopCoroutine(rutinaActual);
            transform.localPosition = posicionOriginal;
        }
        rutinaActual = StartCoroutine(RutinaSacudida(duracion, magnitud));
    }

    private IEnumerator RutinaSacudida(float duracion, float magnitud)
    {
        posicionOriginal = transform.localPosition;
        float transcurrido = 0f;

        while (transcurrido < duracion)
        {
            Vector2 desplazamiento = Random.insideUnitCircle * magnitud;
            transform.localPosition = posicionOriginal + (Vector3)desplazamiento;
            transcurrido += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicionOriginal;
        rutinaActual = null;
    }
}
