using UnityEngine;
using System.Collections;

public class FlavourTile : MonoBehaviour
{

    public Sprite[] flavourTiles;

	// Use this for initialization
	void Start ()
	{

	    var index = Random.Range(0, flavourTiles.Length);
	    GetComponent<SpriteRenderer>().sprite = flavourTiles[index];

	}
	

}
