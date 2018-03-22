using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private AIAgent _owner;
    private float _elapseTime;

    public float ThrowSpeed = 50;

    public AttackState(AIAgent owner)
    {
        _owner = owner;
        _definitions.statename = Definitions.StateName.Attack;
    }

    public override void OnEnter()
    {
        Debug.Log(_owner.gameObject.name + " Enter Attack");
        _elapseTime = 0;

    }

    public override void OnExit()
    {
        Debug.Log(_owner.gameObject.name + " Exit Attack " + _elapseTime);
    }

    public override void OnUpdate()
    {
        _elapseTime += Time.deltaTime;

        Transform closestTarget = NearestTarget(_owner.targets);

        _owner.targetAgent = closestTarget;

        if (_owner.targetAgent != null)
        {
            if (Vector3.Distance(_owner.transform.position, _owner.targetAgent.position) < Mathf.Infinity)
            {

                Throw();
                _owner.SwitchState(Definitions.StateName.Defense);
            }
            else
            {
                Vector3 direction = _owner.GetDirectionToTarget(_owner.transform.position, _owner.targetBall.position);
                _owner.MoveForward(direction);
            }
        }
    }

    public void Throw()
    {
        float speed = _owner.ThrowSpeed;
        Vector3 direction = _owner.GetDirectionToTarget(_owner.transform.position, _owner.targetAgent.position);
        _owner.targetBall.GetComponent<Ball>().Throw(speed, direction);

        _owner.SwitchState(Definitions.StateName.Defense);
    }

    public Transform NearestTarget(List<Transform> allTargets)
    {
       Transform closestTarget = null;

        float closestDistance = Mathf.Infinity;

        foreach (Transform target in allTargets)
        {
            if (target.GetComponent<AIAgent>().isOut)
                continue;

            float checkDistance = Vector3.Distance(_owner.transform.position, target.position);

            if (checkDistance < closestDistance && target.tag != _owner.gameObject.tag)
            {
                closestTarget = target;
                closestDistance = checkDistance;
            }
        }

        

        return closestTarget;
    }
}
