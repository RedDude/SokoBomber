using UnityEngine;
using System.Collections;

public class BackToMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 50, 100, 30), "Back To Menu")) {
			Application.LoadLevel("MainMenu");
				}
	}
}
