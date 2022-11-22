using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float _rotX;
    [SerializeField] float _rotY;
    [SerializeField] float _rotZ;

    private void Update()
    {
        transform.Rotate(_rotX * 0.5f * Time.deltaTime, _rotY * 0.5f * Time.deltaTime, _rotZ * 0.5f * Time.deltaTime);
    }
}
