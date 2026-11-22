using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        // Refactor: si no se asignó a mano en el Inspector, lo busca en el padre (antes tiraba NullReference)
        if (player == null) player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null) return;

        // Se busca el componente en vez de depender solo del Tag "Enemigo"
        Enemigo enemigo = collision.GetComponentInParent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.RecibirDanio(player.DanioEspada);
        }
    }
}
