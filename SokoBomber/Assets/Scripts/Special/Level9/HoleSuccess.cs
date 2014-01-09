using UnityEngine;
using System.Collections;

public class HoleSuccess : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AfterFall()
    {
        ProgressController.Instance.CompleteLevel(ProgressController.Instance.LoadedLevel);
    }
}
