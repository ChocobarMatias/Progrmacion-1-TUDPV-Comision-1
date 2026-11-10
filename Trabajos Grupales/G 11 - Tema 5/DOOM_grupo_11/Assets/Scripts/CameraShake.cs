using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Método público que llama el jugador cuando recibe daño
    public IEnumerator Sacudir(float duracion, float magnitud)
    {
        Vector3 posicionOriginal = transform.localPosition;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            float x = Random.Range(-1f, 1f) * magnitud;
            float y = Random.Range(-1f, 1f) * magnitud;

            transform.localPosition = new Vector3(posicionOriginal.x + x, posicionOriginal.y + y, posicionOriginal.z);

            tiempoTranscurrido += Time.deltaTime;

            yield return null; // Espera al siguiente fotograma
        }

        transform.localPosition = posicionOriginal; // Restaura la posición original de la cámara
    }
}