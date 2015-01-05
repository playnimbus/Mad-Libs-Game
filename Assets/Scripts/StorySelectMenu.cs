using UnityEngine;
using System.Collections;

public class StorySelectMenu : MonoBehaviour {

    public void goToGrandmasCloset()
    {
        Application.LoadLevel("GrandmasCloset");
    }

    public void goToFairyTale()
    {
        Application.LoadLevel("FairyTale");
    }
    public void goToShooter()
    {
        Application.LoadLevel("Shooter");
    }
}
