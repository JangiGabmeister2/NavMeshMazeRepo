using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState { Patrol, Chase }

public class EnemyAi : MonoBehaviour
{
    [SerializeField] GameObject _player; //the player game object
    [SerializeField] Transform[] _waypoints; //an array of waypoints the ai will follow
    float _speed = 7.5f; //the speed at which the ai will move
    int _waypointIndex = 0; //which waypoint it is going to currently

    [SerializeField] private Animator animator;

    public MoveState moveState;

    private void Start()
    {
        NextState();

        animator = GetComponent<Animator>();
    }

    private void NextState()
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
        Search();
        while (moveState == MoveState.Patrol)
        {
            #region Patrol
            Vector3 waypointPosition = _waypoints[_waypointIndex].position;
            MovetoPoint(waypointPosition);

            if (Vector3.Distance(transform.position, waypointPosition) < 0.5f)
            {
                animator.SetBool("isMoving", false);

                yield return new WaitForSeconds(1f);

                _waypointIndex++;
            }
            else
            {
                animator.SetBool("isMoving", true);
            }

            if (_waypointIndex == _waypoints.Length)
            {
                System.Array.Reverse(_waypoints);
                _waypointIndex = 0;
            }
            #endregion

            if (IsPlayerInRange())
            {
                animator.SetBool("isMoving", true);

                moveState = MoveState.Chase;
            }

            yield return null; //wait a single frame
        }
        NextState();
    }

    IEnumerator ChaseState()
    {
        while (moveState == MoveState.Chase)
        {
            ChasePlayer();
            if (!IsPlayerInRange())
            {
                animator.SetBool("isMoving", false);

                yield return new WaitForSeconds(1f);

                moveState = MoveState.Patrol;
            }
            yield return null;
        }
        NextState();
    }

    public bool IsPlayerInRange()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChasePlayer()
    {
        if (IsPlayerInRange())
        {
            MovetoPoint(_player.transform.position);
        }
    }

    void MovetoPoint(Vector3 point)
    {
        Vector3 directionToPlayer = point - transform.position;

        directionToPlayer.Normalize();
        directionToPlayer *= _speed * Time.deltaTime;
        transform.position += directionToPlayer;

        if (directionToPlayer != Vector3.zero)
        {
            transform.forward = directionToPlayer;
        }
    }
    public void Search()
    {
        int closestIndex = -1;
        float closestDistance = float.MaxValue;

        for (int index = 0; index < _waypoints.Length; index++)
        {
            float currentDistance = Vector3.Distance(_waypoints[index].position, transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestIndex = index;
            }
        }

        _waypointIndex = closestIndex;
    }
}
