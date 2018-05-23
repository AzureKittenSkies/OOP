using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] prefabs = null;
    public float spawnRadius = 5.0f;
    public float spawnRate = 1.0f;
    private float spawnFactor = 0.0f; 

    void Update()
    {
        HandleSpawn();
    }

    void HandleSpawn()
    {
        spawnFactor += Time.deltaTime;
        if (spawnFactor > spawnRate)
        {
            // get a random index into array
            int randomIndex = Random.Range(0, prefabs.Length);
            // spawn a random prefab from the list
            Spawn(prefabs[randomIndex]);
            // reset timer
            spawnFactor = 0;
        }
    }

    // spawn an object based off of the argument passed in "_object"
    void Spawn(GameObject _object)
    {
        // clone the object
        GameObject newObject = Instantiate(_object);
        // generate random spawn point
        Vector3 randomPoint = Random.insideUnitCircle * spawnRadius;
        // set new objects position to random one
        newObject.transform.position = transform.position + randomPoint;
    }




}
