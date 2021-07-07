using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Enemy/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string moveName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] EnemyType type;
    [SerializeField] int power;
    [SerializeField] int accurancy;
    [SerializeField] int moveTimes;

    /*Forma en la que hacemos que las variables de una clase sean propiedades*/
    public string MoveName
    {
        get { return moveName; }
    }

    public string Description
    {
        get { return description; }
    }

    public EnemyType Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int Accurancy
    {
        get { return accurancy; }
    }

    public int MovesTimes
    {
        get { return moveTimes; }
    }

    public bool isSpecial
    {
        get{
            if (type == EnemyType.Fire || type == EnemyType.Grass)
            {
                return true;
            }
            else
            {
                return false;
            };
        }
    } //propiedad que registra si el movimiento es o no es especial.
    /*--------------------------*/
}
