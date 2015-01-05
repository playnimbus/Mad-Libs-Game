using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float bulletSpeed;
    public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
        {
            gameObject.rigidbody2D.AddForce(new Vector2( 0, moveSpeed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.rigidbody2D.AddForce(new Vector2(0, -moveSpeed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.rigidbody2D.AddForce(new Vector2(-moveSpeed, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.rigidbody2D.AddForce(new Vector2(moveSpeed, 0));
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spawnBullet(new Vector2(0, bulletSpeed));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            spawnBullet(new Vector2(0, -bulletSpeed));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spawnBullet(new Vector2(-bulletSpeed, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spawnBullet(new Vector2(bulletSpeed, 0));
        }

	}

    void spawnBullet(Vector2 velocity)
    {
        GameObject newBullet = (GameObject)Instantiate(bullet);

        //up = (0,1) down = (0,-1) left = (-1, 0)  right = (1,0)
        Vector2 bulletDirection = new Vector2(velocity.x / Mathf.Abs(velocity.x), velocity.y / Mathf.Abs(velocity.y));

        Vector3 offset = new Vector3(0,0,0);

        switch ((int)bulletDirection.x)
        {
            case -1: offset = new Vector3( -gameObject.GetComponent<PolygonCollider2D>().bounds.size.x, 0,0);break;
            case 1: offset = new Vector3(gameObject.GetComponent<PolygonCollider2D>().bounds.size.x, 0, 0); break;
        }
        switch ((int)bulletDirection.y)
        {
            case -1: offset = new Vector3(0, -gameObject.GetComponent<PolygonCollider2D>().bounds.size.y, 0); break;
            case 1: offset = new Vector3(0, gameObject.GetComponent<PolygonCollider2D>().bounds.size.y, 0); break;
        }

        newBullet.transform.position = gameObject.transform.position + offset;
    
        newBullet.GetComponent<bulletScript>().setVelocity(velocity);
        GameObject.Destroy(newBullet, 3);
    }
}
