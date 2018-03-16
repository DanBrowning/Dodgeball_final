using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private AIAgent _owner;
    private float _elapseTime;

    public IdleState(AIAgent owner)
    {
        _owner = owner;
        _definitions.statename = Definitions.StateName.Idle;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Idle");
        _elapseTime = 0;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Idle: " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;
    }
}
