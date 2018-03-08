using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupState : BaseState
{
    private AIAgent _owner;
    private float _elapseTime;

    public PickupState(AIAgent owner)
    {
        _owner = owner;
        _definitions.statename = Definitions.StateName.Pickup;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Pickup");
        _elapseTime = 0;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Pickup " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;
    }
}
