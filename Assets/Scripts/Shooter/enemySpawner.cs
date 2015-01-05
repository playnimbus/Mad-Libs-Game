using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

    public float spawnTime;
    float spawnCounter = 0;

    public GameObject enemyToSpawn;
    public Texture2D enemyTexture;

    sceneManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("SceneManager").GetComponent<sceneManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (manager.gameState == sceneManager.GameStates.playing)
        {
            spawnCounter += Time.deltaTime;
            if (spawnCounter >= spawnTime)
            {
                GameObject newEnemy = (GameObject)Instantiate(enemyToSpawn);
                newEnemy.transform.position = gameObject.transform.position;
                spawnCounter = 0;


                Sprite tempSprite = Sprite.Create(enemyTexture, new Rect(0, 0, enemyTexture.width, enemyTexture.height), new Vector2(0.5f, 0.5f));


                //for some reaon, assigning a sprite to a spriteRenderer adds a second spriteRenderer to the gameObject. This is my current solution to the problem.
                SpriteRenderer[] renderers = newEnemy.GetComponents<SpriteRenderer>();

                for (int x = 0; x < renderers.Length; x++)
                {
                    renderers[x].sprite = tempSprite;
                }

            }
        }
	
	}
}
