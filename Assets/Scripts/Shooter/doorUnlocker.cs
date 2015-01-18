using UnityEngine;
using System.Collections;

public class doorUnlocker : MonoBehaviour {
    Vector3 startPosition;
	// Use this for initialization
	void Start () {
        //Start Position Is Set So It Never Changes When The Door Unlocks
        startPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug Door Unlocking!
	    if (Input.GetKeyDown(KeyCode.J))
        {
            UnlockDoor();
        }
	}

    public void UnlockDoor()
    {
        iTween.MoveTo(gameObject, new Vector3(startPosition.x + -5.5f, 
            startPosition.y, startPosition.z), 1f);
    }
}
