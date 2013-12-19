﻿using UnityEngine;
using System.Collections;

public class Overlord : MonoBehaviour {
	private int TurnsTotal = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextTurn()
	{
		Debug.Log ("Overlord: NextTurn");
		TurnsTotal += 1;

		var movables = GameObject.FindGameObjectsWithTag ("Movable");

		for (int i = 0; i < movables.Length; i++)
		{
			Debug.Log ("Overlord: TurnTick for " + movables[i].ToString());
			movables[i].SendMessage("TurnTick", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnGUI()
	{
		GUI.TextArea (new Rect (0, 25, 120, 20), "Turns: " + TurnsTotal.ToString());
	}
}
