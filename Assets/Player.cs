using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Rigidbody r;
    public float speed=9;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		r.velocity =speed*new Vector3  (Input.GetAxis ("Horizontal"),0f,Input.GetAxis("Vertical"));
	}
}
