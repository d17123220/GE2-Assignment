using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowBehaviour : SteeringBehaviour
{

    public Path path;
    public float angleOffset = 0.0f;
    public float radiusOffset = 0.0f;
    public int nextPoint = 0;
    private bool canFollow = false;


    Vector3 nextWaypoint;

    public float waypointDistance = 500.0f;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, nextWaypoint);
        }
    }

    public void StartFollowing()
    {
        canFollow = true;
    }


    public void Start()
    {
        
    }

    public override Vector3 Calculate()
    {
        if (canFollow)
        {
            nextWaypoint = path.NextWaypoint(nextPoint);
            if (nextPoint != 0)
            {
                nextWaypoint +=  Vector3.up * radiusOffset * Mathf.Sin(angleOffset) + Vector3.right * radiusOffset * Mathf.Cos(angleOffset);
            }

            if (Vector3.Distance(transform.position, nextWaypoint) < waypointDistance)
            {
                nextPoint++;
            }

            if (path.IsLast(nextPoint))
            {
                return boid.ArriveForce(nextWaypoint);
            }
            else
            {
                return boid.SeekForce(nextWaypoint);
            }
        }
        else
        {
            return Vector3.zero;
        }
    }
}
