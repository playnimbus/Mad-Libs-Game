using UnityEngine;
using System.Collections;
using Parse;

public class sceneManager : MonoBehaviour {

    public ShooterCharacter[] ShooterCharacters;

    //these manage the steps involved with downloading texture from parse
    ParseObject asycedObject;
    bool asyncDownloadComplete = false;
    bool setTexture = false;
    Texture2D downloadedTexture;

    public GameObject startScreen;

    public enum GameStates { menu, playing };

    public GameStates gameState = GameStates.menu;

	// Use this for initialization
	void Start () {
	
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

                    if (ShooterCharacters[i].name == "enemy")
                    {
                        GameObject[] spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

                        for (int s = 0; s < spawners.Length; s++)
                        {
                            spawners[s].GetComponent<enemySpawner>().enemyTexture = downloadedTexture;
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
    //-------------------------------------------------------------


    void switchState(GameStates newState)
    {
        switch(gameState)
        {
            case GameStates.menu: menuExit(); break;
            case GameStates.playing: playingExit(); break;
        }

        gameState = newState;

        switch (gameState)
        {
            case GameStates.menu: menuEnter(); break;
            case GameStates.playing: playingEnter(); break;
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
