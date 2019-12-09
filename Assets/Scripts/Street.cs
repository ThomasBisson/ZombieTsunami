using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    public Transform m_createGroundPoint;

    public bool MustCreateAnotherPoint(Vector3 pos)
    {
        return pos.x > m_createGroundPoint.position.x;
    }
}
