using UnityEngine;
using System.Collections;

public class RoomExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            GameObject.Find("SceneManager").GetComponent<sceneManager>().GoToNextRoom();
        }
    }
}
