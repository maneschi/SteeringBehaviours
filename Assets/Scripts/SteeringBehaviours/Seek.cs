using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 5f;
    public override Vector3 GetForce ()
    {
        Vector3 force = Vector3.zero;         //SET force zero

        //if target is null
            //RETURN force
            if (target == null) return force;

        //SET desiredForce to direction from target to owners pos
        Vector3 desiredForce =  target.position - transform.position;

        //SET desiredForce y to zero
        desiredForce.y = 0f;


        //IF desiredForce is greater than stopping distance    MAGNITUDE
        if (desiredForce.magnitude > stoppingDistance)
        {
            //SET desiredForce to desiredForce normalised x weighing
            desiredForce = desiredForce.normalized * weighting;

            //  SET force to desiredForce - owners velocity
            force = desiredForce;// - owner.velocity;
        }


        return force;              //RETURN force

    }
 }
