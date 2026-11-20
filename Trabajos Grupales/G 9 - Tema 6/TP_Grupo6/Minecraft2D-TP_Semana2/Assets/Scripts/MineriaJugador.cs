using UnityEngine;

public class MineriaJugador : MonoBehaviour
{
    [SerializeField] private float distanciaMaxima = 3f;
    [SerializeField] private LayerMask capaBloques; 
    [SerializeField] private Herramienta herramienta;

    private Camera camaraPrincipal;

    private void Awake()
    {
        camaraPrincipal = Camera.main;

        if (herramienta == null)
        {
            herramienta = GetComponent<Herramienta>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IntentarPicarBloque();
        }
    }

    private void IntentarPicarBloque()
    {
        if (herramienta == null || !herramienta.PuedeUsarse())
        {
            Debug.Log("No se puede picar: la herramienta está rota.");
            return;
        }

        Vector3 posicionCursor = camaraPrincipal.ScreenToWorldPoint(
            Input.mousePosition);

        Vector2 origen = transform.position;
        Vector2 destino = new Vector2(
            posicionCursor.x,
            posicionCursor.y);

        Vector2 direccion = destino - origen;

        if (direccion == Vector2.zero)
        {
            return;
        }

        direccion.Normalize();

        RaycastHit2D impacto = Physics2D.Raycast(
            origen,
            direccion,
            distanciaMaxima,
            capaBloques);

        Debug.DrawRay(
            origen,
            direccion * distanciaMaxima,
            Color.yellow,
            1f);

        if (!impacto)
        {
            return;
        }

        BloqueDestructible bloque =
            impacto.collider.GetComponent<BloqueDestructible>();

        if (bloque == null)
        {
            return;
        }

        bloque.RecibirGolpe(herramienta.DanioPorGolpe);
        herramienta.Usar();
    }
}
