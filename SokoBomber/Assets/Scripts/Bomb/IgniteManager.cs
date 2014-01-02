using UnityEngine;
using System.Collections;

public class IgniteManager : MonoBehaviour {
	public bool IsIgnited = false;
	public int Ticks = 2;
	public GameObject ExplosionObject = null;

	public AudioClip explodeClip = null;
	public AudioClip blipClip = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Ignite(int ticks)
	{
		if (!IsIgnited)
		{
			IsIgnited = true;
			Ticks = ticks;
		}
	}

	public void TurnTick()
	{
		if (!IsIgnited) {
				var igniters = GameObject.FindGameObjectsWithTag ("Ignite");

					for (int i = 0; i < igniters.Length; i++) {
								if ((this.transform.position - igniters [i].transform.position).magnitude < 0.2f) {
										if (igniters [i].GetComponent (typeof(IgniterTimer)) != null) {
												var timer_ammt = (igniters [i].GetComponent<IgniterTimer>() as IgniterTimer).Ticks;
												if (timer_ammt != 0)
												{
													Ignite (timer_ammt);
												}
												else
												{
													Ignite (1);
													this.gameObject.SendMessage("TurnTick");
												}
												
												return;
										}
								}
						}
				}

		if (IsIgnited) {
						Debug.Log ("IgniteManager: TurnTick - Turns to explosion:" + (Ticks - 1).ToString ());
						Ticks -= 1;
						if (Ticks > 0) {
							AudioSource.PlayClipAtPoint (blipClip, this.transform.position, 0.5f);
						}
						else if (Ticks == 0) {
								//Explode!
								Debug.Log ("IgniteManager: TurnTick - Explode");

								//FSM for each direction:
								//UP! - increased Y
								Vector3 startPoint = this.transform.position;
								bool up_done = false;
								int m_d = 10;
								while (!up_done && m_d > 0) {
									Vector3 newPoint = startPoint + new Vector3 (0, 1, 0);

										var o_player = FindTaggedObjectAtPoint ("Player", newPoint);
										var o_coll = FindTaggedObjectAtPoint ("Collidable", newPoint);
										var o_destr = FindTaggedObjectAtPoint ("Destructible", newPoint);
										var o_movable = FindTaggedObjectAtPoint ("Movable", newPoint);

										if (o_player != null) {
												//player dies!
												o_player.SendMessage ("Die");
										} else if (o_coll != null) {
												//collidable means we stop!
												up_done = true;
										} else if (o_destr != null) {
												//destructibles must die
												Destroy (o_destr);
												up_done = true;
										} else if (o_movable) {
												//Blow up the thing is it is a bomb
												var cmpt = o_movable.GetComponent<IgniteManager> () as IgniteManager;
												cmpt.IsIgnited = true;
												if (cmpt.Ticks != 0) {
														cmpt.Ticks = 1;
														o_movable.SendMessage ("TurnTick");
												}

												up_done = true;
										}

										Instantiate (ExplosionObject, newPoint, Quaternion.identity);

										startPoint = newPoint;

										m_d -= 1;
								}

								startPoint = this.transform.position;
								bool down_done = false;
								m_d = 10;
								while (!down_done && m_d > 0) {
										Vector3 newPoint = startPoint + new Vector3 (0, -1, 0);
					
										var o_player = FindTaggedObjectAtPoint ("Player", newPoint);
										var o_coll = FindTaggedObjectAtPoint ("Collidable", newPoint);
										var o_destr = FindTaggedObjectAtPoint ("Destructible", newPoint);
										var o_movable = FindTaggedObjectAtPoint ("Movable", newPoint);
					
										if (o_player != null) {
												//player dies!
												o_player.SendMessage ("Die");
										} else if (o_coll != null) {
												//collidable means we stop!
												down_done = true;
										} else if (o_destr != null) {
												//destructibles must die
												Destroy (o_destr);
												down_done = true;
										} else if (o_movable) {
												//Blow up the thing is it is a bomb
												var cmpt = o_movable.GetComponent<IgniteManager> () as IgniteManager;
												cmpt.IsIgnited = true;
												if (cmpt.Ticks != 0) {
														cmpt.Ticks = 1;
														o_movable.SendMessage ("TurnTick");
												}
						
												down_done = true;
										}

										Instantiate (ExplosionObject, newPoint, Quaternion.identity);
					
										startPoint = newPoint;

										m_d -= 1;
								}

								startPoint = this.transform.position;
								bool left_done = false;
								m_d = 10;
								while (!left_done && m_d > 0) {
										Vector3 newPoint = startPoint + new Vector3 (-1, 0, 0);
					
										var o_player = FindTaggedObjectAtPoint ("Player", newPoint);
										var o_coll = FindTaggedObjectAtPoint ("Collidable", newPoint);
										var o_destr = FindTaggedObjectAtPoint ("Destructible", newPoint);
										var o_movable = FindTaggedObjectAtPoint ("Movable", newPoint);
					
										if (o_player != null) {
												//player dies!
												o_player.SendMessage ("Die");
										} else if (o_coll != null) {
												//collidable means we stop!
												left_done = true;
										} else if (o_destr != null) {
												//destructibles must die
												Destroy (o_destr);
												left_done = true;
										} else if (o_movable) {
												//Blow up the thing is it is a bomb
												var cmpt = o_movable.GetComponent<IgniteManager> () as IgniteManager;
												cmpt.IsIgnited = true;
												if (cmpt.Ticks != 0) {
														cmpt.Ticks = 1;
														o_movable.SendMessage ("TurnTick");
												}
						
												left_done = true;
										}
					
										startPoint = newPoint;

										Instantiate (ExplosionObject, newPoint, Quaternion.identity);

										m_d -= 1;
								}

								startPoint = this.transform.position;
								bool right_down = false;
								m_d = 10;
								while (!right_down && m_d > 0) {
										Vector3 newPoint = startPoint + new Vector3 (1, 0, 0);
					
										var o_player = FindTaggedObjectAtPoint ("Player", newPoint);
										var o_coll = FindTaggedObjectAtPoint ("Collidable", newPoint);
										var o_destr = FindTaggedObjectAtPoint ("Destructible", newPoint);
										var o_movable = FindTaggedObjectAtPoint ("Movable", newPoint);
					
										if (o_player != null) {
												//player dies!
												o_player.SendMessage ("Die");
										} else if (o_coll != null) {
												//collidable means we stop!
												right_down = true;
										} else if (o_destr != null) {
												//destructibles must die
												Destroy (o_destr);
												right_down = true;
										} else if (o_movable) {
												//Blow up the thing is it is a bomb
												var cmpt = o_movable.GetComponent<IgniteManager> () as IgniteManager;
												cmpt.IsIgnited = true;
												if (cmpt.Ticks != 0) {
														cmpt.Ticks = 1;
														o_movable.SendMessage ("TurnTick");
												}
						
												right_down = true;
										}

										Instantiate (ExplosionObject, newPoint, Quaternion.identity);
					
										startPoint = newPoint;

										m_d -= 1;
								}


								Instantiate (ExplosionObject, this.transform.position, Quaternion.identity);

								AudioSource.PlayClipAtPoint (explodeClip, this.transform.position, 0.5f);

								Destroy (gameObject);
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
