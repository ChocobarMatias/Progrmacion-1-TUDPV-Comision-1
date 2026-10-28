
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class SupervivienteControlador : MonoBehaviour
{
    [Header("Interfaz")]
    [SerializeField] private Slider barraVida;
    [SerializeField] private TMP_Text textoHierba;
    [SerializeField] private TMP_Text textoSpray;

    private Superviviente leon;

    void Start()
    {
        leon = new Superviviente("Leon", 35, true);

        leon.AgregarItem(new HierbaVerde("Hierba Verde"));
        leon.AgregarItem(new SprayPrimerosAuxilios("Spray"));

        ActualizarBarraVida();
    }

    
    void Update()
    {

        if (Keyboard.current == null)
            return;
        // hola chicos, lo unico que hace esto es llamar al metodo //RecibirDano()// al precionar //H// es solo para probar si funciona >:)
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            RecibirDano(10);
        }

        // usa la yerba con F
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            UsarHierbaVerde();
        }

        // usa el sprait con G
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            UsarSpray();
        }

       
    }

    
    public void RecibirDano(int cantidad)
    {
        if (leon == null)
            return;

        leon.RecibirDano(cantidad);
        ActualizarBarraVida();
    }


    
    public void ActualizarBarraVida()
    {
        if (leon == null || barraVida == null)
            return;

        barraVida.value = leon.Salud;
    }

    
    public void UsarHierbaVerde()
    {
        if (leon == null)
            return;

        if (leon.UsarItem(1))
        {
            textoHierba.text = "0";
            ActualizarBarraVida();
        }
    }


    
    public void UsarSpray()
    {
        if (leon == null)
            return;

        if (leon.UsarItem(2))
        {
            textoSpray.text = "0";
            ActualizarBarraVida();
        }
    }

}

