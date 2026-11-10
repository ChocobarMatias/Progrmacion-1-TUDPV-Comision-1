using UnityEngine;

public class RecuperarMunicion : MonoBehaviour
{
    public int cantidadMunicion = 20;

    [Header("Tipo de Munición a Recargar")]
    public DoomSimple.TipoMunicion tipoMunicion = DoomSimple.TipoMunicion.Balas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController jugador =
                collision.GetComponentInParent<PlayerController>();

            if (jugador != null)
            {
                // Llamamos al método pasando el tipo y la cantidad
                jugador.RecuperarMunicionEspecifica(tipoMunicion, cantidadMunicion);
                Destroy(gameObject);
            }
        }
    }
}