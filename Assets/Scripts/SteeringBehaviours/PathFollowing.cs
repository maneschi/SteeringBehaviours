using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class PathFollowing : SteeringBehaviour
{
    public Transform target;
    public float nodeRadius = 5f, targetRadius = 3f;
    private Graph graph;
    private int currentNode = 0;
    private bool isAtTarget = false;
    private List<Node> path;
    // Use this for initialization
    void Start()
    {
        graph = FindObjectOfType<Graph>();
        if (graph == null)
        {
            Debug.LogError("No graph");
            Debug.Break();
        }
    }
    public void UpdatePath()
    {
        path = graph.FindPath(transform.position, target.position);
        currentNode = 0;

    }

    #region SEEK
    Vector3 Seek(Vector3 target)
    {
        Vector3 force = Vector3.zero;
        Vector3 desiredForce = target - transform.position;
        desiredForce.y = 0f;

        float distance = 0;
        distance = isAtTarget ? targetRadius : nodeRadius;

        if (desiredForce.magnitude > distance)
        {
            desiredForce = desiredForce.normalized * weighting;
            force = desiredForce - owner.velocity;
        }
        return force;
    }
    #endregion

    #region GetForce
    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;
        if (path != null && path.Count > 0)
        {
            Vector3 currentPos = path[currentNode].position;
            if (Vector3.Distance(transform.position, currentPos) <= nodeRadius)
            {
                ++currentNode;
                if (currentNode >= path.Count) currentNode = path.Count - 1;
            }
            force = Seek(currentPos);

            #region GIZMOS
            Vector3 prevPosition = path[0].position;
            foreach (Node node in path)
            {
                GizmosGL.AddSphere(node.position, graph.nodeRadius, Quaternion.identity, Color.red);
                GizmosGL.AddLine(prevPosition, node.position, 0.1f, 0.1f, Color.blue, Color.yellow);
                prevPosition = node.position;
            }
            #endregion
        }
        return force;
    }
#endregion
    // Update is called once per frame
    void Update()
    {

    }
}
