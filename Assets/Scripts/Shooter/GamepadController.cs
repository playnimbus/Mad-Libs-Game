using UnityEngine;
using System.Collections;

public class GamepadController : MonoBehaviour {
    public GameObject playerWeapon;
    public GameObject bulletOrigin;
    public GameObject bullet;
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

        if (vNewInput.sqrMagnitude < 0.1f)
        {
            return;
        }

        var angle = Mathf.Atan2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical")) * Mathf.Rad2Deg;
        playerWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            Rigidbody2D clone;
            clone = Instantiate(bullet, bulletOrigin.transform.position, transform.rotation) as Rigidbody2D;
            clone.velocity = transform.TransformDirection(Vector3.up * 3);
            Destroy(clone.gameObject, 2);

        }
    }
}
