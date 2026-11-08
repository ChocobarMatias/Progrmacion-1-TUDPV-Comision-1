using UnityEngine;

public class Disparar : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;

    private PlayerController jugador;
    private DoomSimple.Jugador jugadorLogico;

    void Awake()
    {
        jugador = GetComponent<PlayerController>();

        if (jugador != null)
        {
            jugadorLogico = jugador.JugadorLogico;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (jugador != null &&
                jugador.IntentarUsarMunicion(1))
            {
                Instantiate(
                    prefabBala,
                    puntoDisparo.position,
                    puntoDisparo.rotation 
                );
            }
        }
    }
}