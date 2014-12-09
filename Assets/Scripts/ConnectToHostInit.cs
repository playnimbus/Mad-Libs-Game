using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class ConnectToHostInit : MonoBehaviour {

    public GameObject inputTextOBJ;
    public GameObject HeaderTxtObj;
    Text inputText;
    Text headerText;
    float timer = 0;
    bool objGrabbed = false;

    ParseObject currentParseObject;

	// Use this for initialization
	void Start () {

        inputText = inputTextOBJ.GetComponentInChildren<Text>();
        headerText = HeaderTxtObj.GetComponent<Text>();

        getObject();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (objGrabbed == true)
        {
            setupTextFields();
            objGrabbed = false;
        }
	}

    void getObject()
    {
        ParseQuery<ParseObject> query = ParseObject.GetQuery("StoryInit");
        query.FirstAsync().ContinueWith(t =>
        {
            currentParseObject = t.Result;
            string test = currentParseObject.Get<string>("BlankHint");
            int num = currentParseObject.Get<int>("BlankLocation");
            Debug.Log(test + " " + num.ToString() + " " + timer.ToString());

            objGrabbed = true;
        });
    }

    void setupTextFields()
    {
        headerText.text = currentParseObject.Get<string>("BlankHint") + " " + currentParseObject.Get<int>("BlankLocation") + " In: " + timer.ToString() + " seconds.";
        inputTextOBJ.SetActive(true);
        inputText.text = currentParseObject.Get<string>("BlankHint");
    }

    public void sendInputToParse()
    {
        Debug.Log("Sent to parse");
    }
}
