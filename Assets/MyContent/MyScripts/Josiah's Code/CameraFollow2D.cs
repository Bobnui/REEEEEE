using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follows a target precisely at -10 on the z axis.
/// </summary>
public class CameraFollow2D : MonoBehaviour
{
    [Header("The target we want to follow")]
    public Transform FollowTarget;

    public void LateUpdate()
    {
        Vector3 targetPos = new Vector3(FollowTarget.position.x, FollowTarget.position.y, -10f);
        transform.position = targetPos;
    }
}
