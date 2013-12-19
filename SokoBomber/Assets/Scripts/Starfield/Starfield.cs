using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Starfield : MonoBehaviour {
	public List<GameObject> StarTypes = new List<GameObject>();

	public int NumberOfStarsPerLayer = 1;
	public int NumberOfLayers = 1;

	public GameObject MovementTarget;

	public Vector2 Velocity;

	public float LayerAccelerationIncrement;


	private List<GameObject> stars = new List<GameObject>();

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.ToFileTimeUtc();
		if(StarTypes.Count > 0 && NumberOfStarsPerLayer > 0 & NumberOfLayers > 0){
			for(int i = 0; i < NumberOfLayers; i++){
				for(int j = 0; j < NumberOfStarsPerLayer; j++){
					int starindex = Random.Range(0,StarTypes.Count - 1);
					GameObject g = (GameObject)GameObject.Instantiate(StarTypes[starindex],new Vector2(Random.Range(-Screen.width*100/2,Screen.width*100/2)/10000f,Random.Range (-Screen.height*100/2,Screen.height*100)/10000f),Quaternion.identity);
					g.transform.localScale = new Vector2(j,j);
					stars.Add(g);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Velocity != Vector2.zero){
			foreach(GameObject g in stars){
				g.transform.Translate(Velocity);

			}
		}
	}
}
