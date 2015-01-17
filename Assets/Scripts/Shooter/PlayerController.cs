using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float mousetrackingSpeed;
    public float bulletSpeed;
    public float fireRate;

    public GameObject bullet;
    public GameObject playerWeapon;
    public GameObject weaponSprite;
    public GameObject playerCursor;
    public GameObject bulletOrigin;

    public bool isGamepad;

    private float lastShot = 0.0F;

    sceneManager scene;

	// Use this for initialization
	void Start () {
        scene = GameObject.Find("SceneManager").GetComponent<sceneManager>();
        checkInputType();
	}
	
	// Update is called once per frame
	void Update () {

        switch (scene.gameState)
        {
            case sceneManager.GameStates.menu: menuUpdate(); break;
            case sceneManager.GameStates.playing: playingUpdate(); break;
            case sceneManager.GameStates.switchingRoom: switchingRoomUpdate(); break;
        }
	}

    void menuUpdate()
    {
        checkMovementInput();
    }
    void playingUpdate()
    {
        checkMovementInput();
        checkShootingInput();
    }
    void switchingRoomUpdate()
    {
        gameObject.rigidbody2D.AddForce(new Vector2(0, moveSpeed/2));
        checkShootingInput();
    }

    void checkInputType()
    {
        if (isGamepad)
        {
            //keyboard and mouse selected
            playerCursor.SetActive(false);
        }
        else if (!isGamepad)
        {
            //Gamepad selected
            playerCursor.SetActive(true);
        }
    }

    void checkMovementInput()
    {
        if (!isGamepad)
        {
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.rigidbody2D.AddForce(new Vector2(0, moveSpeed));
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
        }
        if (isGamepad)
        {
            float ySpeed = Input.GetAxis("Vertical") * moveSpeed;
            float xSpeed = Input.GetAxis("Horizontal") * moveSpeed;

            gameObject.rigidbody2D.AddForce(new Vector2(xSpeed, ySpeed));
        }
        
    }
    void checkShootingInput()
    {
        if (!isGamepad)
        {
            //cursor gameObject movement
            Screen.showCursor = false;
            playerCursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.99F));

            //aim
            Vector3 rot = playerCursor.transform.position - transform.position;
            float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            playerWeapon.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            //shoot
            if (Input.GetMouseButtonDown(0))
            {
                GameObject clone;
                clone = Instantiate(bullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation) as GameObject;
                clone.rigidbody2D.velocity = -bulletOrigin.transform.up * bulletSpeed;
                Destroy(clone.gameObject, 2);
                lastShot = Time.time;
            }
        }

        if (isGamepad)
        {
            //aim
            Vector2 rsInput = new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
            float angle = Mathf.Atan2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical")) * Mathf.Rad2Deg;
            //deadzone detection
            if (rsInput.sqrMagnitude < 0.15f)
            {
                return;
            }
            playerWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);

            //shoot
            //** Figuring out how to 3rd Axis on 360 Controller but for now when not deadzoning timed shots instantiate
            if (rsInput.sqrMagnitude > 0.15f && Time.time > fireRate + lastShot)
            {
                GameObject clone;
                clone = Instantiate(bullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation) as GameObject;
                clone.rigidbody2D.velocity = -bulletOrigin.transform.up * bulletSpeed;
                Destroy(clone.gameObject, 2);
                lastShot = Time.time;
            }
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
