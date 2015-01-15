using UnityEngine;
using System.Collections;

public class GamepadController : MonoBehaviour {
    public GameObject bullet;
    public GameObject playerWeapon;
    public GameObject bulletOrigin;
    public float moveSpeed = 25;

	void Update () {
        GamepadMovement();
        GamepadShooter();
	}

    void GamepadMovement()
    {
        float ySpeed = Input.GetAxis("Vertical") * moveSpeed;
        float xSpeed = Input.GetAxis("Horizontal") * moveSpeed;

        gameObject.rigidbody2D.AddForce(new Vector2(xSpeed, ySpeed));
    }

    void GamepadShooter()
    {
        Vector2 vNewInput = new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
        var angle = Mathf.Atan2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical")) * Mathf.Rad2Deg;
        if (vNewInput.sqrMagnitude < 0.1f)
        {
            return;
        }
        playerWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
       
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            GameObject clone;
            clone = Instantiate(bullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation) as GameObject;
            clone.rigidbody2D.velocity = bulletOrigin.transform.up * -25;
            Destroy(clone.gameObject, 2);
        }
    }
}
