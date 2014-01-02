using UnityEngine;
using System.Collections;

public class MainMenuScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 100, 100), "Test")) {
			Application.LoadLevel ("TestScene");
		}

		if (GUI.Button (new Rect (120, 10, 100, 100), "StarTest")) {
			Application.LoadLevel ("StarfieldTestScene");
		}

		if (GUI.Button (new Rect (230, 10, 100, 100), "Sandbox")) {
			Application.LoadLevel("SandboxScene");
		}

		if (GUI.Button (new Rect (10, 120, 100, 100), "Lv1")) {
			Application.LoadLevel("Level1Scene");
		}
	}
}
