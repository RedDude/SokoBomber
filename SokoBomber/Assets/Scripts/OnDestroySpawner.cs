using UnityEngine;
using System.Collections;

public class OnDestroySpawner : MonoBehaviour {
    public GameObject ToSpawn = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCustomDestroy()
    {
        Instantiate(ToSpawn, this.transform.position, Quaternion.identity);
    }
}
