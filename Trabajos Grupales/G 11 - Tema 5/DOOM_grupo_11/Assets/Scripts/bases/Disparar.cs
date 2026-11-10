
using UnityEngine;

public class Disparar : MonoBehaviour
{
   
    [Header("Proyectiles")]
    public GameObject prefabBala;
    public GameObject prefabEscopeta;
    public GameObject prefabSuperShotgun; // <--- Nuevo prefab para la Super Shotgun
    public GameObject prefabPlasma;



    [Header("Disparo")]
    public Transform puntoDisparo;

    private PlayerController jugador;
    private DoomSimple.Jugador jugadorLogico;

    void Start()
    {
        jugador = GetComponent<PlayerController>();

        if (jugador == null)
        {
            Debug.LogError("No se encontró PlayerController.");
            enabled = false;
            return;
        }

        jugadorLogico = jugador.JugadorLogico;

        if (jugadorLogico == null)
        {
            Debug.LogError("JugadorLogico sigue siendo null.");
            enabled = false;
            return;
        }

        Debug.Log("Arma inicial: " + jugadorLogico.ArmaEquipada.Nombre);
    }

    void Update()
    {

        




        if (jugadorLogico == null)
            return;

        // Cambiar armas
        if (Input.GetKeyDown(KeyCode.Alpha1)) jugadorLogico.CambiarArma(0);


        if (Input.GetKeyDown(KeyCode.Alpha2))
            jugadorLogico.CambiarArma(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            jugadorLogico.CambiarArma(2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            jugadorLogico.CambiarArma(4);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            jugadorLogico.CambiarArma(5);

        // Disparar
        if (Input.GetButtonDown("Fire1"))
        {
            DispararArma();
        }
    }

    void DispararArma()
    {
        var arma = jugadorLogico.ArmaEquipada;

        GameObject proyectil = null;

        switch (arma.Nombre)
        {
            case "Pistola":
                proyectil = prefabBala;
                break;

            case "Escopeta":
                proyectil = prefabEscopeta;
                break;

            case "SuperShotgun":
                proyectil = prefabSuperShotgun; // <--- Usa su propio prefab
                break;

            case "BFG9000":
                proyectil = prefabPlasma;
                break;

            case "Motosierra":
                Debug.Log("La motosierra es un arma cuerpo a cuerpo.");
                return;
        }

        if (proyectil == null)
            return;

        if (!jugador.IntentarUsarMunicion(arma))
            return;

        Instantiate(
            proyectil,
            puntoDisparo.position,
            puntoDisparo.rotation
        );
    }
}
