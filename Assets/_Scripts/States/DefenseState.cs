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
        Debug.Log(_owner.gameObject.name + " Enter Defense");
        _elapseTime = 0;
    }

    public override void OnExit()
    {
        Debug.Log(_owner.gameObject.name + " Exit Defense " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;

        foreach (Transform ball in _owner.balls)
        {
            if (ball.tag == _owner.tag)
                _owner.SwitchState(Definitions.StateName.Run);
        }
    }




}
