using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definitions
{
    public enum StateName
    {
        Null = -1,
        Idle,
        Attack,
        Pickup,
        Run
    }


    public StateName statename { get; set; }


}