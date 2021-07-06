using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    EnemyBase _base;
    int level;

    public int HP { get; set; }

    public List<Moves> moves { get; set; }

    public Enemy(EnemyBase eBase, int eLevel)
    {
        _base = eBase;
        level = eLevel;
        HP = _base.MaxHP;

        //Genera los movimientos en los enemigos
        moves = new List<Moves>();
        foreach(var move in _base.LearnableMoves)
        {
            if (move.Level <= level)
            {
                moves.Add(new Moves(move.Base));
            }

            if (moves.Count >= 4)
                break;
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((_base.MaxHP * level) / 100f) + 10; }
    }
    public int SpAttack
    {
        get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
    }
    public int SpDefense
    {
        get { return Mathf.FloorToInt((_base.SpDefense * level) / 100f) + 5; }
    }
}
