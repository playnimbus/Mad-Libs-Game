using UnityEngine;
using System.Collections;
using Parse;

public class StoryFairyTale : MonoBehaviour {

    public DoodleCharacter[] DoodleCharacters;

    ParseObject asycedObject;

    bool asyncDownloadComplete = false;
    bool setTexture = false;

    Texture2D downloadedTexture;

    float fetchCountdown = 3;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < DoodleCharacters.Length; i++)
        {
            SendCharacterToParse(DoodleCharacters[i]);
        }

	}

    // Update is called once per frame
    void Update()
    {
        fetchCountdown -= Time.deltaTime;
        if (fetchCountdown <= 0)
        {
            downloadTexture();
            fetchCountdown = 3;
        }

        if (setTexture == true)
        {
            string asyncName = asycedObject.Get<string>("name");
            for (int i = 0; i < DoodleCharacters.Length; i++)
            {
                if (DoodleCharacters[i].name == asyncName)
                {
          //          Debug.Log("names match");
                    DoodleCharacters[i].chosenPersonality = asycedObject.Get<string>("personality");
           //         Destroy(DoodleCharacters[i].gameObject.GetComponent<SpriteRenderer>());

                    Sprite tempSprite = Sprite.Create( downloadedTexture, new Rect(0,0,downloadedTexture.width,downloadedTexture.height),new Vector2(0.5f,0.5f)) ;


                    //for some reaon, assigning a sprite to a spriteRenderer adds a second spriteRenderer to the gameObject. This is my current solution to the problem.
                    SpriteRenderer[] renderers = DoodleCharacters[i].gameObject.GetComponents<SpriteRenderer>();

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

    public void SendCharacterToParse(DoodleCharacter character)
    {
        ParseObject StoryInit = new ParseObject("DoodleStoryInit");
        StoryInit["name"] = character.name;
        StoryInit["personality1"] = character.personality1;
        StoryInit["personality2"] = character.personality2;
        StoryInit["personality3"] = character.personality3;
        StoryInit.SaveAsync();
    }

    public void downloadTexture()
    {
        ParseQuery<ParseObject> query = ParseObject.GetQuery("DoodleCharacterComplete");
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
