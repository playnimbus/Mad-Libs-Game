using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class sceneManager : MonoBehaviour {

    public ShooterCharacter[] ShooterCharacters;
    public GameObject RoomPrefab;


    //these manage the steps involved with downloading texture from parse
    ParseObject asycedObject;
    bool asyncDownloadComplete = false;
    bool setTexture = false;
    Texture2D downloadedTexture;

    public GameObject startScreen;

    public enum GameStates { menu, playing, switchingRoom };
    public GameStates gameState = GameStates.menu;

	// Use this for initialization
	void Start () {

        clearOldParseData();

        for(int i = 0; i < ShooterCharacters.Length;i++)
        {
            SendCharacterToParse(ShooterCharacters[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {

        switch (gameState)
        {
            case GameStates.menu: menuUpdate(); break;
            case GameStates.playing: playingUpdate(); break;
            case GameStates.switchingRoom: switchingRoomUpdate(); break;
        }
	
	}

    //-------------Menu State------------------------------------

    void menuEnter() { }

    float parsePullCounter = 3;
    void menuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switchState(GameStates.playing);
        }

        parsePullCounter -= Time.deltaTime;
        if (parsePullCounter <= 0)
        {
            downloadTexture();
            parsePullCounter = 3;
        }

        if (setTexture == true)
        {
            string asyncName = asycedObject.Get<string>("name");
            for (int i = 0; i < ShooterCharacters.Length; i++)
            {
                if (ShooterCharacters[i].name == asyncName)
                {
                    if (ShooterCharacters[i].tag == "Enemy")
                    {
                        GameObject[] spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

                        for (int s = 0; s < spawners.Length; s++)
                        {
                            if (spawners[s].name == ShooterCharacters[i].name + "Spawner")
                            {
                                spawners[s].GetComponent<enemySpawner>().enemyTexture = downloadedTexture;
                            }
                        }
                    }
                    //          Debug.Log("names match");
                    //ShooterCharacters[i].chosenPersonality = asycedObject.Get<string>("personality");
                    //         Destroy(DoodleCharacters[i].gameObject.GetComponent<SpriteRenderer>());
                    Sprite tempSprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), new Vector2(0.5f, 0.5f));


                    //for some reaon, assigning a sprite to a spriteRenderer adds a second spriteRenderer to the gameObject. This is my current solution to the problem.
                    SpriteRenderer[] renderers = ShooterCharacters[i].gameObject.GetComponents<SpriteRenderer>();

                    for (int x = 0; x < renderers.Length; x++)
                    {
                        renderers[x].sprite = tempSprite;
                    }
                }
            }

            setTexture = false;
        }

        if (asyncDownloadComplete == true)
        {
            StartCoroutine(wwwRequest(asycedObject));
            asyncDownloadComplete = false;
        }


    }
    void menuExit() 
    {
        startScreen.SetActive(false);
    }


    //-------------Play State------------------------------------
    void playingEnter() 
    {
    }
    void playingUpdate()
    {

    }
    void playingExit() 
    {
    }

    //-------------switchingRoom State------------------------------------

    GameObject oldRoom = null;
    GameObject newRoom = null;

    public float transitionLength;
    float transitionTimer = 0;

    void switchingRoomEnter()
    {
        transitionTimer = 0;
        oldRoom = GameObject.Find("Room");

        newRoom = (GameObject)Instantiate(RoomPrefab);
        newRoom.transform.position = oldRoom.transform.position + new Vector3(0,20,0);
        newRoom.name = "Room";

        iTween.MoveTo(Camera.main.gameObject, newRoom.GetComponent<RoomObjectHolder>().CameraLocation.transform.position, transitionLength);
        
    }
    void switchingRoomUpdate()
    {
        transitionTimer += Time.deltaTime;

        if (transitionTimer >= transitionLength)
        {
            switchState(GameStates.playing);
        }
    }
    void switchingRoomExit()
    {
        GameObject.Destroy(oldRoom);

        iTween.MoveBy(newRoom.GetComponent<RoomObjectHolder>().Door, new Vector3(5.5f, 0, 0), 1f);
        gameObject.GetComponent<objectiveController>().RandomObjective(); //changes the current objective to a random one...
        oldRoom = null;
        newRoom = null;
    }
    //-------------------------------------------------------------

    public void GoToNextRoom()
    {
        switchState(GameStates.switchingRoom);
    }

    void switchState(GameStates newState)
    {
        switch(gameState)
        {
            case GameStates.menu: menuExit(); break;
            case GameStates.playing: playingExit(); break;
            case GameStates.switchingRoom: switchingRoomExit(); break;
        }

        gameState = newState;

        switch (gameState)
        {
            case GameStates.menu: menuEnter(); break;
            case GameStates.playing: playingEnter(); break;
            case GameStates.switchingRoom: switchingRoomEnter(); break;
        }
    }

    public void SendCharacterToParse(ShooterCharacter character)
    {
        ParseObject ShooterInit = new ParseObject("ShooterInit");
        ShooterInit["name"] = character.name;
 //       StoryInit["personality1"] = character.personality1;
 //       StoryInit["personality2"] = character.personality2;
 //       StoryInit["personality3"] = character.personality3;
        ShooterInit.SaveAsync();
    }

    void clearOldParseData()
    {
        ParseQuery<ParseObject> query = ParseObject.GetQuery("ShooterCharacterComplete");
        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;

            foreach (var result in results)
            {
                result.DeleteAsync();
            }
            
        });

        ParseQuery<ParseObject> query2 = ParseObject.GetQuery("ShooterInit");
        query2.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;

            foreach (var result in results)
            {
                result.DeleteAsync();
            }
        });
    }

    public void downloadTexture()
    {
        ParseQuery<ParseObject> query = ParseObject.GetQuery("ShooterCharacterComplete");
        query.FirstAsync().ContinueWith(t =>
        {
            asycedObject = t.Result;
            //          Debug.Log("texture found: " + asycedObject.Get<string>("name"));
            asyncDownloadComplete = true;
            t.Result.DeleteAsync();
        });
    }
    public IEnumerator wwwRequest(ParseObject obj)
    {
        //       Debug.Log("entered www");

        var imageFile = obj.Get<ParseFile>("texture");
        //       Debug.Log("image url: " + imageFile.Name);
        var imageRequest = new WWW(imageFile.Url.ToString());

        yield return imageRequest;

        //       Debug.Log("www complete");

        downloadedTexture = imageRequest.texture;
        setTexture = true;
    }
}
