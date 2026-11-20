using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject BarraDeVida;

    public void SetHP(float hpNormalized)
    {
        BarraDeVida.transform.localScale = new Vector3(hpNormalized, 1f);
    }
}
