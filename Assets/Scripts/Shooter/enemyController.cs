using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class enemyController : MonoBehaviour {

    public List<string> enemyTypes = new List<string>();
    public float health = 5;
    public float speed;
    public float defense;
    public float damage;
    public string attackType;

    GameObject player;
    public GameObject bullet;

    int tempTimer = 0;
	// Use this for initialization
	void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player");
        defineEnemyTypes();
        Debug.Log("Enemy Type" + attackType);
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.transform.position - gameObject.transform.position;

        rigidbody2D.velocity = direction;

        tempTimer++;
        if (tempTimer >= 100)
        {
            shootTowards(player);
            tempTimer = 0;
        }
            
	}

    void defineEnemyTypes()
    {
        enemyTypes.Add("Touch");
        enemyTypes.Add("Ranged");
    }

    public void setEnemy(int enemy)
    {
        switch (enemyTypes[enemy])
        {
            case "Touch":
                health = 5;
                speed = 5;
                defense = 5;
                damage = 5;
                attackType = "Touch";
                break;
            case "Ranged":
                health = 3.5f;
                speed = 3;
                defense = 1;
                damage = 5;
                attackType = "Ranged";
                break;
        }
    }

    void shootTowards(GameObject towardsThisObject)
    {
        GameObject newBullet = (GameObject)Instantiate(bullet);
        Vector3 offset = new Vector3(0, 0, 0);
        
        Vector3 directionTowardsObject = towardsThisObject.transform.position - gameObject.transform.position;
        Vector3 movementDirection = player.transform.position - gameObject.transform.position;

        if (movementDirection.x < 0)
            offset = new Vector3 (gameObject.GetComponent<PolygonCollider2D>().bounds.size.x * -1, 0f, 0f);
        else if (movementDirection.x > 0)
            offset = new Vector3 (gameObject.GetComponent<PolygonCollider2D>().bounds.size.x * 1, 0f, 0f);

        if (movementDirection.y < 0)
            offset = new Vector3(0f, gameObject.GetComponent<PolygonCollider2D>().bounds.size.y * -1, 0f);
        else if (movementDirection.y > 0)
            offset = new Vector3(0f, gameObject.GetComponent<PolygonCollider2D>().bounds.size.y * 1, 0f);


        newBullet.transform.position = gameObject.transform.position + offset;
        newBullet.GetComponent<bulletScript>().setVelocity(directionTowardsObject);
        
        GameObject.Destroy(newBullet, 3);

    }

    public void dealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
