using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadPrefab;
    public float spawnPosZ = 0f;
    public int amountRoadParts = 3;
    private float offset;
    
    public GameObject roadPartPrefab;

    private GameObject[] SpawnedObjects;////
    


    void Start()
    {
        SpawnedObjects = new GameObject[amountRoadParts];/////
        spawnPosZ = roadPartPrefab.transform.localScale.z;

        offset = roadPartPrefab.transform.localScale.z * (amountRoadParts - 1);

        for(int i = 0; i < amountRoadParts; i++)
        {
            SpawnRoad(i);
            
        }

        ImplementPrevObj();/////
    }

    

    void SpawnRoad(int index)
    {
        GameObject roadObj = Instantiate(roadPrefab, 
        new Vector3(roadPrefab.transform.localPosition.x, roadPrefab.transform.localPosition.y, spawnPosZ),
        Quaternion.identity, transform);
        SpawnedObjects[index] = roadObj;//////
        
        // roadObj.GetComponent<RoadPartController>().respawnOffset = offset;
       
        roadObj.GetComponent<RoadObjectsSpawner>().roadPref = roadPartPrefab;

        spawnPosZ += roadPartPrefab.transform.localScale.z;
        
    }

    void ImplementPrevObj()
    {
        for(int i = 0; i < SpawnedObjects.Length; i++)
        {
            if(i == 0)
                SpawnedObjects[i].GetComponent<RoadPartController>().prevRoadObj = SpawnedObjects[SpawnedObjects.Length-1];
            else
                SpawnedObjects[i].GetComponent<RoadPartController>().prevRoadObj = SpawnedObjects[i-1];
        }
    }
}
