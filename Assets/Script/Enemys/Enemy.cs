using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ System.Serializable]
public class Enemy 
{
    [SerializeField] EnemyBase _base;
    [SerializeField] int level;

    /*Se escribe en mayuscula cuando son propiedades y de esta forma podemos diferenciarla de una variable*/
    public EnemyBase Base {
        get
        {
            return _base;
        }
    }
    public int Level {
        get
        {
            return level;
        } 
    }

    public int HP { get; set; }

    public List<Moves> moves { get; set; }

    public void Init()
    {

        HP = MaxHP;

        //Genera los movimientos en los enemigos
        moves = new List<Moves>();
        foreach(var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
            {
                moves.Add(new Moves(move.Base));
            }

            if (moves.Count >= 4)
                break;
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; } //Forma en la que se toma la estadistica en pokemon
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10; } //Forma en la que se toma la estadistica en pokemon
    }
    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; } //Forma en la que se toma la estadistica en pokemon
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; } //Forma en la que se toma la estadistica en pokemon
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; } //Forma en la que se toma la estadistica en pokemon
    }
    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; } //Forma en la que se toma la estadistica en pokemon
    }
    /*-----------------------------------------------*/


    public DamageDetails TakeDamage(Moves moves, Enemy attacker) 
    {
        /*Funcion encargada de registrar si el golpe es critico o no*/
        float critical = 1f;
        if (Random.value * 100f < 6.25f)
        {
            critical = 2f;
        }

        /* Metodo por el que obtenemos el modificador de tipo de ataque*/
        float type = TypeChart.GetEffectiveness(moves.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(moves.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Critical = critical,
            Surrended = false
        };

        float attack = (moves.Base.isSpecial) ? attacker.SpAttack : attacker.Attack;
        float defense = (moves.Base.isSpecial) ? attacker.SpDefense : attacker.Defense;
        /* La variable de arriba registra el tipo de ataque, sea especial o no. La forma en la que se 
         registra es como un if. Si move.base.isSpecial es true, se ejecuta la primera parte antes de los dos 
        puntos y sino se ejecuta la segunda parte */

        //Forma que se utiliza en los juegos de pokemon para controlar el daño dependiendo de las stats de la unidad
        float modifier = Random.Range(0.85f, 1f)* type*critical;  
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * moves.Base.Power * ((float)attack/ defense) + 2;
        int damage = Mathf.FloorToInt(d * modifier);
        //

        //Controla si la unidad a la que se le hace daño tiene o no tiene vida
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Surrended = true;
        }
        //

        return damageDetails;
    }

    public Moves GetRandomMove() // Obtiene un movimiento enemigo al azar
    {
        int r = Random.Range(0, moves.Count);
        return moves[r];
    }

}

public class DamageDetails
{
    public bool Surrended { get; set; }
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }
}
