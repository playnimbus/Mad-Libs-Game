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
            Shoot();
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

    void Shoot()
    {
        GameObject newBullet = (GameObject)Instantiate(bullet);
        newBullet.GetComponent<bulletScript>().SetHurtPlayer(true);

        Vector3 movementDirection = player.transform.position - gameObject.transform.position;

        //up = (0,1) down = (0,-1) left = (-1, 0)  right = (1,0)
        Debug.Log(Mathf.Abs(movementDirection.x) + " " + Mathf.Abs(movementDirection.y));
        //Left (-1,0)
        if ((Mathf.Abs(movementDirection.x) < 0) && (Mathf.Abs(movementDirection.y) == 0))
        {
            newBullet.transform.position = new Vector3(gameObject.transform.position.x - gameObject.GetComponent<PolygonCollider2D>().bounds.size.x,
                gameObject.transform.position.y, 
                gameObject.transform.position.z);
            Debug.Log("Left");
        }
        //Right (1,0)
        else if ((Mathf.Abs(movementDirection.x) > 0) && (Mathf.Abs(movementDirection.y) == 0))
        {
            newBullet.transform.position = new Vector3(gameObject.transform.position.x + gameObject.GetComponent<PolygonCollider2D>().bounds.size.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z);
            Debug.Log("Right");
        }
        //Up
        else if ((Mathf.Abs(movementDirection.x) == 0) && (Mathf.Abs(movementDirection.y) < 0))
        {
            newBullet.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z);
            Debug.Log("Up");
        }
        //Down
        else if ((Mathf.Abs(movementDirection.x) == 0) && (Mathf.Abs(movementDirection.y) < 0))
        {
            newBullet.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y - gameObject.GetComponent<PolygonCollider2D>().bounds.size.y,
                gameObject.transform.position.z);
            Debug.Log("Down");
        }
        
        newBullet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        newBullet.transform.LookAt(player.transform);
        newBullet.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        newBullet.GetComponent<bulletScript>().setVelocity(movementDirection * 5);
        

    }

    void shootTowards(GameObject towardsThisObject)
    {


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
