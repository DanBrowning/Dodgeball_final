using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private AIAgent _owner;
    private float _elapseTime;

    public AttackState(AIAgent owner)
    {
        _owner = owner;
        _definitions.statename = Definitions.StateName.Attack;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Attack");
        _elapseTime = 0;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Attack " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;
    }
}
