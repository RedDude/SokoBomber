using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float MoveSpeedModifier = 2f;

	// Use this for initialization
	void Start () {
		moving = false;
		movingCollision = false;
		movementTargetPos = new Vector3 (0, 0, 0);
		movableObject = null;
	}
	
	// Update is called once per frame
	private bool moving;
	private Vector3 movementTargetPos;
	private Vector3 movableTargetPos;
	private bool movingCollision;
	private GameObject movableObject;
	void Update () {
		Vector3 InWorldPos = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z)).GetPoint(10);

		if (Input.GetMouseButton(0) && !moving) //On click/tap
		{
			Vector3 mouseDir = InWorldPos - this.transform.position;
			//get the direction
			if (Mathf.Abs(mouseDir.x) > Mathf.Abs(mouseDir.y))
			{
				//larger X movement
				if (mouseDir.x < 0)
				{
					//move left
					movementTargetPos = new Vector3(this.transform.position.x - 1f, this.transform.position.y, this.transform.position.z);
					movableTargetPos = new Vector3(this.transform.position.x - 2f, this.transform.position.y, this.transform.position.z);
				}
				else if (mouseDir.x >= 0)
				{
					//move right
					movementTargetPos = new Vector3(this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z);
					movableTargetPos = new Vector3(this.transform.position.x + 2f, this.transform.position.y, this.transform.position.z);
				}
			}
			else if (Mathf.Abs(mouseDir.x) <= Mathf.Abs(mouseDir.y))
			{
				//Larger z movement
				if (mouseDir.y < 0)
				{
					//move up
					movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
					movableTargetPos = new Vector3(this.transform.position.x, this.transform.position.y - 2f, this.transform.position.z);
				}
				else if (mouseDir.y >= 0)
				{
					//move down
					movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
					movableTargetPos = new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z);
				}
			}

			moving = true;
			movingCollision = true;
		}

		if (!moving)
		{
			if (Input.GetKey(KeyCode.S))
			{
				movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
				movableTargetPos = new Vector3(this.transform.position.x, this.transform.position.y - 2f, this.transform.position.z);
				moving = true;
				movingCollision = true;
			}
			else if (Input.GetKey(KeyCode.W))
			{
				movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
				movableTargetPos = new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z);
				moving = true;
				movingCollision = true;
			}
			else if (Input.GetKey(KeyCode.A))
			{
				movementTargetPos = new Vector3(this.transform.position.x - 1f, this.transform.position.y, this.transform.position.z);
				movableTargetPos = new Vector3(this.transform.position.x - 2f, this.transform.position.y, this.transform.position.z);
				moving = true;
				movingCollision = true;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				movementTargetPos = new Vector3(this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z);
				movableTargetPos = new Vector3(this.transform.position.x + 2f, this.transform.position.y, this.transform.position.z);
				moving = true;
				movingCollision = true;
			}
		}

		if (movingCollision)
		{ //do a collision check!
			moving = IsCollisionFree(movementTargetPos);
			if (moving)
			{
				FindMovable(movementTargetPos);
				if (movableObject != null)
				{
					moving = IsCollisionFree(movableTargetPos);
				}
			}
			movingCollision = false;
		}

		if (moving)
		{
			var directVector = movementTargetPos - this.transform.position;
			if (directVector.magnitude > 0.025f)
			{
				directVector.Normalize();

				var mainDelta = MoveSpeedModifier * Time.deltaTime;

				this.transform.Translate(mainDelta * directVector);
				Camera.main.transform.Translate(mainDelta * directVector);

				MoveMovableBy(mainDelta * directVector);
			}
			else
			{
				moving = false;

				Camera.main.transform.Translate(movementTargetPos - this.transform.position);

				MoveMovableBy(movementTargetPos - this.transform.position);

				this.transform.Translate(movementTargetPos - this.transform.position);

				var obj = GameObject.FindGameObjectWithTag("Overlord");

				obj.SendMessage("NextTurn");
			}
		}
	}

	bool IsCollisionFree(Vector3 pos)
	{
		var collidables = GameObject.FindGameObjectsWithTag ("Collidable");

		for (int i = 0; i < collidables.Length; i++)
		{
			var dist = (collidables[i].transform.position - pos).magnitude;
			if (dist < 0.1f)
			{
				return false;
			}
		}

		return true;
	}

	void FindMovable(Vector3 pos)
	{
		movableObject = null;

		var movables = GameObject.FindGameObjectsWithTag ("Movable");

		for (int i = 0; i < movables.Length; i++)
		{
			var dist = (movables[i].transform.position - pos).magnitude;
			if (dist < 0.1f)
			{
				movableObject = movables[i];
			}
		}
	}

	void MoveMovableBy(Vector3 ammt)
	{
		if (movableObject != null)
		{
			movableObject.transform.Translate(ammt);
		}
	}

	void OnGUI()
	{
		GUI.TextArea (new Rect (0, 0, 120, 20), this.transform.position.ToString ());
	}

	public void Die()
	{
		Destroy (this.gameObject);
	}
}
