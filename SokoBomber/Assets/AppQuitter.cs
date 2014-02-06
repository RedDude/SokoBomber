using UnityEngine;
using System.Collections;

public class AppQuitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "MainMenuScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else if (Application.loadedLevelName == "ChooseLevelScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("MainMenuScene");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("ChooseLevelScene");
            }
        }
	}
}
