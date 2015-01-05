using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

    public float spawnTime;
    float spawnCounter = 0;

    public GameObject enemyToSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        spawnCounter += Time.deltaTime;
        if (spawnCounter >= spawnTime)
        {
            GameObject newEnemy = (GameObject)Instantiate(enemyToSpawn);
            newEnemy.transform.position = gameObject.transform.position;
            spawnCounter = 0;
        }
	
	}
}
