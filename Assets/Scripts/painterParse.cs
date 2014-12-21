using UnityEngine;
using System.Collections;
using Parse;
using Parse.Internal;

public class painterParse : MonoBehaviour {

    public Texture2D texture;

    ParseObject asycedObject;

    bool asyncDownloadComplete = false;
    bool sendTexture = false;

    Texture2D downloadedTexture;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
        if (sendTexture == true)
        {
            gameObject.SendMessage("SetTexture", downloadedTexture);
            sendTexture = false;
            Debug.Log("texture sent to painter");
        }

        if (asyncDownloadComplete == true)
        {
            StartCoroutine(wwwRequest(asycedObject));
            asyncDownloadComplete = false;
        }

	}

    public void uploadTexture()
    {
        byte[] data = texture.EncodeToPNG();
        ParseFile parseFile = new ParseFile("texture", data);
        parseFile.SaveAsync().ContinueWith( t => {

            ParseObject painterItem = new ParseObject("PainterItem");
            painterItem["Texture"] = parseFile;
            painterItem.SaveAsync();
        });
    }

    public void downloadTexture()
    {
        ParseQuery<ParseObject> query = ParseObject.GetQuery("PainterItem");
        query.FirstAsync().ContinueWith(t =>
        {
            Debug.Log("query complete");
            asycedObject = t.Result;
            asyncDownloadComplete = true;
        });
    }

    public IEnumerator wwwRequest(ParseObject obj)
    {
        Debug.Log("entered www");

        var imageFile = obj.Get<ParseFile>("Texture");
        Debug.Log("image url: " + imageFile.Name);
        var imageRequest = new WWW(imageFile.Url.ToString());



        yield return imageRequest;

        Debug.Log("www complete");

        downloadedTexture = imageRequest.texture;
        sendTexture = true;
    }


}
