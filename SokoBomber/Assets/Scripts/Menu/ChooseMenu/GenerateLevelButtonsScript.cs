using UnityEngine;
using System.Collections;

public class GenerateLevelButtonsScript : MonoBehaviour {
	public GameObject buttonTemplate = null;

    public GameObject UpTemplate = null;
    public GameObject DownTemplate = null;

    public float ScrollSpeed = 4.5f;

	// Use this for initialization
	void Start () {
		int currentProgress = ProgressController.Instance.CompletionProgress;

		int state1 = 0;
		int state2 = -1;
		for (int i = 0; i < currentProgress + 1; i++)
		{
			state2 += 1;
			var o = Instantiate(buttonTemplate, new Vector3(2f * (state2 - 1), state1 * -2f, 0), Quaternion.identity) as GameObject;
			var cmpt = o.GetComponent<LevelButtonScript>();
			
            cmpt.LevelToLoadId = i + 1;

            cmpt.LevelToLoad = "Level" + cmpt.LevelToLoadId.ToString() + "Scene";

            ProgressController.Instance.SpawnStarAt(ProgressController.Instance.ReadStarLevel(i + 1), new Vector3(2f * (state2 - 1) - 0.5f, state1 * -2f +0.5f, 0));

			if (state2 == 2)
			{
				state2 = -1;
				state1 += 1;
			}
		}

        MaxOffset = 2f * state1;
        CurrentOffset = MaxOffset;

        var objs = GameObject.FindGameObjectsWithTag("LevelButton");

        var offsetAmmount = new Vector3(0, MaxOffset, 0);
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].transform.Translate(offsetAmmount);
        }


        Instantiate(UpTemplate, upButton, Quaternion.identity);
        Instantiate(DownTemplate, downButton, Quaternion.identity);
	}

    Vector3 upButton = new Vector3(-4f, 2f, 0f);
    Vector3 downButton = new Vector3(-4f, 0, 0f);

	// Update is called once per frame
	void Update () {
        Ray sptoRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 inWorld = sptoRay.GetPoint(-sptoRay.origin.z);

        if (Input.GetMouseButton(0))
        {

            if ((inWorld.x > upButton.x - 0.75) && (inWorld.x < upButton.x + 0.75) &&
                    (inWorld.y > upButton.y - 0.75) && (inWorld.y < upButton.y + 0.75))
            {
                var objs = GameObject.FindGameObjectsWithTag("LevelButton");

                var offsetAmmount = new Vector3(0, -ScrollSpeed, 0) * Time.deltaTime;
                if (offsetAmmount.y + CurrentOffset > MinOffset)
                {
                    CurrentOffset += offsetAmmount.y;
                    for (int i = 0; i < objs.Length; i++)
                    {
                        objs[i].transform.Translate(offsetAmmount);
                    }
                }
            }

            if ((inWorld.x > downButton.x - 0.75) && (inWorld.x < downButton.x + 0.75) &&
                    (inWorld.y > downButton.y - 0.75) && (inWorld.y < downButton.y + 0.75))
            {
                var objs = GameObject.FindGameObjectsWithTag("LevelButton");

                var offsetAmmount = new Vector3(0, ScrollSpeed, 0) * Time.deltaTime;

                if (offsetAmmount.y + CurrentOffset < MaxOffset)
                {
                    CurrentOffset += offsetAmmount.y;
                    for (int i = 0; i < objs.Length; i++)
                    {
                        objs[i].transform.Translate(offsetAmmount);
                    }
                }
            }
        }
	}

    float MaxOffset = 7f;
    float MinOffset = 0f;
    float CurrentOffset = 0f;

    void OnGUI()
    {
        //Vector3 wtspUp = Camera.main.WorldToScreenPoint(upButton);
        //Vector3 wtspDown = Camera.main.WorldToScreenPoint(downButton);
        //
        //GUI.TextArea(new Rect(wtspUp.x - 15, Screen.height - wtspUp.y - 50, 100, 100), "Up");
        //GUI.TextArea(new Rect(wtspDown.x - 15, Screen.height - wtspDown.y - 50, 100, 100), "Down");

        //if (GUI.Button(new Rect(0, 0, 100, 100), "Up", ProgressController.Instance.ButtonStyle))
        //{
        //    var objs = GameObject.FindGameObjectsWithTag("LevelButton");
        //
        //    var offsetAmmount = new Vector3(0, -4.5f, 0) * Time.deltaTime;
        //    if (offsetAmmount.y + CurrentOffset > MinOffset)
        //    {
        //        CurrentOffset += offsetAmmount.y;
        //        for (int i = 0; i < objs.Length; i++)
        //        {
        //            objs[i].transform.Translate(offsetAmmount);
        //        }
        //    }
        //}
        //if (GUI.Button(new Rect(0, 100, 100, 100), "Down", ProgressController.Instance.ButtonStyle))
        //{
        //    var objs = GameObject.FindGameObjectsWithTag("LevelButton");
        //
        //    var offsetAmmount = new Vector3(0, 4.5f, 0) * Time.deltaTime;
        //
        //    if (offsetAmmount.y + CurrentOffset < MaxOffset)
        //    {
        //        CurrentOffset += offsetAmmount.y;
        //        for (int i = 0; i < objs.Length; i++)
        //        {
        //            objs[i].transform.Translate(offsetAmmount);
        //        }
        //    }
        //}

        if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 100, 100), "Back To Menu", ProgressController.Instance.ButtonStyle))
        {
            Application.LoadLevel("MainMenuScene");
        }
    }
}
