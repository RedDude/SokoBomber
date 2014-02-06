using UnityEngine;
using System.Collections;

public class LevelButtonScript : MonoBehaviour
{
    public string LevelToLoad = "";
    public int LevelToLoadId = -1;

    public GUIStyle Style = new GUIStyle();

    // Use this for initialization
    void Start()
    {
        if (LevelToLoadId <= ProgressController.Instance.CompletionProgress + 1 || LevelToLoadId == 1)
        {
            ProgressController.Instance.SpawnStarAt(ProgressController.Instance.ReadStarLevel(LevelToLoadId), this.transform.position + new Vector3(0,0.7f,0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector3(0, -3f, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector3(0, 3f, 0) * Time.deltaTime);
        }

        screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        if (LevelToLoadId > ProgressController.Instance.CompletionProgress + 1 && LevelToLoadId > 1)
        {
            //Debug.Log("Can't use button: " + LevelToLoadId.ToString() + ", " + ProgressController.Instance.CompletionProgress.ToString());
            return; //disable buttons
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray sptoRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 inWorld = sptoRay.GetPoint(-sptoRay.origin.z);

            if ((inWorld.x > this.transform.position.x - 0.75) && (inWorld.x < this.transform.position.x + 0.75) &&
                    (inWorld.y > this.transform.position.y - 0.75) && (inWorld.y < this.transform.position.y + 0.75))
            {
                if (UnityEngine.Windows.LicenseInformation.isOnAppTrial && LevelToLoadId > 10)
                {
                    Application.LoadLevel("TrialNoteScene");
                    return;
                }
                ProgressController.Instance.LoadedLevel = LevelToLoadId;

                //AnalyticsHelper.Instance.logEvent("level_" + LevelToLoadId.ToString(), "start", 0);

                Application.LoadLevel(LevelToLoad);
            }
        }

    }

    Vector3 screenPos;

    void OnGUI()
    {
        GUI.TextArea(new Rect(screenPos.x - 0.25f, Screen.height - (screenPos.y + 0.25f), 0.5f, 0.5f), LevelToLoadId.ToString(), Style);

        //var pos = Camera.main.WorldToScreenPoint(this.transform.position);
        //GUI.TextArea(new Rect(pos.x - 75,Screen.height - pos.y - 75, 150, 150), " ");
    }
}