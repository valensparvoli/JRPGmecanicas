using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] EnemyBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Enemy enemy { get; set; }

    public void SetUp() //Establece que tipo de imagen se cargara cuando comience la batalla
    {
        enemy= new Enemy(_base, level);
        if (isPlayerUnit)
            GetComponent<Image>().sprite = enemy.Base.BackSprite;
        else
            GetComponent<Image>().sprite = enemy.Base.FrontSprite;
        
    }
}
