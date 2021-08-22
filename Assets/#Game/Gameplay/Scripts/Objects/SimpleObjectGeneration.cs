using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectGeneration : MonoBehaviour
{
    public GameObject[] collectables;
    public GameObject[] obstacles;

    public Transform[] collectableSpawnPosition;
    public Transform[] obstaclesSpawnPosition;
    
    public float xOffSet;
    public float yOffSet;

    //private List<GameObject> objectsQueue;
    void Start()
    {
        //objectsQueue = new List<GameObject>(collectable);

        RegenerationObjectsPosition(collectables,collectableSpawnPosition);
        RegenerationObjectsPosition(obstacles, obstaclesSpawnPosition);
    }

    public void RegenerationObjectsPosition(GameObject[] objects, Transform[] positions)
    {
        for(int i = 0; i < positions.Length; i++)
        {
            int nRandom = Random.Range(0, objects.Length);
            
            Vector3 newDirection = new Vector2(Random.Range(-xOffSet, xOffSet), Random.Range(-yOffSet, yOffSet));
            
            Instantiate(objects[nRandom],
                positions[i].transform.position + newDirection,
                Quaternion.identity);
        }
    }

    // Update is called once per frame


    public void ResetObjectsIteractable(string objectsTag, GameObject[] collectables, Transform[] collectableSpawnPosition)
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(objectsTag);    
        foreach (GameObject objs in allObjects)
        {
            Destroy(objs);
        }
        RegenerationObjectsPosition(collectables, collectableSpawnPosition);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetObjectsIteractable(Constants.collectable, collectables, collectableSpawnPosition);
            ResetObjectsIteractable(Constants.obstacle, obstacles, obstaclesSpawnPosition);

        }
    }
}
