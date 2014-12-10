using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Parse;

public class StoryGrandmasCloset : MonoBehaviour {

    /*----- Story -------
     I was in my Grandma <First Name> closet, looking for a <Noun> and you will never guess what I found in there.
     First, when I was walking through I saw a <Adjective> <Adjective> thing in the corner. When I got closer I 
     realized it was a <Adjective> stinky, <Adjective> <Article of clothing>. After throwing it back in the corner, 
     I came across a bright red lacy, <Article of clothing> I didn't even wanna think why my Grandma had that 
     in her closet. But, the most shocking thing I discovered was a BIG, <Adjective> <Adjective> condom. Now, I
     know too never ever go in my grandma's closet again.
     * */

    /*---- Blanks ------
      0: <First Name>
      1: <Noun>
      2: <Adjective>
      3: <Adjective>
      4: <Adjective>
      5: <Adjective>
      6: <Article of clothing>
      7: <Article of clothing>
      8: <Adjective>
      9: <Adjective>
     * */

    public GameObject showStoryBtn;

    const string StoryName = "Grandmas Closet";
    StoryObject grandMasCloset;
    List<string> userResponses;

    bool storyFound = false;

    // Use this for initialization
	void Start () {

        userResponses = new List<string>();

        string[] blanks = {
                       /*0*/"First Name",
                       /*1*/"Noun",
                       /*2*/"Adjective",
                       /*3*/"Adjective",
                       /*4*/"Adjective",
                       /*5*/"Adjective",
                       /*6*/"Article of clothing",
                       /*7*/"Article of clothing",
                       /*8*/"Adjective",
                       /*9*/"Adjective"};

        string[] story = {
                     /*0*/"I was in my Grandma",
                     /*1*/"closet, looking for a",
                     /*2*/"and you will never guess what I found in there.First, when I was walking through I saw a",
                     /*3*/"thing in the corner. When I got closer I realized it was a",
                     /*4*/"stinky,",
                     /*5*/". After throwing it back in the corner, I came across a bright red lacy,",
                     /*6*/"I didn't even wanna think why my Grandma had that in her closet. But, the most shocking thing I discovered was a BIG,",
                     /*7*/"condom. Now, I know too never ever go in my grandma's closet again."
                         };



        grandMasCloset = new StoryObject(StoryName, story, blanks);
        grandMasCloset.SendStoryToParse();
	}
	
	// Update is called once per frame
	void Update () {


        if (storyFound == true)
        {
            printStory();
            storyFound = false;
        }
	}

    public void showStory()
    {
        showStoryBtn.SetActive(false);

        ParseQuery<ParseObject> query = ParseObject.GetQuery("StoryItem").OrderBy("BlankLocation");
        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;

            foreach (var result in results)
            {
                userResponses.Add(result["UserResponse"].ToString());
                storyFound = true;
                result.DeleteAsync();
            }

        });
    }

    void printStory()
    {
        GameObject.Find("StoryOutput").GetComponent<Text>().text = grandMasCloset.StoryChunks[0] + " " +
                                                                    userResponses[0] + " " +
                                                                    grandMasCloset.StoryChunks[1] + " " +
                                                                    userResponses[1] + " " +
                                                                    grandMasCloset.StoryChunks[2] + " " +
                                                                    userResponses[2] + " " +
                                                                    userResponses[3] + " " +
                                                                    grandMasCloset.StoryChunks[3] + " " +
                                                                    userResponses[4] + " " +
                                                                    grandMasCloset.StoryChunks[4] + " " +
                                                                    userResponses[5] + " " +
                                                                    userResponses[6] + " " +
                                                                    grandMasCloset.StoryChunks[5] + " " +
                                                                    userResponses[7] + " " +
                                                                    grandMasCloset.StoryChunks[6] + " " +
                                                                    userResponses[8] + " " +
                                                                    userResponses[9] + " " +
                                                                    grandMasCloset.StoryChunks[7];
    }
}
