using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class ThirdState : State<AI>
{
    private static ThirdState _instance;

    private ThirdState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThirdState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThirdState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Third State");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Third State");
    }

    public override void UpdateState(AI _owner)
    {
        if (_owner.switchState)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }
    }
}
