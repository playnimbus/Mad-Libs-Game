using UnityEngine;
using System.Collections;

public class objectiveController : MonoBehaviour {
    public string currentObjective;

    /* Key Objective Variables */
    bool keyDropped;
    bool keyPickedUp;

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
        
    }
    void objectiveKeyEnd()
    {

    }

}
