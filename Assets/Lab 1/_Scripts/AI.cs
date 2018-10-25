using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    

    private GameObject player;
    private Animator animator;
    private float maxDistanceToCheck = 6.0f;
    private float currentDistance;
    private Vector3 checkDirection;
    public int currentTarget;
    private float distanceFromTarget;
    public Transform[] waypoints = null;
    private Ray ray;
    private RaycastHit hit;
    private  int _amountOfAmmo = 10;
    private bool OnLink;
    private bool _isOpeningDoor;
    private bool _rotating;
    private float _rotation;
    private DoorScript _currentdoorscript;

    // Patrol state variables
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public Transform pointE;
    public Transform pointF;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    public int AmountOfAmmo
    {
        get
        {
            return this._amountOfAmmo;
        }
        set
        {
            _amountOfAmmo = value;
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        pointA = GameObject.Find("p1").transform;
        pointB = GameObject.Find("p2").transform;
        pointC = GameObject.Find("p3").transform;
        pointD = GameObject.Find("p4").transform;
        pointE = GameObject.Find("p5").transform;
        pointF = GameObject.Find("p6").transform;

        navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        waypoints = new Transform[6] {
            pointA,
            pointB,
            pointC,
            pointD,
            pointE,
            pointF
        };
        currentTarget = Random.Range(0,5);
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
        //
    }

    private void Update()
    {
        OnLink = navMeshAgent.isOnOffMeshLink;
        //First we check distance from the player 
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        //Then we check for visibility
        checkDirection = player.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);
        if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            if (hit.collider.gameObject == player)
            {
                animator.SetBool("isPlayerVisible", true);
            }
            else
            {
                animator.SetBool("isPlayerVisible", false);
            }
            if(hit.transform.gameObject.CompareTag("Door") && OnLink && !_isOpeningDoor)
            {
                navMeshAgent.isStopped = true;
                Debug.Log("Stopping");
                Debug.Log("Opening Door");
                _isOpeningDoor = true;
                _currentdoorscript=hit.collider.gameObject.GetComponent<DoorScript>();
                _currentdoorscript.Open();
                StartCoroutine(Continue());
            }
        }
        else
        {
            animator.SetBool("isPlayerVisible", false);
        }

        if(_amountOfAmmo <= 0)
        {
            animator.SetBool("IsOutOfAmmo", true);
        }
        else
        {
            animator.SetBool("IsOutOfAmmo", false);
        }
        if (_rotating)
        {
            _rotation += Time.deltaTime * 30;
            transform.rotation = Quaternion.Euler(0, _rotation, 0);
        }
        //Lastly, we get the distance to the next waypoint target
        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTarget);
        animator.SetInteger("amountOfAmmo",_amountOfAmmo);
        //checks if about to go through door
        OnLink = navMeshAgent.isOnOffMeshLink;
        if (OnLink && !Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            navMeshAgent.isStopped=true;
            StopsAndLook();
        }
    }
    public void SetNextPoint()
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 2;
                break;
            case 2:
                currentTarget = 3;
                break;
            case 3:
                currentTarget = 4;
                break;
            case 4:
                currentTarget = 5;
               break;
            case 5:
               currentTarget = 0;
                break;
        }
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }
    public void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
        //navMeshAgent.isStopped = false;
    }
    public void StopsAndLook()
    {
        navMeshAgent.isStopped = true;
        _rotating = true;
        StartCoroutine(StopRotation());
    }
    IEnumerator Continue()
    {
        yield return new WaitForSeconds(3);
        navMeshAgent.isStopped = false;
        Debug.Log("Closing door");
        yield return new WaitForSeconds(.5f);
        navMeshAgent.isStopped = true;
        _currentdoorscript.Close();
        StopsAndLook();
        yield return new WaitForSeconds(2);
    }
    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(10);
        _rotating = false;
        navMeshAgent.isStopped = false;
    }
}
