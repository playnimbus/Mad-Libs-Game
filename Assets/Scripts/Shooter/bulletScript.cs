using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
    bool hurtPlayer = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setVelocity(Vector2 velocity)
    {
        gameObject.rigidbody2D.velocity = velocity;
    }

    public void SetHurtPlayer(bool set)
    {
        hurtPlayer = set;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy" && !hurtPlayer)
        {
            coll.gameObject.GetComponent<enemyController>().dealDamage(1);
            GameObject.Destroy(gameObject);
        }
        if (coll.gameObject.tag == "Player" && hurtPlayer)
        {
            coll.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            GameObject.Destroy(gameObject);
        }
    }

    
}
