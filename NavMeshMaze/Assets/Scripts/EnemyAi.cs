using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MoveState { Patrol, Chase }

public class EnemyAi : MonoBehaviour
{
    [SerializeField] GameObject _player; //the player game object
    [SerializeField] Transform[] _waypoints; //an array of waypoints the ai will follow
    [SerializeField] Animator animator; //animator controller to activate movement and idle animations
    [Range(0f,5f)]
    [SerializeField] float waitSeconds = 1f; //the amount of seconds the ai will wait after reaching a waypoint, default 1 second
    [Range(3f, 20f)]
    [SerializeField] float searchRadius = 6f; //the radius at which the enemy searches the player in, if the player is within the radius, the ai will follow the player
    [SerializeField] bool resetPosition; //resets position to first waypoint position after reaching player if true.

    int _waypointIndex = 0; //which waypoint it is going to currently
    bool moving = false; //checks if agent is moving

    NavMeshAgent _agent; //the navmesh agent

    public MoveState moveState; //switches from patrolling and chasing states depending on player distance from enemy

    private void Start()
    {
        animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        NextState();
    }

    private void Update()
    {
        animator.SetBool("isMoving", moving);
    }

    private void NextState() //switches movement states
    {
        switch (moveState)
        {
            case MoveState.Patrol:
                StartCoroutine(PatrolState());
                break;
            case MoveState.Chase:
                StartCoroutine(ChaseState());
                break;
            default:
                break;
        }
    }

    IEnumerator PatrolState()
    {
        Search(); //searches for the closest waypoint, rather than the last one it followed
        while (moveState == MoveState.Patrol)
        {
            #region Patrol
            Vector3 waypointPosition = _waypoints[_waypointIndex].position; //sets a vector3 position based on the position of the currently selected waypoint
            _agent.SetDestination(waypointPosition); //sets the destination of the navmesh agent as the above position

            if (Vector3.Distance(transform.position, waypointPosition) < 0.5f) //checks if the agent is close to the waypoint position
            {
                moving = false; //sets animation to idle

                yield return new WaitForSeconds(waitSeconds); //waits a set amount of seconds

                _waypointIndex++; //increases waypoint array index, which makes the agent move to next waypoint
            }
            else
            {
                moving = true; //if agent is not close enough to waypoint position, continues movement animation
            }

            if (_waypointIndex == _waypoints.Length) //if waypoint index is at the end of the waypoint array
            {
                System.Array.Reverse(_waypoints); //reverses the order of the array, so last item is now first item, and previously first item is now last item
                _waypointIndex = 0; //sets the waypoint index to the first item ie next waypoint is now current waypoint
            }
            #endregion

            if (IsPlayerInRange()) //checks if player is within range
            {
                moving = true; //if true, animation changes to running animation, if not already

                moveState = MoveState.Chase; //changes movement state to chase state
            }

            yield return null;
        }
        NextState(); //changes move state if it has changed
    }

    IEnumerator ChaseState()
    {
        while (moveState == MoveState.Chase) //checks if move state is chase state
        {
            ChasePlayer(); //chases the player
            if (!IsPlayerInRange()) //checks if player is no longer in range
            {
                moving = false; //animation switches to idle state

                yield return new WaitForSeconds(1f); //waits one second

                moveState = MoveState.Patrol; //swtiches move state to patrol state
            }
            yield return null;
        }
        NextState();
    }

    public bool IsPlayerInRange() //checks if player is in range
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < searchRadius) //if the playe rposition is withing 6 units(?) of the enemy
        {            
            return true; //if true, return true
        }
        else
        {
            return false; //if false, ...
        }
    }

    public void ChasePlayer()//chases the player
    {
        if (IsPlayerInRange())//if player is within range of enemy
        {
            _agent.SetDestination(_player.transform.position); //sets agent destination to player position
        }
    }

    public void Search() //searches for closest waypoint to enemy agent
    {
        int closestIndex = -1;
        float closestDistance = float.MaxValue;

        for (int index = 0; index < _waypoints.Length; index++) //for every item in its waypoint array
        {
            float currentDistance = Vector3.Distance(_waypoints[index].position, transform.position); //checks for the distance between that waypoint and enemy position
            if (currentDistance < closestDistance) //if that distance is smaller that the furthest possible distance
            {
                closestDistance = currentDistance; //sets the current distance as the closest distance
                closestIndex = index; //sets closest waypoint as new waypoint index
            }
        }

        _waypointIndex = closestIndex; //sets new waypoint index as index
    }
}
