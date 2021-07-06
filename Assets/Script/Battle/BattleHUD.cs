using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text lvlText;
    [SerializeField] HPBar hpbar;

    Enemy _enemy;

    public void SetData(Enemy enemy) //Establece los valores de cada uno de los textos dentro del Hud
    {
        _enemy = enemy;

        nameText.text = enemy.Base.Name;
        lvlText.text = "Lvl " + enemy.Level;
        hpbar.SetHP((float) enemy.HP/enemy.MaxHP);
    }
    
    public IEnumerator UpdateHP() //Controla la barra de vida del HUD
    {
       yield return hpbar.SetHPSmooth((float)_enemy.HP / _enemy.MaxHP);
    }

}
