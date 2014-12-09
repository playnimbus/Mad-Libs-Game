using UnityEngine;
using System.Collections;

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

    const string StoryName = "Grandmas Closet";

    // Use this for initialization
	void Start () {
  
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



        StoryObject grandMasCloset = new StoryObject(StoryName, story, blanks);
        grandMasCloset.SendStoryToParse();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
