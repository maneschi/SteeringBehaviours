using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;
public class AgentDirector : MonoBehaviour {
    public Transform selectedTarget;
    public float rayDistance = 1000f;
    public LayerMask selectionLayer;
    private AIAgent[] agents;

	// Use this for initialization
	void Start () {
        agents = FindObjectsOfType<AIAgent>();
	}
    void ApplySelection()
    {
        foreach (AIAgent agent in agents)
        {
            PathFollowing  pathFollowing = agent.GetComponent<PathFollowing>();
            if (pathFollowing != null)
            {
                pathFollowing.target = selectedTarget;
                pathFollowing.UpdatePath();
            }
        }
    }
    //Constantly checking for Input
	void CheckSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, rayDistance, selectionLayer))
        {
            GizmosGL.AddSphere(hit.point, 5f, Quaternion.identity, Color.red);
            // IF  user clicked left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                selectedTarget = hit.collider.transform;
                ApplySelection();
            }
        }
    }
	// Update is called once per frame
	void Update () {
        CheckSelection();
	}
}
