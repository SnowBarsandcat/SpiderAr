using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    GameObject spider;
    public GameObject point;
    void Start()
    {
        spider = GameObject.FindGameObjectWithTag("Spider");
        InvokeRepeating("SpawnPoint", 1f, 2f);
    }

    void SpawnPoint()
    {
        float spawnX = spider.transform.position.x + Random.Range(-3, 3);
        float spawnY = spider.transform.position.y + spider.transform.localScale.y;
        float spawnZ = spider.transform.position.z + Random.Range(-4, 4);
        Vector3 positionToSpawn = new Vector3(spawnX, spawnY, spawnZ);
        Instantiate(point, positionToSpawn, transform.rotation);
    }


    void Update()
    {
        
    }
}
