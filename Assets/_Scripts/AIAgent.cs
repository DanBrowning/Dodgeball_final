﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour {

    private StateManager _stateManager;


	// Use this for initialization
	void Start ()
    {
        _stateManager = new StateManager();
        _stateManager.AddState(new IdleState(this));
        _stateManager.AddState(new AttackState(this));
        _stateManager.AddState(new PickupState(this));
        _stateManager.AddState(new RunState(this));
        _stateManager.desiredState = Definitions.StateName.Idle;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _stateManager.desiredState = Definitions.StateName.Idle;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _stateManager.desiredState = Definitions.StateName.Attack;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _stateManager.desiredState = Definitions.StateName.Pickup;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _stateManager.desiredState = Definitions.StateName.Run;
        }
        _stateManager.Update();
	}
}