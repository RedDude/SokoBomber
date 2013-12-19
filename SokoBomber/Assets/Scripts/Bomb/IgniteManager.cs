using UnityEngine;
using System.Collections;

public class IgniteManager : MonoBehaviour {
	public bool IsIgnited = false;
	public int Ticks = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Ignite(int ticks)
	{
		Debug.Log ("IgniteManager: Ignite");
		if (!IsIgnited)
		{
			IsIgnited = true;
			Ticks = ticks;
		}
	}

	public void TurnTick()
	{
		Debug.Log ("IgniteManager: TurnTick");
		if (!IsIgnited) {
						var igniters = GameObject.FindGameObjectsWithTag ("Ignite");

						for (int i = 0; i < igniters.Length; i++) {
								if ((this.transform.position - igniters [i].transform.position).magnitude < 0.2f) {
										if (igniters [i].GetComponent (typeof(IgniterTimer)) != null) {
												var timer_ammt = (igniters [i].GetComponent (typeof(IgniterTimer)) as IgniterTimer).Ticks;
												Ignite (timer_ammt);
												return;
										}
								}
						}
				}

		if (IsIgnited)
		{
			Debug.Log ("IgniteManager: TurnTick - Turns to explosion:" + (Ticks - 1).ToString());
			Ticks -= 1;
			if (Ticks == 0)
			{
				//Explode!
				Debug.Log ("IgniteManager: TurnTick - Explode");
				Destroy(this);
			}
		}
	}
}
