using UnityEngine;
using System.Collections;

public class WallFragmentsSorting : MonoBehaviour {

	// Use this for initialization
	void Start () {
        particleSystem.renderer.sortingLayerName = "Foreground";
	}
	
}
