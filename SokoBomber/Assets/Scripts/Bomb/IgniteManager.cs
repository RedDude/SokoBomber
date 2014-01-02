using UnityEngine;
using System.Collections;

public class IgniteManager : MonoBehaviour {
	public bool IsIgnited = false;
	public int Ticks = 2;
	public GameObject ExplosionObject = null;

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

				//FSM for each direction:
				//UP! - increased Y
				Vector3 startPoint = this.transform.position;
				bool up_done = false;
				while (!up_done)
				{
					Vector3 newPoint = startPoint + new Vector3(0,1,0);

					var o_player = FindTaggedObjectAtPoint("Player", newPoint);
					var o_coll = FindTaggedObjectAtPoint("Collidable", newPoint);
					var o_destr = FindTaggedObjectAtPoint("Destructible", newPoint);
					var o_movable = FindTaggedObjectAtPoint("Movable", newPoint);

					if (o_player != null)
					{
						//player dies!
						o_player.SendMessage("Die");
					}
					else if (o_coll != null)
					{
						//collidable means we stop!
						up_done = true;
					}
					else if (o_destr != null)
					{
						//destructibles must die
						Destroy (o_destr);
						up_done = true;
					}
					else if (o_movable)
					{
						//Blow up the thing is it is a bomb
						var cmpt = o_movable.GetComponentInChildren<IgniteManager>() as IgniteManager;
						cmpt.IsIgnited = true;
						if (cmpt.Ticks != 0)
						{
							cmpt.Ticks = 0;
							cmpt.TurnTick();
						}

						up_done = true;
					}

					Instantiate(ExplosionObject, newPoint, Quaternion.identity);

					startPoint = newPoint;
				}

				startPoint = this.transform.position;
				bool down_done = false;
				while (!down_done)
				{
					Vector3 newPoint = startPoint + new Vector3(0,-1,0);
					
					var o_player = FindTaggedObjectAtPoint("Player", newPoint);
					var o_coll = FindTaggedObjectAtPoint("Collidable", newPoint);
					var o_destr = FindTaggedObjectAtPoint("Destructible", newPoint);
					var o_movable = FindTaggedObjectAtPoint("Movable", newPoint);
					
					if (o_player != null)
					{
						//player dies!
						o_player.SendMessage("Die");
					}
					else if (o_coll != null)
					{
						//collidable means we stop!
						down_done = true;
					}
					else if (o_destr != null)
					{
						//destructibles must die
						Destroy (o_destr);
						down_done = true;
					}
					else if (o_movable)
					{
						//Blow up the thing is it is a bomb
						var cmpt = o_movable.GetComponentInChildren<IgniteManager>() as IgniteManager;
						cmpt.IsIgnited = true;
						if (cmpt.Ticks != 0)
						{
							cmpt.Ticks = 0;
							cmpt.TurnTick();
						}
						
						down_done = true;
					}

					Instantiate(ExplosionObject, newPoint, Quaternion.identity);
					
					startPoint = newPoint;
				}

				startPoint = this.transform.position;
				bool left_done = false;
				while (!left_done)
				{
					Vector3 newPoint = startPoint + new Vector3(-1,0,0);
					
					var o_player = FindTaggedObjectAtPoint("Player", newPoint);
					var o_coll = FindTaggedObjectAtPoint("Collidable", newPoint);
					var o_destr = FindTaggedObjectAtPoint("Destructible", newPoint);
					var o_movable = FindTaggedObjectAtPoint("Movable", newPoint);
					
					if (o_player != null)
					{
						//player dies!
						o_player.SendMessage("Die");
					}
					else if (o_coll != null)
					{
						//collidable means we stop!
						left_done = true;
					}
					else if (o_destr != null)
					{
						//destructibles must die
						Destroy (o_destr);
						left_done = true;
					}
					else if (o_movable)
					{
						//Blow up the thing is it is a bomb
						var cmpt = o_movable.GetComponentInChildren<IgniteManager>() as IgniteManager;
						cmpt.IsIgnited = true;
						if (cmpt.Ticks != 0)
						{
							cmpt.Ticks = 0;
							cmpt.TurnTick();
						}
						
						left_done = true;
					}
					
					startPoint = newPoint;

					Instantiate(ExplosionObject, newPoint, Quaternion.identity);
				}

				startPoint = this.transform.position;
				bool right_down = false;
				while (!right_down)
				{
					Vector3 newPoint = startPoint + new Vector3(1,0,0);
					
					var o_player = FindTaggedObjectAtPoint("Player", newPoint);
					var o_coll = FindTaggedObjectAtPoint("Collidable", newPoint);
					var o_destr = FindTaggedObjectAtPoint("Destructible", newPoint);
					var o_movable = FindTaggedObjectAtPoint("Movable", newPoint);
					
					if (o_player != null)
					{
						//player dies!
						o_player.SendMessage("Die");
					}
					else if (o_coll != null)
					{
						//collidable means we stop!
						right_down = true;
					}
					else if (o_destr != null)
					{
						//destructibles must die
						Destroy (o_destr);
						right_down = true;
					}
					else if (o_movable)
					{
						//Blow up the thing is it is a bomb
						var cmpt = o_movable.GetComponentInChildren<IgniteManager>() as IgniteManager;
						cmpt.IsIgnited = true;
						if (cmpt.Ticks != 0)
						{
							cmpt.Ticks = 0;
							cmpt.TurnTick();
						}
						
						right_down = true;
					}

					Instantiate(ExplosionObject, newPoint, Quaternion.identity);
					
					startPoint = newPoint;
				}


				Instantiate(ExplosionObject, this.transform.position, Quaternion.identity);

				Destroy(gameObject);
			}
		}
	}

	private GameObject FindTaggedObjectAtPoint(string tag, Vector3 point)
	{
		var objs = GameObject.FindGameObjectsWithTag (tag);
		for (int i = 0; i < objs.Length; i++)
		{
			if ((objs[i].transform.position - point).magnitude < 0.01f)
			{
				return objs[i];
			}
		}

		return null;
	}
}
