using UnityEngine;
using System.Collections;

public class TrialNoteSceneScript : MonoBehaviour {
    public GUIStyle BackButtonStyle = new GUIStyle();
    public GUIStyle TextStyle = new GUIStyle();
    public GUIStyle BuyButtonStyle = new GUIStyle();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(25, 25, 100, 100), "", BackButtonStyle))
        {
            Application.LoadLevel("MainMenuScene");
        }

        GUI.TextArea(new Rect(150, 150, Screen.width - 300, Screen.height - 300), AboutNote, TextStyle);

        if (GUI.Button(new Rect(Screen.width - 125, Screen.height - 125, 100, 100), "", BuyButtonStyle))
        {
            UnityEngine.Windows.LicenseInformation.PurchaseApp();
        }
    }
    string AboutNote = "Oh dear! You have finished everything available on the trial version of SokoBomber...\n\n" +
        "If you liked it and would like to buy it, click the button on the lower right to be sent to the Store to purchase the full version.\n\n" +
        "If you didn't like it, get in touch with us and let us know why!";
}
