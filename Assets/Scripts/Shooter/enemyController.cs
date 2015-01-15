using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class enemyController : MonoBehaviour {

    public List<string> enemyTypes = new List<string>();
    public float health = 5;
    public float speed = 1;
    public float defense = 1;
    public float damage = 1;
    public string attackType = "Ranged";

    GameObject player;
    public GameObject bullet;

    int shootTimer = 0;
    int meleeTimer = 0;
	// Use this for initialization
	void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player");
        defineEnemyTypes(); //Defines the enemies and their statistics. Look into finding a way to make this to be done outside of this script.
        setEnemy(0); //Setting Enemies to ranged for now. Need to set this from the data sent from the drawing app. 
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.transform.position - gameObject.transform.position;

        rigidbody2D.velocity = direction * speed;

        switch (attackType)
        {
            case "Ranged":
                {
                    shootTimer++;
                    if (shootTimer >= 100)
                    {
                        Shoot();
                        shootTimer = 0;
                    }
                    break;
                }
            case "Melee":
                {
                    //Nothing yet....
                    break;
                }
        }
	}

    void defineEnemyTypes()
    {
        enemyTypes.Add("Melee");
        enemyTypes.Add("Ranged");
    }

    public void setEnemy(int enemy)
    {
        switch (enemyTypes[enemy])
        {
            case "Melee":
                health = 5;
                speed = 1;
                defense = 1;
                damage = 1;
                attackType = "Melee";
                break;

            case "Ranged":
                health = 3.5f;
                speed = 0.5f;
                defense = 1f;
                damage = 5f;
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
        
        newBullet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        newBullet.transform.LookAt(player.transform);
        newBullet.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        newBullet.GetComponent<bulletScript>().setVelocity(movementDirection * 5);
    }

    void Melee()
    {
        meleeTimer++;
        if (meleeTimer > 100)
        {
            player.SendMessage("TakeDamage", 1);
            meleeTimer = 0;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Melee();
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            meleeTimer = 0;
        }
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
