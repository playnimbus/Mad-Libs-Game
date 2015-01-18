using UnityEngine;
using System.Collections;

public class objectiveController : MonoBehaviour {
    public string currentObjective;

    public float survivalSeconds;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        survivalRoom();
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
        currentObjective = objective;
    }

}
