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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 inWorld = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10);

            if ((inWorld.x > this.transform.position.x - 0.5) && (inWorld.x < this.transform.position.x + 0.5) &&
                    (inWorld.y > this.transform.position.y - 0.5) && (inWorld.y < this.transform.position.y + 0.5))
            {
                ProgressController.Instance.LoadedLevel = LevelToLoadId;
                Application.LoadLevel(LevelToLoad);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector3(0, -3f, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector3(0, 3f, 0) * Time.deltaTime);
        }

        screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

    }

    Vector3 screenPos;

    void OnGUI()
    {
        GUI.TextArea(new Rect(screenPos.x - 0.25f, Screen.height - (screenPos.y + 0.25f), 0.5f, 0.5f), LevelToLoadId.ToString(), Style);
    }
}