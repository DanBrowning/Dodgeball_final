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
        Debug.Log(_owner.gameObject.name + " Enter Run");

        _elapseTime = 0;
    }

    public override void OnExit()
    {
        Debug.Log(_owner.gameObject.name + " Exit Run " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;

        Transform closestBall = NearestBall(_owner.balls);

        _owner.targetBall = closestBall;

        if (_owner.targetBall != null)
        {
            Vector3 direction = _owner.GetDirectionToTarget(_owner.transform.position,_owner.targetBall.position);

            if (!_owner.hasBall)
            {
                _owner.MoveForward(direction.normalized);
                if (Vector3.Distance(_owner.transform.position, _owner.targetBall.position) < 1.5f)
                {
                    _owner.Pickup();
                    _owner.SwitchState(Definitions.StateName.Attack);
                }
            }


        }

        if (!closestBall)
        {
            //_owner.SwitchState(Definitions.StateName.Defense);
        }
    }

    public Transform NearestBall(List<Transform> allBalls)
    {
        Transform closestBall = null;

        float closestDistance = Mathf.Infinity;

        foreach (Transform ball in allBalls)
        {
            if (ball.GetComponent<Ball>().isAirborn)
            {
                continue;
            }

            float checkDistance = Vector3.Distance(_owner.transform.position, ball.position);

            if (checkDistance < closestDistance && ball.tag == _owner.gameObject.tag)
            {
                closestBall = ball;
                closestDistance = checkDistance;
            }
        }

        return closestBall;
    }
}
