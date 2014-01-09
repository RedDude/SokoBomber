using UnityEngine;
using System.Collections;

public class EndGameScreenScript : MonoBehaviour {
    public bool WasSuccessful = false;

    private string Message = "No message? Something went wrong!";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void Success(int msg)
    {
        switch (msg)
        {
            case 1: Message = "Oh no, you died! But that is quite alright, just remember bombs can kill you!"; break;
            default: Message = "You successfully completed the level!"; break;
        }
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
        GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 100), Message);

        if (WasSuccessful)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 100, 100), "Next Level"))
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
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 100, 100), "Choose Level"))
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
