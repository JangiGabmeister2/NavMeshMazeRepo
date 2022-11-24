using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float _rotX; //rotation speed on x axis
    [SerializeField] float _rotY; //rotation speed on y axis
    [SerializeField] float _rotZ; //rotation speed on z axis

    private void Update()
    {
        transform.Rotate(_rotX * 0.5f * Time.deltaTime, _rotY * 0.5f * Time.deltaTime, _rotZ * 0.5f * Time.deltaTime); //rotates object at assigned speeds
    }
}
