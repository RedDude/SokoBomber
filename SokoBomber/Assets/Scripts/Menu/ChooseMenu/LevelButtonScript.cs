using UnityEngine;
using System.Collections;

public class LevelButtonScript : MonoBehaviour {
	public string LevelToLoad = "";
    public int LevelToLoadId = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (Input.GetMouseButtonDown (0)) {
						Vector3 inWorld = Camera.main.ScreenPointToRay (Input.mousePosition).GetPoint (10);

						if ((inWorld.x > this.transform.position.x - 0.5) && (inWorld.x < this.transform.position.x + 0.5) &&
								(inWorld.y > this.transform.position.y - 0.5) && (inWorld.y < this.transform.position.y + 0.5)) {
                                ProgressController.Instance.LoadedLevel = LevelToLoadId;
								Application.LoadLevel (LevelToLoad);
						}
				}

		}
}
