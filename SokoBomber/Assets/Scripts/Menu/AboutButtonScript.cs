using UnityEngine;
using System.Collections;

public class AboutButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 inWorld = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10);

			if ((inWorld.x > this.transform.position.x - 1) && (inWorld.x < this.transform.position.x + 1) &&
			    (inWorld.y > this.transform.position.y - 0.25) && (inWorld.y < this.transform.position.y + 0.25))
			{
				Application.LoadLevel("MainMenuScene"); //TODO: Make an about screen with Credits
			}
		}
	}
}
