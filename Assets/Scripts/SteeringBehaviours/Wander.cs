using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class Wander : SteeringBehaviour  {
    public float offset = 3f, radius= 1.5f,jitter=0.4f;
    private Vector3 targetDir,randomDir,circlePos;
	
    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        //Generate random number from - half max to half max
        float randX = Random.Range(0, 0x7fff) - 0x7fff * 0.5f;   //0x7fff = 32767
        float randZ = Random.Range(0, 0x7fff) - 0x7fff * 0.5f;

        #region Calc RandomDir
        //SET randomDIR to new Vector3 x = randx   and z =randz
        randomDir = new Vector3(randX, 0.0f, randZ);
        //SET RandomDir to normalised randomDir
        randomDir = randomDir.normalized;

        //SET randomDIR to randomDIR x Jitter
        randomDir *= jitter;

        #endregion

        #region Calc targetDir
        //SET targetDIR to targetDR + randomDir
        targetDir += randomDir;
        //SET targetDIR to normalise
        targetDir = targetDir.normalized;
        //SET targetdir= targetDir*radius
        targetDir *= radius;
        #endregion


        #region Calc Force
        //SET seeekPOS to owner's position  + targetDir
        Vector3 seekPos = owner.transform.position + targetDir;

        //SET seekpos to seekpos + owner's fwd * offset
        seekPos = seekPos + owner.transform.forward * offset;

        #region GIZMOS
        Vector3 offsetPos = transform.position + transform.forward.normalized * offset;
        GizmosGL.AddCircle(offsetPos + Vector3.up * 0.1f, radius, Quaternion.LookRotation(Vector3.down), 16, Color.red);
   
        GizmosGL.AddCircle(seekPos + Vector3.up * 0.15f,
            radius * 0.6f,
            Quaternion.LookRotation(Vector3.down),
            16,
            Color.blue
            );
        #endregion

        #endregion
        //SET desiredForce to seekpos - position
        Vector3 desiredForce = seekPos - transform.position;

        //IF desiredForce is not zero
        if (desiredForce != Vector3.zero)
        {
            //SET desiredForce to desiredForce.normalised x weighting
            desiredForce = desiredForce.normalized * weighting;

            //SET force to desired force - owner's velocity
            force = desiredForce - owner.velocity;
        }
        return force;
    }
}
