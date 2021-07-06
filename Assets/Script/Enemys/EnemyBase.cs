using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ CreateAssetMenu(fileName ="Enemy", menuName ="Enemy/Create new enemy")] //Crea la posibilidad de crear desde unity nuevos objetos. 
public class EnemyBase : ScriptableObject
{
    [SerializeField] string enemyName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] EnemyType type1;
    [SerializeField] EnemyType type2;

    //Base stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List <LearnableMove> learnablesMoves;

    public string Name
    {
        get { return enemyName; }
    }

    public string Description
    {
        get { return description; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public EnemyType Type1
    {
        get { return type1; }
    }

    public EnemyType Type2
    {
        get { return type2; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnablesMoves; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }
}

[System.Serializable]
public class LearnableMove //Clase que muestra los movimientos que puede aprender una unidad 
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;
    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum EnemyType //Establece los tipos de unidades que pueden existir
{
    None,
    Normal,
    Fire,
    Grass
}

public class TypeChart //Clase que crea la ventaja entre los diferentes tipos de unidades
{
    static float[][] chart =
    {   
        //                      Nor  Fir Grass
        /*Normal*/ new float [ ]{1f, 1f, 1f},
        /*Fire*/   new float [ ]{1f, 0.5f, 2f},
        /*Grass*/  new float [ ]{1f, 0.5f, 0.5f}
    };

    public static float GetEffectiveness(EnemyType attackType, EnemyType defenseType) //Funcion que calcula la efectividad del ataque
    {
        if (attackType == EnemyType.None || defenseType == EnemyType.None)
        {
            return 1;
        }

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
            
    }

}
