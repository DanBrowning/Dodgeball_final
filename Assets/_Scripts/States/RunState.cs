using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunState : BaseState
{
    private AIAgent _owner;
    private float _elapseTime;
    private float _probability;

    public RunState(AIAgent owner)
    {
        _owner = owner;
        _definitions.statename = Definitions.StateName.Run;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Run");
        _elapseTime = 0;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Run " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;
    }

}
