using UnityEngine;
using System.Collections;

public class MainMenuCotroller : MonoBehaviour {

    public void goToHostScene()
    {
        Application.LoadLevel("HostGame");
    }
    public void goToConnectScene()
    {
        Application.LoadLevel("ConnectToGame");
    }
    public void goToPainterScene()
    {
        Application.LoadLevel("Painter");
    }
}
