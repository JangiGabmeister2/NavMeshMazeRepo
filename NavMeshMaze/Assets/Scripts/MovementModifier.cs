using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementModifier : MonoBehaviour
{
    private NavMeshAgent _agent;
    private float _speed = 8f;
    private float _grassSpeed = 1f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        NavMeshHit hit;

        _agent.SamplePathPosition(-1, 0.0f, out hit);

        int GrassMask = 1 << NavMesh.GetAreaFromName("TallGrass");
        int filtered = hit.mask & GrassMask;

        if (filtered == 0)
        {
            _agent.speed = _speed;
        }
        else
        {
            _agent.speed = _grassSpeed;
        }

        //Debug.Log(hit.mask);
    }
}
