using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves
{ 
    public MoveBase Base { get; set; }

    public int MOVETIMES { get; set; }

    public Moves(MoveBase pBase)
    {
        Base = pBase;
        MOVETIMES = pBase.MovesTimes;
    }




}

