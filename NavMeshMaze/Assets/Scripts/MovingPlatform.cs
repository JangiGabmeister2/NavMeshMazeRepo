using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] protected Vector3 _startPosition; //the start position of the moving platform
    [SerializeField] protected Vector3 _endPosition; //the end position
    [Range(0f, 1f)]
    [SerializeField] protected float _t; //point between the start and end positions
    [SerializeField] protected float _speed; //speed at which the platform moves

    protected bool _forward; //the bool if the platform is forward (start to end), then back (end to start)

    protected float cooldown; //the platform pauses before moving to next point

    private void Update()
    {
        cooldown -= Time.deltaTime; //cooldown goes down 1 every second
        if (cooldown > 0) //if cooldown is greater than 0
        {
            return; //returns to if statement until false
        }

        if (_forward) //if moving forward
        {
            _t += _speed * Time.deltaTime; //increases point by speed per second
            _t = Mathf.Min(1, _t); //returns the smallest value between point and 1
            if (_t >= 1) //if point is not less than 1
            {
                _forward = false; //bool is set to false, therefore going backwards
                cooldown = 3f; //cooldown resets to 3 seconds
            }
        }
        else //if going backward
        {
            _t -= _speed * Time.deltaTime; //same as above
            _t = Mathf.Max(0, _t);
            if (_t <= 0)
            {
                _forward = true;
                cooldown = 3f;
            }
        }

        transform.position = NextMove(_t); //sets the next point to move towards
    }

    protected virtual Vector3 NextMove(float t)
    {
        return Vector3.Lerp(_startPosition, _endPosition, t); //moves the platform from start to end at an interpolated value
    }
}
