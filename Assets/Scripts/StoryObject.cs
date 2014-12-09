using UnityEngine;
using System.Collections;
using Parse;

public class StoryObject {

    public string[] StoryChunks;
    public string[] Blanks;
    public string StoryName;
    public string RoomID;

    public StoryObject(string _storyName, string[] _storyChunks, string[] _Blanks)
    {
        StoryChunks = _storyChunks;
        Blanks = _Blanks;
        StoryName = _storyName;
        RoomID = "NIMBUS";
    }

    public void SendStoryToParse()
    {
        for (int i = 0; i < Blanks.Length; i++)
        {
            ParseObject StoryInit = new ParseObject("StoryInit");
            StoryInit["StoryName"] = StoryName;
            StoryInit["BlankHint"] = Blanks[i];
            StoryInit["BlankLocation"] = i;
            StoryInit["RoomID"] = RoomID;
            StoryInit.SaveAsync();
        }
    }

}
