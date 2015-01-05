using UnityEngine;
using System.Collections;

public class MainMenuCotroller : MonoBehaviour {

    public void goToHostScene()
    {
        Application.LoadLevel("Shooter");
    }
    public void goToPainterScene()
    {
        Application.LoadLevel("Painter");
    }
}
