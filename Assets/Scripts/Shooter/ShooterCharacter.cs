using UnityEngine;
using System.Collections;
using Parse;

public class ShooterCharacter : MonoBehaviour {

    //user chosen personalities are saved as ints and will need to translated in the FairyTale story script
    //   public string personality1;
    //   public string personality2;
    //   public string personality3;

    public string chosenPersonality;

    public void SendCharacterToParse()
    {
        ParseObject ShooterInit = new ParseObject("ShooterInit");
        ShooterInit["name"] = gameObject.name;
        //        StoryInit["personality1"] = personality1;
        //       StoryInit["personality2"] = personality2;
        //      StoryInit["personality3"] = personality3;
        ShooterInit.SaveAsync();
    }

}
