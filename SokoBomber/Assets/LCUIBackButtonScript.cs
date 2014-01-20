using UnityEngine;
using System.Collections;

public class LCUIBackButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Ray sptoRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 inWorld = sptoRay.GetPoint(-sptoRay.origin.z);

            if ((inWorld.x > this.transform.position.x - 0.75) && (inWorld.x < this.transform.position.x + 0.75) &&
                    (inWorld.y > this.transform.position.y - 0.75) && (inWorld.y < this.transform.position.y + 0.75))
            {
                Application.LoadLevel("MainMenuScene");
            }
        }
	}
}
