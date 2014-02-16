using UnityEngine;
using System.Collections;

public class EndGameScreenScript : MonoBehaviour {
    public bool WasSuccessful = false;

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
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200), Message, ProgressController.Instance.SuccessNoticeStyle);
        }
        else
        {
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200), Message, ProgressController.Instance.FailNoticeStyle);
        }
    }
}
