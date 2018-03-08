using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager {

    private List<BaseState> _states = new List<BaseState>();
    private BaseState _currentState;

    private Definitions.StateName _desiredState;

    public BaseState currentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    public Definitions.StateName desiredState
    {
        get { return _desiredState; }
        set { _desiredState = value; }
    }

    public StateManager()
    {
        _desiredState = Definitions.StateName.Null;
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	public void Update ()
    {
        Definitions.StateName currentTempState = _currentState != null ? _currentState.GetStateName() : Definitions.StateName.Null;

        if (_desiredState != currentTempState && _desiredState != Definitions.StateName.Null)
        {
            BaseState newState = GetState(_desiredState);

            if (newState != null)
            {
                if (_currentState != null)
                {
                    _currentState.OnExit();
                }

                newState.OnEnter();
                _currentState = newState;
            }
        }

        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }

    public void AddState(BaseState state)
    {
        _states.Add(state);
    }

    private BaseState GetState(Definitions.StateName stateName)
    {
        foreach(BaseState state in _states)
        {
            if(state.GetStateName() == stateName)
                {
                return state;
            }
        }

        return null;
    }
}
