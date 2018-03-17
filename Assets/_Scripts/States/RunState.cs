using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunState : BaseState
{
    private AIAgent _owner;
    private float _elapseTime;

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

        Transform closestBall = GetClosestBall();

        _owner.targetBall = closestBall;

        if (_owner.targetBall != null)
        {
            Vector3 direction = _owner.GetDirectionToTarget(_owner.transform.position,_owner.targetBall.position);

            if (!_owner.hasBall)
            {
                _owner.MoveForward(direction.normalized);
                CanGrabBall();
            }
        }

    }

}
