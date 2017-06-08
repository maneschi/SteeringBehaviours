﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    public Transform target;
    public float distance = 5f;
    public float zoomSpeed = 5f;
    public float orthoSize = 5f;

    public float xSpeed = 120f, ySpeed = 120f;
    public float yMinLimit = 0;
    public float yMaxLimit = 80f;
    public float minDistance = 5f;
    public float maxDistance = 20f;

    public float minOrthoSize = 5f;
    public float maxOrthoSize = 20f;


    private float x = 0, y = 0;
	// Use this for initialization
	void Start () {
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

	}
	
	// Update is called once per frame
	void LateUpdate ()        //happens AFETR everything else rendered
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // check if cmera otho
        if (Camera .main .orthographic)
        {
            
            orthoSize = Mathf.Clamp(orthoSize -scroll*zoomSpeed ,minOrthoSize , maxOrthoSize  );
        }
        else
        {
            distance = Mathf.Clamp(distance-scroll,minDistance ,maxDistance );
        }
        if(target!= null&&Input.GetMouseButton(1))   //check for target and right mouse button pressed
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            x += mouseX * xSpeed * Time.deltaTime;
            y += -mouseY * ySpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negativeDistance = new Vector3(0, 0, -distance);
            Vector3 position = rotation * negativeDistance + target.position;
            transform.rotation = rotation;
            transform.position = position;

        }
	}
}
