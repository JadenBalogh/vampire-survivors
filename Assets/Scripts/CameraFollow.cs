using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followTime = 0.1f;

    private Vector2 currVel;

    private void FixedUpdate()
    {
        Vector3 targetPos = Vector2.SmoothDamp(transform.position, target.position, ref currVel, followTime);
        transform.position = targetPos + Vector3.forward * transform.position.z;
    }
}
