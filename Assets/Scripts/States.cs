using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PatrolState : State
{
    // State Enter transition
    public override void Enter()
    {
        // enable path following behaviour
        owner.GetComponent<PathFollowBehaviour>().enabled = true;
    }

    // State "OnUpdate" part
    public override void Think()
    {
        // if Enemy is in sigt, attack
        //owner.ChangeState(new AttackState());
        
        // if 2 big friendly ships left, flee
        //owner.ChangeState(new FleeState());
    }

    // State Exit transition
    public override void Exit()
    {
        // disable path following behaviour
        owner.GetComponent<PathFollowBehaviour>().enabled = false;
    }
}


public class AttackState : State
{
    // State Enter transition
    public override void Enter()
    {
        // enable attack behaviour
        //owner.GetComponent<AttackBehaviour>().enabled = true;   
    }

    // State "OnUpdate" part
    public override void Think()
    {
        // if No enemy in sigt, patrol
        //owner.ChangeState(new PatrolState());
        
        // if 2 big friendly ships left, flee
        //owner.ChangeState(new FleeState());
    }

    // State Exit transition
    public override void Exit()
    {
        // disable attack behaviour
        //owner.GetComponent<AttackBehaviour>().enabled = false;
    }
}

public class FleeState : State
{
    // State Enter transition
    public override void Enter()
    {
        // enable flee behaviour
        owner.GetComponent<ShipFleeBehaviour>().enabled = true;
    }

    // State "OnUpdate" part
    public override void Think()
    {
        var target = owner.GetComponent<ShipFleeBehaviour>().target;
        var vortexOpen = owner.GetComponent<ShipController>().escapeVortex;
        // vector between ship and target
        var vectorToTarget = target - owner.transform.position;
        // angle between ship and target
        var angleToTarget = Quaternion.LookRotation(vectorToTarget);

        // if facing flee target already, and no vortex open yet - open space vortex
        if (target != Vector3.zero && angleToTarget.y < 0.05 && angleToTarget.z < 0.05 && !vortexOpen)
            owner.GetComponent<ShipController>().OpenVortex(target);
    }

    // State Exit transition
    public override void Exit()
    {
        // no exit, only destruction        
    }
}
