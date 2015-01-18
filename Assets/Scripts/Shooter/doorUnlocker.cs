using UnityEngine;
using System.Collections;

public class doorUnlocker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
        iTween.MoveBy(gameObject, new Vector3(-5.5f, 0, 0), 1f);
    }
}
