using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseState :BaseState
{
    private AIAgent _owner;
    private float _elapseTime;

    public DefenseState(AIAgent owner)
    {
        _owner = owner;
        _definitions.statename = Definitions.StateName.Defense;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Defense");
        _elapseTime = 0;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Defense " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;
    }




}
