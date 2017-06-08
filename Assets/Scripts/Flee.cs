using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour
{

    public Transform target;
  
    public override Vector3 GetForce()
    {
        //This steering behaviour will flee from the target

        Vector3 force = Vector3.zero;         //SET force zero

        //if target is null
        //RETURN force
        if (target == null) return force;

        //SET desiredForce to direction to  flee from the target
        Vector3 desiredForce =  transform.position - target.position;

        //SET desiredForce y to zero
        desiredForce.y = 0f;


     
            //SET desiredForce to desiredForce normalised x weighing
            desiredForce = desiredForce.normalized * weighting;

        //  SET force to desiredForce - owners velocity
        force = desiredForce;// - owner.velocity;

      

        return force;              //RETURN force

    }
}
