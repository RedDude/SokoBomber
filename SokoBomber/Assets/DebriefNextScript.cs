using UnityEngine;
using System.Collections;

public class DebriefNextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Ray sptoRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 inWorld = sptoRay.GetPoint(-sptoRay.origin.z);

            if ((inWorld.x > this.transform.position.x - 0.75) && (inWorld.x < this.transform.position.x + 0.75) &&
                    (inWorld.y > this.transform.position.y - 0.75) && (inWorld.y < this.transform.position.y + 0.75))
            {
                var obj = GameObject.FindGameObjectWithTag("Finish");
                var cmpt = obj.GetComponent<EndGameScreenScript>();

                if (cmpt.WasSuccessful)
                {
                    if (ProgressController.Instance.LoadedLevel < ProgressController.Instance.TotalLevelsCount - 1)
                    {
                        ProgressController.Instance.LoadedLevel += 1;
                        ScreenShakeManager.shakeInt = 0;
                        Application.LoadLevel("Level" + (ProgressController.Instance.LoadedLevel).ToString() + "Scene");
                    }
                    else
                    {
                        ScreenShakeManager.shakeInt = 0;
                        Application.LoadLevel("ChooseLevelScene");
                    }
                }
                else
                {
                    Application.LoadLevel(Application.loadedLevelName);
                }
            }
        }
	}
}
