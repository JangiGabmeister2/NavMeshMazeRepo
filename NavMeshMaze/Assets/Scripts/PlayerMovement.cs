using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent _agent; //the navmesh agent
    RaycastHit rayHit; 

    public Animator animator; //the animator controller which swtiches the animation from idle and running

    void Start()
    {
        animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        NewMovement(); //executes the new movement code to move the player
    }

    void NewMovement()
    {
        if (Input.GetMouseButton(0)) //when the player clicks or holds down the mouse
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //creates a raycast from the screen to where it hits in the game scene
            if (Physics.Raycast(ray.origin, ray.direction, out rayHit))
            {
                _agent.destination = rayHit.point; //moves the agent to that ray hit point
            }
        }

        if (_agent.remainingDistance >= _agent.stoppingDistance) //checks if the agent has reached its click-to-move point
        {
            animator.SetBool("isMoving", true); //moves the player if hasnt yet
        }
        else
        {
            animator.SetBool("isMoving", false); //stops the player when it has
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") //when the player collider collides with an enemy AI's collider
        {
            Die(); //player dies
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reloads the scene - simulates dying and starting the game over
    }
}
