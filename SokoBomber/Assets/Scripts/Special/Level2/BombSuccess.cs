using UnityEngine;
using System.Collections;

public class BombSuccess : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AfterExplode()
    {
        ProgressController.Instance.CompleteLevel(ProgressController.Instance.LoadedLevel);

        Application.LoadLevel("ChooseLevelScene");
    }
}
