using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent _agent;
    RaycastHit rayHit;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        NewMovement();
    }

    void NewMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out rayHit))
            {
                _agent.destination = rayHit.point;
            }
        }

        if (!(_agent.remainingDistance > _agent.stoppingDistance))
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
