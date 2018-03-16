using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState {

    protected Definitions _definitions = new Definitions();

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();

    public Definitions.StateName GetStateName()
    {
        return _definitions.statename;
    }
}
