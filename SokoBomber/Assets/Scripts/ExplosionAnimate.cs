using UnityEngine;
using System.Collections;

public class ExplosionAnimate : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	float timeDone = 0f;
	void Update () {
		timeDone += Time.deltaTime;
		if (timeDone >= 0.9f)
		{
			Destroy (this.gameObject);
		}
	}
	
}
