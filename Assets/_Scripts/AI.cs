using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;
    public bool inDebug = false;

    public StateMachine<AI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(FirstState.Instance);
        gameTimer = Time.time;
    }

    private void Update()
    {
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }

        if (seconds == 5)
        {
            seconds = 0;
            switchState = !switchState;
        }
        if (!inDebug)
        {
            stateMachine.Update();
        }
    }
    private Transform _player;
    private Vector3 _backtospot;
    private Quaternion _backtorotation;
    private bool _followplayer;
    private MeshCollider _collider;

    public NavMeshAgent Agent;
    public GameObject Body;
    public bool FollowPlayer
    {
        get
        {
            return this._followplayer;
        }
        set
        {
            this._followplayer = value;
        }
    }
    /*
     * if (FollowPlayer)
        {
            this.Agent.SetDestination(this._player.position);
    }
        else
        {
            this.Body.transform.position = this._backtospot;
            this.Body.transform.rotation = this._backtorotation;
        }
public void Follow()
    {
        this.FollowPlayer = true;
        this._collider.convex = true;
        this._collider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        this.FollowPlayer = false;
        this._collider.isTrigger = false;
        this._collider.convex = false;
    }
     */
}
