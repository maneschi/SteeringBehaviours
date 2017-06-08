using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AIAgent))]    //you cnt attach h
public class SteeringBehaviour : MonoBehaviour {
    public float weighting = 7.5f;   //7.5 is a good number
    [HideInInspector] public AIAgent owner;    //owner tells all who owns it

    // Use this for initialization
    void Awake()    //gets called even if unticked

    {
        owner = GetComponent<AIAgent>();
    }

    // Update is called once per frame
    void Update() {

    }

    public virtual Vector3 GetForce()      //calc force depending on behaviour
    {
        return Vector3.zero;
    } 
}
