using UnityEngine;

// Ejercicio 13: Zona de Daño Continuo / Lava
// Se adjunta al objeto LavaZone (BoxCollider2D con Is Trigger activado).
public class DamageZone : MonoBehaviour
{
    [SerializeField] private float damageInterval = 1f;   // segundos entre cada daño
    [SerializeField] private int damageAmount = 10;       // daño por "tick" (int, igual que GuerreroLogico)

    private float timer = 0f;   // tiempo acumulado dentro de la zona

    // Se ejecuta en cada frame de física mientras el jugador siga dentro del trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        timer += Time.deltaTime;

        if (timer >= damageInterval)
        {
            Debug.Log($"[Lava] {other.name} recibió {damageAmount} de daño");

            // Si el jugador es un Guerrero, se le aplica el daño real (escudo → vida)
            GuerreroController guerrero = other.GetComponent<GuerreroController>();
            if (guerrero != null)
                guerrero.RecibirDanio(damageAmount, "Lava");

            timer = 0f;   // interrumpe la acumulación y arranca un nuevo intervalo
        }
    }

    // Al salir, se reinicia el temporizador para que el próximo ingreso empiece de cero
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            timer = 0f;
    }
}
