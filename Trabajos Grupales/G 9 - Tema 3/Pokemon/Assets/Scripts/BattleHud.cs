using Tema3Pokemon;
using UnityEngine;
using UnityEngine.UI;
public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpText;

    public void SetData(Pokemon pokemon)
    {
        nameText.text = pokemon.Apodo;
        levelText.text = "Lvl " + pokemon.Nivel;
        hpText.SetHP((float) pokemon.Salud + 100 );
    }
}
