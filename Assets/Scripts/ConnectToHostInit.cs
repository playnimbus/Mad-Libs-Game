using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class ConnectToHostInit : MonoBehaviour {

    public GameObject inputTextOBJ;
    public GameObject inputFieldTextOBJ;
    public GameObject inputTextPlaceHolderOBJ;

    public GameObject HeaderTxtObj;
    public GameObject submitBtn;
    Text inputText;
    Text inputPlaceHolderText;

    Text headerText;
    float fetchTimer = 0;
    bool objGrabbed = false;

    ParseObject currentParseObject;

	// Use this for initialization
	void Start () {

        inputText = inputFieldTextOBJ.GetComponent<Text>();
        headerText = HeaderTxtObj.GetComponent<Text>();
        inputPlaceHolderText = inputTextPlaceHolderOBJ.GetComponent<Text>();

        getObject();
    }
	
	// Update is called once per frame
	void Update () {

        fetchTimer += Time.deltaTime;

        if (objGrabbed == true)
        {
            setupTextFields();
            objGrabbed = false;
        }
	}

    void getObject()
    {
        fetchTimer = 0;
        ParseQuery<ParseObject> query = ParseObject.GetQuery("StoryInit");
        query.FirstAsync().ContinueWith(t =>
        {
            currentParseObject = t.Result;
            string test = currentParseObject.Get<string>("BlankHint");
            int num = currentParseObject.Get<int>("BlankLocation");
    //        Debug.Log(test + " " + num.ToString() + " " + fetchTimer.ToString());

            objGrabbed = true;

            currentParseObject.DeleteAsync();
        });
    }

    void setupTextFields()
    {
        headerText.text = currentParseObject.Get<string>("BlankHint") + " " + currentParseObject.Get<int>("BlankLocation") + " In: " + fetchTimer.ToString() + " seconds.";
        submitBtn.SetActive(true);
        inputTextOBJ.SetActive(true);
        inputPlaceHolderText.text = currentParseObject.Get<string>("BlankHint");
    }

    public void sendInputToParse()
    {
        ParseObject StoryItem = new ParseObject("StoryItem");
        StoryItem["StoryName"] = currentParseObject.Get<string>("StoryName");
        StoryItem["UserResponse"] = inputText.text;
        StoryItem["BlankLocation"] = currentParseObject.Get<int>("BlankLocation");
        StoryItem["RoomID"] = currentParseObject.Get<string>("RoomID");
        StoryItem.SaveAsync();

        inputText.text = "";
        submitBtn.SetActive(false);
        inputTextOBJ.SetActive(false);
     
        getObject();
    }
}
