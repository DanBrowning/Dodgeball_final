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

        if (Vector3.Distance(_owner.transform.position, _owner.targetAgent.position) < 10)
        {
            Throw();
        }
        else
        {
            _owner.GetDirectionToTarget(_owner.transform.position,_owner.targetBall.position);
        }
    }

    public void Throw()
    {
        if (!_owner.item)
            return;

        Debug.Log("Throw");

        _owner.item.GetComponent<Rigidbody>().isKinematic = false;
        _owner.item.GetComponent<Rigidbody>().useGravity = true;
        _owner.item = null;
        _owner.Holding.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = _owner.transform.position * _owner.ThrowSpeed;
        _owner.Holding.GetChild(0).parent = null;
        {
            _owner.canHold = true;
        }

        _owner.SwitchState(Definitions.StateName.Defense);
    }
}
