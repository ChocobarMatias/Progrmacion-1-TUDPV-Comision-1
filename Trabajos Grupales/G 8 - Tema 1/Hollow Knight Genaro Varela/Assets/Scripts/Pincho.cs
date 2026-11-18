using UnityEngine;

public class Pincho : MonoBehaviour
{
    [Header("Pogo Jump")]
    public float fuerzaRebote = 12f;
    [Header("Efectos")]
    public GameObject Chispas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null)
            return;
        if (rb.linearVelocity.y > 0f)
            return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaRebote);
        if (Chispas != null)
        {
            Vector2 posicion = other.ClosestPoint(transform.position);
            GameObject efecto = Instantiate(
                Chispas,
                posicion,
                Quaternion.identity
            );
            Destroy(efecto, 1f);
        }
        Debug.Log("Jugar dañado");
    }
    
}
