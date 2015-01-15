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

	// Use this for initialization
	void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player");
        defineEnemyTypes();
        setEnemy(Random.Range(0,1));
        Debug.Log("Enemy Type" + attackType);
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.transform.position - gameObject.transform.position;

        rigidbody2D.velocity = direction;
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

    public void dealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
