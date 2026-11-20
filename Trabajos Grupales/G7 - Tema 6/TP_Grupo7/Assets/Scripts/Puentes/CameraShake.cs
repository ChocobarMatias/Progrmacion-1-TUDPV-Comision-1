using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instancia { get; private set; }

    private Vector3 posicionOriginal;

    private void Awake()
    {
        if (Instancia == null) Instancia = this;
        else Destroy(gameObject);
    }

    public void Sacudir(float duracion = 0.15f, float intensidad = 0.2f)
    {
        StopAllCoroutines();
        StartCoroutine(RutinaSacudida(duracion, intensidad));
    }

    private IEnumerator RutinaSacudida(float duracion, float intensidad)
    {
        posicionOriginal = transform.localPosition;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float x = Random.Range(-1f, 1f) * intensidad;
            float y = Random.Range(-1f, 1f) * intensidad;

            transform.localPosition = posicionOriginal + new Vector3(x, y, 0f);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicionOriginal;
    }
}
