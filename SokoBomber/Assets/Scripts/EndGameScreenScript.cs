using UnityEngine;
using System.Collections;

public class EndGameScreenScript : MonoBehaviour {
    public bool WasSuccessful = false;

    public GUIStyle goodStyle = new GUIStyle();
    public GUIStyle badStyle = new GUIStyle();

    private string Message = "No message? Something went wrong!";

	// Use this for initialization
	void Start () {
		//check if another instance exists:
        var o = GameObject.FindGameObjectWithTag("Finish");
        if (o != null && o != this.gameObject)
        {
            Destroy(o);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    void Success(int msg)
    {
        switch (msg)
        {
            case 1: Message = "Oh no, you died! But that is quite alright, just remember bombs can kill you!"; break;
            case 2: Message = "It looks like these small bombs fall through holes, but that is quite alright. Just remember this in future!"; break;
            default: Message = "You successfully completed the level!"; break;
        }

        WasSuccessful = true;
    }

    void Failure(int msg)
    {
        switch (msg)
        {
            default: Message = "Oh no, you failed! Keep at it, you will get it!"; break;
        }
    }

    void OnGUI()
    {
        
        if (WasSuccessful)
        {
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 100), Message, goodStyle);

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 100, 100), "Next Level", goodStyle))
            {
                Application.LoadLevel("ChooseLevelScene");
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 100, 100, 100), "Replay"))
            {
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
        else
        {
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 100), Message, badStyle);

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 100, 100), "Choose Level", badStyle))
            {
                Application.LoadLevel("ChooseLevelScene");
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 100, 100, 100), "Retry"))
            {
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }
}
