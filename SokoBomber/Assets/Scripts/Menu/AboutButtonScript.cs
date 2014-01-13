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
            Ray sptoRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 inWorld = sptoRay.GetPoint(-sptoRay.origin.z);

			if ((inWorld.x > this.transform.position.x - 1) && (inWorld.x < this.transform.position.x + 1) &&
			    (inWorld.y > this.transform.position.y - 0.25) && (inWorld.y < this.transform.position.y + 0.25))
			{
				Application.LoadLevel("MainMenuScene"); //TODO: Make an about screen with Credits
			}
		}
	}
}
