using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadPrefab;
    public float spawnPosZ = 0f;
    public int amountRoadParts = 3;
    private float offset;
    // public bool clearFirstRoadPart = true;
    public GameObject roadPartPrefab;
    // private float roadLengh;


    void Start()
    {
        spawnPosZ = roadPartPrefab.transform.localScale.z;

        offset = roadPartPrefab.transform.localScale.z * (amountRoadParts - 1);

        for(int i = 0; i < amountRoadParts; i++)
        {
            SpawnRoad();
        }
    }

    

    void SpawnRoad()
    {
        GameObject roadObj = Instantiate(roadPrefab, 
        new Vector3(roadPrefab.transform.localPosition.x, roadPrefab.transform.localPosition.y, spawnPosZ),
        Quaternion.identity, transform);
        
        roadObj.GetComponent<RoadPartController>().respawnOffset = offset;
       
        roadObj.GetComponent<RoadObjectsSpawner>().roadPref = roadPartPrefab;

        spawnPosZ += roadPartPrefab.transform.localScale.z;
        
    }
}
