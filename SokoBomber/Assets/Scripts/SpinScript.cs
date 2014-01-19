using UnityEngine;
using System.Collections;

public class SpinScript : MonoBehaviour
{

    public float speed = 2.0f;
    
	// Update is called once per frame
	void Update ()
	{

	    transform.Rotate(Vector3.forward*speed*Time.deltaTime);

	}
}
