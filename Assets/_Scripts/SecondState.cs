using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using UnityEngine.AI;

public class SecondState : State<AI>
{
    private static SecondState _instance;

    private SecondState()
    {
        if (_instance != null)
        {
            return;
        }
        //this._player = GameObject.FindWithTag("Player").transform;
        //FollowPlayer = false;
        //_collider = GetComponent<MeshCollider>();
        //this._backtospot = this.Body.transform.position;
        //this._backtorotation = this.Body.transform.rotation;
    }

    public static SecondState Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Second State");
        _instance = this;
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Second State");
    }

    public override void UpdateState(AI _owner)
    {
        if (!_owner.switchState)
        {
            _owner.stateMachine.ChangeState(ThirdState.Instance);
        }
    }
}
