using UnityEngine;

public class DropBloque : MonoBehaviour
{
    public string Item { get; private set; }

    public void Configurar(string item)
    {
        Item = item;
    }

    private void Start()
    {
        // Regla Cátedra: Destrucción programada tras 30s por si no lo recogen
        Destroy(gameObject, 30f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InventarioJugador inventario))
        {
            inventario.Recoger(Item);
            Destroy(gameObject);
        }
    }
}