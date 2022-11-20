using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] protected Vector3 _startPosition;
    [SerializeField] protected Vector3 _endPosition;
    [Range(0f, 1f)]
    [SerializeField] protected float _t;
    [SerializeField] protected float _speed;

    protected bool _forward;

    private void Update()
    {
        if (_forward)
        {
            _t += _speed * Time.deltaTime;
            _t = Mathf.Min(1, _t);
            if (_t >= 1) _forward = false;
        }
        else
        {
            _t -= _speed * Time.deltaTime;
            _t = Mathf.Max(0, _t);
            if (_t <= 0) _forward = true;
        }

        transform.position = NextMove(_t);        
    }

    protected virtual Vector3 NextMove(float t)
    {
        return Vector3.Lerp(_startPosition, _endPosition, t);
    }
}
