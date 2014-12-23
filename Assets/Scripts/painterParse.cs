using UnityEngine;
using System.Collections;
using Parse;
using Parse.Internal;
using UnityEngine.UI;

public class painterParse : MonoBehaviour {

    public Texture2D texture;

    public Text name;
    public Text personalityText1;
    public Text personalityText2;
    public Text personalityText3;

    public GameObject uploadBtn;

    string personalityToUpload = "";

    ParseObject currentPulledParseObject;
    bool objGrabbed = false;
    float fetchTimer = 0;

	// Use this for initialization
	void Start () {
        getObject();

	}
	
	// Update is called once per frame
	void Update () {

        //pulling doodleCharacterInit
        fetchTimer += Time.deltaTime;
        if (objGrabbed == true)
        {
            //setupTextFields();
            Debug.Log("Fetch Time: " + fetchTimer);

            name.text = currentPulledParseObject.Get<string>("name");
            personalityText1.text = currentPulledParseObject.Get<string>("personality1");
            personalityText2.text = currentPulledParseObject.Get<string>("personality2");
            personalityText3.text = currentPulledParseObject.Get<string>("personality3");

            objGrabbed = false;
        }
	}

    void getObject()
    {
        fetchTimer = 0;
        ParseQuery<ParseObject> query = ParseObject.GetQuery("DoodleStoryInit");
        query.FirstAsync().ContinueWith(t =>
        {
            currentPulledParseObject = t.Result;
            objGrabbed = true;

            currentPulledParseObject.DeleteAsync();
        });
    }

    public void uploadTexture()
    {
        personalityToUpload = getPersonalityChoice();
        uploadBtn.SetActive(false);

        byte[] data = texture.EncodeToPNG();
        ParseFile parseFile = new ParseFile("texture", data);
        parseFile.SaveAsync().ContinueWith( t => {

            ParseObject DoodleCharacterComplete = new ParseObject("DoodleCharacterComplete");
            DoodleCharacterComplete["texture"] = parseFile;
            DoodleCharacterComplete["name"] = name.text;
            DoodleCharacterComplete["personality"] = personalityToUpload;
            DoodleCharacterComplete.SaveAsync();
        });
    }

    string getPersonalityChoice()
    {
        Toggle[] toggles = GameObject.Find("Canvas").GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn == true)
            {
                return toggles[i].GetComponentInChildren<Text>().text;
            }
        }
        return "null personality";


    }
}
