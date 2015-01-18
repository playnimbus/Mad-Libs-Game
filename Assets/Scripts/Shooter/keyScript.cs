using UnityEngine;
using System.Collections;

public class keyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("SceneManager").GetComponent<objectiveController>().objectiveKeyPickup(true); //Picks up the key.
            //TODO: Eventually turn this into an inventory system? 
            GameObject.Destroy(gameObject);
        }
    }
    
}
