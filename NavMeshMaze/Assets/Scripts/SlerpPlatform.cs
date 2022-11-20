using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpPlatform : MovingPlatform
{
    protected override Vector3 NextMove(float t)
    {
        return Vector3.Slerp(_startPosition, _endPosition, t);
    }
}
