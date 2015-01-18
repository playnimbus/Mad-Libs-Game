using UnityEngine;
using System.Collections;

public class objectiveController : MonoBehaviour {
    public string currentObjective;
    public float survivalSeconds;
    
    /* Key Objective Variables */
    bool keyDropped;
    bool keyPickedUp;

    int enemiesKilled;
    int totalEnemiesNeeded;

    GameObject TheKey;

    /* Other Variables */


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        survivalRoom();
        
        //Running the update functoins for the current objectives. 
	    switch (currentObjective)
        {
            case "FindKey" :
                {
                    objectiveKeyUpdate();
                    break;
                }
            case "Survival" :
                {
                    break;
                }
        }
	}

    void survivalRoom()
    {
        if (currentObjective == "Survival")
        {
            survivalSeconds -= Time.deltaTime;

            if (survivalSeconds == 0)
            {
                //GG MATE. //lol hi jacob//
                survivalSeconds += 30;
            }
        }
    }

    void OnGUI()
    {
        if (survivalSeconds <= 9)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.width / 2, 60, 60), "0:0" + (int)survivalSeconds + "");
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.width / 2, 60, 60), "0:" + (int)survivalSeconds + "");
        }
    }

    public void SwitchObjective (string objective)
    {
        //Setting the previous objective and calling the end functions for all previous objectives. 
        string previousObjective;
        previousObjective = currentObjective;

        switch (previousObjective)
        {
            case "FindKey" :
                {
                    objectiveKeyEnd();
                    break;
                }
            case "Survival" :
                {
                    break;
                }
        }
        
        //Switching over to the new objectives and calling their start functions. 
        currentObjective = objective;
        switch (currentObjective)
        {
            case "FindKey":
                {
                    objectiveKeyStart();
                    break;
                }
            case "Survival":
                {
                    break;
                }
        }
    }

    /*Key Objective */
    void objectiveKeyStart()
    {
        keyPickedUp = false;
        keyDropped = false;
    }
    void objectiveKeyUpdate()
    {
        if (keyPickedUp)
        {
            GameObject.Find("ExitDoor").SendMessage("UnlockDoor");
        }

    }
    void objectiveDropKey(GameObject enemyObject)
    {
        if (enemiesKilled >= totalEnemiesNeeded)
        {
            GameObject key = (GameObject)Instantiate(TheKey, new Vector3(enemyObject.transform.position.x,
            enemyObject.transform.position.y,
            enemyObject.transform.position.z),
            new Quaternion(0f, 0f, 0f, 0f));
        }
        
    }
    void objectiveKeyEnd()
    {

    }
    /*Survival Objective */
}
