using UnityEngine;
using System.Collections;

public class objectiveController : MonoBehaviour {
    public string currentObjective;

<<<<<<< HEAD
    public float survivalSeconds;
=======
    /* Key Objective Variables */
    bool keyDropped;
    bool keyPickedUp;

    /* Other Variables */

>>>>>>> origin/master
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        survivalRoom();
=======
        
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
>>>>>>> origin/master
	}

    void survivalRoom()
    {
        survivalSeconds -= Time.deltaTime;
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

    }
    void objectiveKeyUpdate()
    {
        
        if (keyPickedUp)
        {
            GameObject.Find("ExitDoor").SendMessage("UnlockDoor");
        }

    }
    void objectiveKeyEnd()
    {

    }

}
