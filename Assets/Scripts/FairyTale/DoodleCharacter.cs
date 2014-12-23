using UnityEngine;
using System.Collections;
using Parse;

public class DoodleCharacter: MonoBehaviour{

    //user chosen personalities are saved as ints and will need to translated in the FairyTale story script
    public string personality1;
    public string personality2;
    public string personality3;

    public string chosenPersonality;
    /*
    public DoodleCharacter(string _name, string _personality1, string _personality2, string _personality3, Texture2D _texture)
    {
        name = _name;
        texture = _texture;
        personality1 = _personality1;
        personality2 = _personality2;
        personality3 = _personality3;
    }
    */
    public void SendCharacterToParse()
    {
        ParseObject StoryInit = new ParseObject("DoodleStoryInit");
        StoryInit["name"] = gameObject.name;
        StoryInit["personality1"] = personality1;
        StoryInit["personality2"] = personality2;
        StoryInit["personality3"] = personality3;
        StoryInit.SaveAsync();
    }

}
