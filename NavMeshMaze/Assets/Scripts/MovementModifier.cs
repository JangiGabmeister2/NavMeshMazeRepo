using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementModifier : MonoBehaviour
{
    private NavMeshAgent _agent; //the nav mesh agent
    private float _speed = 8f; //the speed at which the agent moves
    private float _grassSpeed = 3f; //the speed at which the agent moves when walking through tall grass

    public int filter; //displays filter to inspector, for testing purposes

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        NavMeshHit hit; //when the navmesh agent hits another mesh

        _agent.SamplePathPosition(-1, 0.0f, out hit);

        int AreaMask = 1 << NavMesh.GetAreaFromName("TallGrass"); //the area mask is 1 if the agent enters tall grass

        filter = hit.mask & AreaMask; //displays area mask

        if (filter == 0) //if area mask = 0, default value
        {
            _agent.speed = _speed; //default speed
        }
        else //if any other value, only 2 values
        {
            _agent.speed = _grassSpeed; //reduces speed by a lot ( do the math )
        }

        //Debug.Log(hit.mask);
    }
}
