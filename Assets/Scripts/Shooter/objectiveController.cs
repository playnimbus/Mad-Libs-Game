using UnityEngine;
using System.Collections;

public class objectiveController : MonoBehaviour {
    public string currentObjective;

    /* Key Objective Variables */
    bool keyDropped;
    bool keyPickedUp;

    int enemiesKilled;
    int totalEnemiesNeeded;

    GameObject TheKey;
    /* Survival Objective Variables */
    public float survivalSeconds;
    public bool isSurvivalObjective;

    /* Other Variables */


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
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
                    objectiveSurvivalUpdate();
                    break;
                }
        }
	}

    void OnGUI()
    {
        switch (currentObjective)
        {
            case "FindKey" :
                //TODO: Do Key GUI Here...
                break;

            case "Survival" :
                {
                    if (survivalSeconds < 10)
                    {
                        //GG MATE. //lol hi jacob//
                        survivalSeconds += 30;
                        GUI.Label(new Rect(Screen.width / 2, Screen.width / 2, 60, 60), "0:0" + (int)survivalSeconds + "");
                    }
                    else
                    {
                        GUI.Label(new Rect(Screen.width / 2, Screen.width / 2, 60, 60), "0:" + (int)survivalSeconds + "");
                    }
                    break;
                }
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
                    objectiveSurvivalEnd();
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
                    objectiveSurvivalStart();
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
    void objectiveSurvivalStart()
    {
        
    }
    void objectiveSurvivalUpdate()
    {
        survivalSeconds -= Time.deltaTime;
        
        if (survivalSeconds <= 0)
        {
            GameObject.Find("ExitDoor").SendMessage("UnlockDoor");
            survivalSeconds = 0;
        }
    }
    void objectiveSurvivalEnd()
    {
        
    }
}
