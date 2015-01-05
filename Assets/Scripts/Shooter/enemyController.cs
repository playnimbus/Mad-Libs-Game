using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

    int health = 5;

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.transform.position - gameObject.transform.position;

        rigidbody2D.velocity = direction;
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
