using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject prefab;  //object to spawn
    public float spawnRate = 1f;   //spawn rate in seconds
    [HideInInspector]
    public List<GameObject> objects = new List<GameObject>();
    

    private float spawnTimer = 0f;  //counts up every frame in seconds

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);  //transparent
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

	Vector3 GenerateRandomPoint()
    {
        //SET halfscale to half of transform's scale
        Vector3 halfScale = transform.localScale * 0.5f;  //*0.5f  run slightly faster than /2
        //SET RandomPoint to zero
        Vector3 randomPoint = Vector3.zero;

        //SET Random point x y and z to random range between -halfScale to halfScale
        randomPoint.x = Random.Range(-halfScale.x, halfScale.x);
        randomPoint.y = Random.Range(-halfScale.y, halfScale.y);
        randomPoint.z = Random.Range(-halfScale.z, halfScale.z);

      //  print(randomPoint);
        return randomPoint;
    }

    public void Spawn(Vector3 position, Quaternion rotation)
    {
        //SET clone to new instance of prefab
        GameObject clone = Instantiate(prefab);
        //ADD clone to objects list
        objects.Add(clone);
        //SET clone's position to spawner position + position
        clone.transform.position = position;
        //SET clone's rotation to rotation
        clone.transform.rotation = rotation;
    }


	// Update is called once per frame
	void Update () {
        //SET spawnTimer to spawnTimer + delta time
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnRate)
        {
            Vector3 randomPoint = GenerateRandomPoint();
            Spawn(transform.position + randomPoint , Quaternion.identity);
            spawnTimer = 0;
        }
	}
}
