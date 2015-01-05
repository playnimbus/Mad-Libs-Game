using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

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

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject.Destroy(gameObject);

        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<enemyController>().dealDamage(1);
        }
    }
}
