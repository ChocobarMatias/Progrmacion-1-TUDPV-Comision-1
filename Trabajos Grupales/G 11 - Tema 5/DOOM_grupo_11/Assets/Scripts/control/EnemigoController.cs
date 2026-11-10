using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    private DoomSimple.Zombie zombie;

    void Awake()
    {
        // Inicializamos la lógica pura del zombie en C#
        zombie = new DoomSimple.Zombie(0, 0);
    }

    public void RecibirDanio(int cantidad)
    {
        zombie.RecibirDanio(cantidad);

        Debug.Log("Vida del zombie: " + zombie.Vida);

        if (!zombie.EstaVivo())
        {
            Debug.Log("Zombie eliminado");
            Destroy(gameObject);
        }
    }

    // Se activa cuando el jugador entra al área de rango (Trigger) del enemigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController jugador = collision.GetComponentInParent<PlayerController>();

            if (jugador != null)
            {
                // El zombie usa su método Atacar definido en las clases lógicas
                string mensajeAtaque = zombie.Atacar(jugador.JugadorLogico);

                // Aplicamos el daño visual/corrutina en el PlayerController
                jugador.RecibirDanioJugador(0);

                Debug.Log(mensajeAtaque + " | Vida actual del jugador: " + jugador.JugadorLogico.Vida);
            }
        }
    }
}