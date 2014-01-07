using UnityEngine;
using System.Collections;

public class GenerateLevelButtonsScript : MonoBehaviour {
	public GameObject buttonTemplate = null;

	// Use this for initialization
	void Start () {
		int currentProgress = ProgressController.Instance.CompletionProgress;

		int state1 = 0;
		int state2 = -1;
		for (int i = 0; i < currentProgress + 1; i++)
		{
			state2 += 1;
			var o = Instantiate(buttonTemplate, new Vector3(1.5f * (state2 - 1), state1 * -1.5f, 0), Quaternion.identity) as GameObject;
			var cmpt = o.GetComponent<LevelButtonScript>();
			
            cmpt.LevelToLoadId = i + 1;

            cmpt.LevelToLoad = "Level" + cmpt.LevelToLoadId.ToString() + "Scene";

			if (state2 == 1)
			{
				state2 = -1;
				state1 += 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
