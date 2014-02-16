using UnityEngine;
using System.Collections;

public class AboutSceneNotes : MonoBehaviour {
    public GUIStyle BackButtonStyle = new GUIStyle();
    public GUIStyle TextStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(25, 25, 100, 100), "", BackButtonStyle))
        {
            Application.LoadLevel("MainMenuScene");
        }

        GUI.TextArea(new Rect(0, 0, Screen.width, Screen.height), AboutNote, TextStyle);
    }
    string AboutNote = "SokoBomber v1.0\n" +
                       "Created by:\n" +
                       "Ernest Loveland\n" +
                       "Paul Lombard\n" +
                       "\n" +
                       "Special thanks to the Make Games SA community for their feedback and support!\n" +
                       "\n" +
                       "Privacy Policy:\n" +
                       "This game does not have access to (and will never ask for) your private details. Any data collected is only done so anonymously and only includes the following analytics data:\n" +
                       "Game launches\n" +
                       "Turns taken to complete a level\n" +
                       "Level starts/restarts/quits\n" +
                       "\n" +
                       "No other information is accessed or transmitted at any point in time.";
}
