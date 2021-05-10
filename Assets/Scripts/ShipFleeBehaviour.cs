using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFleeBehaviour : SteeringBehaviour
{
    public Vector3 target = Vector3.zero;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target);
        }
    }

    public override Vector3 Calculate()
    {   
        // if there is no target to flee to - calculate one
        if (target == Vector3.zero)
            target = CalculateFleeTarget();

        return - boid.SeekForce(target);
    }

    // if there is no target to flee to, calculate a point in space behind ship
    public Vector3 CalculateFleeTarget()
    {
        // Calculate and return one behind in 12000 units (roughly 1.5x of ship length)
        Vector3 targetPoint = transform.position - 12000.0f * transform.forward;
        
        // if for some reason flee point becomes absolute (0,0,0), thne adjust it slightly to avoid pitfalls of check equation
        if (targetPoint == Vector3.zero)
            targetPoint += new Vector3(0.0f,0.0f,1.0f);

        return targetPoint;
    }


    public void Update()
    {

    }
}
