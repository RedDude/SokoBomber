using UnityEngine;
using System.Collections;

public class ShowMouseCoords : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        Ray sptoRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 inWorld = sptoRay.GetPoint(-sptoRay.origin.z);

        GUI.TextArea(new Rect(100, 100, 100, 20), inWorld.ToString());

        GUI.TextArea(new Rect(100, 125, 100, 20), Input.mousePosition.ToString());
    }
}
