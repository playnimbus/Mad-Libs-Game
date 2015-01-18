using UnityEngine;
using System.Collections;

public class objectiveController : MonoBehaviour {
    public string currentObjective;

    /* Key Objective Variables */
    bool keyDropped;
    bool keyPickedUp;

    int enemiesKilled;
    int totalEnemiesNeeded;
    public int startingTotalEnemiesNeeded = 10;

    public GameObject TheKey;

    /* Survival Objective Variables */
    public float survivalSeconds;
    public bool isSurvivalObjective;

    /* Other Variables */


	// Use this for initialization
	void Start () {
        Random.seed = (int)System.DateTime.Now.ToBinary(); //Seeding the random number generator so we don't always have the same random number calls.
	}
	
	// Update is called once per frame
	void Update () {

        //Running the update functoins for the current objectives. 
	    switch (currentObjective)
        {
            case "" : //Usually on start there won't be an objective, or if there just happens to be an empty objective for some reason, it'll find a new one. 
                RandomObjective();
                break;
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

    public void RandomObjective()
    {
        int randomObjective = Random.Range(0, 2); //This needs to be updated for each objective. TODO: Mike, figure a better way to do this whole thing.
        switch (randomObjective)
        {
            case 0:
                SwitchObjective("FindKey");
                break;
            case 1:
                SwitchObjective("Survival");
                break;
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
        Debug.Log("Previous Objective: " + previousObjective);
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
                    Debug.Log("Survival Started");
                    break;
                }
        }
        Debug.Log("Current Objective: " + currentObjective);
    }

    /*Key Objective */
    void objectiveKeyStart()
    {
        keyPickedUp = false;
        keyDropped = false;
        enemiesKilled = 0;
        totalEnemiesNeeded = startingTotalEnemiesNeeded;
    }
    void objectiveKeyUpdate()
    {
        if (keyPickedUp && currentObjective == "FindKey") //JustMakinSure that that its on the right objective. Also, right now debugs on keyDrop and not pickup.
        {
            GameObject.Find("ExitDoor").SendMessage("UnlockDoor");
        }
    }
    
    public void objectiveDropKey(GameObject enemyObject) //Can Be Called From Any Object That May Want To Drop Key
    {
        if (enemiesKilled >= totalEnemiesNeeded && !keyDropped && currentObjective == "FindKey")
        {
            GameObject key = (GameObject)Instantiate(TheKey, new Vector3(enemyObject.transform.position.x,
            enemyObject.transform.position.y,
            enemyObject.transform.position.z),
            new Quaternion(0f, 0f, 0f, 0f));
            keyDropped = true;
        } 
    }
    
    public void objectiveKeyPickup(bool keyState)
    {
        keyPickedUp = keyState;
    }

    public void objectiveAddEnemiesKilled(int amount)
    {
        enemiesKilled += amount;
    }

    void objectiveKeyEnd()
    {
        enemiesKilled = 0;
        keyDropped = false;
        keyPickedUp = false;
    }

    /* Survival Objective */
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
        survivalSeconds = 30;
    }
}
