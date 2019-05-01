using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPartController : MonoBehaviour
{

    GameManager GM;
    private Vector3 movingVector;
    [HideInInspector]
    public float respawnOffset;
    public bool isFirstPart = false;
    
    public GameObject road;

    void Start()
    {
        
        GM = FindObjectOfType<GameManager>();
        movingVector = new Vector3(0, 0, -1);
    }

    void LateUpdate()
    {
        if( transform.position.z < -road.transform.localScale.z)
        {
            if(isFirstPart == false)
                Respawn();
            else
                Destroy();
        }
    }
    void FixedUpdate()
    {
         if(!GM.gameOver)
        {
            Moving();
        }
    }

    void Moving()
    {
        transform.Translate(movingVector * Time.deltaTime * GM.actualRunningSpeed );
        
    }

    public void Respawn()
    {
        Vector3 newPos = transform.position;
        newPos.z = respawnOffset;
        transform.position = newPos;
        GetComponent<RoadObjectsSpawner>().DisactivateObjects();
        GetComponent<RoadObjectsSpawner>().RespawnObjects();
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
