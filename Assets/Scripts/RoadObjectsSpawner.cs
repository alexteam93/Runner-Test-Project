using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadObjectsSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject RoadObjectsPrefab;
        public int maxAmount;
        
    }

    public Pool[] pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    [HideInInspector]
    public GameObject roadPref;

    public float firstLinePosition, lineWidth;
    private int linesAmount = 3;
    private float halfSize;///

   
    void Start()
    {
        
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.maxAmount; i++)
            {
                GameObject obj = Instantiate<GameObject>(pool.RoadObjectsPrefab);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.name, objectPool);
           
        }
        halfSize = roadPref.transform.localScale.z/2;////
        RespawnObjects();
        
    }


    public GameObject SpawnFromPool(string name)
    {
        // if(!poolDictionary.ContainsKey(name))
        // {
        //     Debug.Log(name + " doesn't exist in dictionary!");
        //     return null;
        // }

        GameObject ObjectToSpawn = poolDictionary[name].Dequeue();
        poolDictionary[name].Enqueue(ObjectToSpawn);

        return ObjectToSpawn;
    }

    public void RespawnObjects()
    {
        ShufflePools();
        for (int i = 0; i < linesAmount; i++)
        {

            int poolIndex = i;
            int objectSpawnAmount = Random.Range(0, pools[poolIndex].maxAmount + 1);
            
            if(objectSpawnAmount != 0)
            {
                Vector3 newPos = SpawnFromPool(pools[poolIndex].name).transform.position;

                newPos.x = firstLinePosition + (lineWidth * i);
                newPos.z = Random.Range(-halfSize + transform.position.z + lineWidth, 
                                        halfSize + transform.position.z - (lineWidth * (objectSpawnAmount + 1)));
                
                for(int n = 0; n < objectSpawnAmount; n++)
                {                  
                    
                    GameObject obj = SpawnFromPool(pools[poolIndex].name);                
                    
                    obj.transform.position = newPos;
                    obj.SetActive(true);
                    newPos.z += lineWidth;
                    


                }
                
            }
           
        }
    }

    public void ShufflePools()
    {
        for(int i = 0; i < pools.Length; i++)
        {
            int rnd = Random.Range(0, pools.Length);
            Pool tmpPool = pools[rnd];
            pools[rnd] = pools[i];
            pools[i] = tmpPool;

        }
    }

    public void DisactivateObjects()
    {
        foreach(Pool pool in pools)
        {
            for(int i = 0; i < poolDictionary[pool.name].Count; i++)
            {
                SpawnFromPool(pool.name).SetActive(false);
            }
                
           
        }
    }
}
