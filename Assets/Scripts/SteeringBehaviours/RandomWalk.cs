using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : SteeringBehaviour
{

    public Transform target;

    public override Vector3 GetForce()
    {
        //This steering behaviour will walk randomly

        Vector3 force = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));



        return force;              //RETURN force

    }
}
