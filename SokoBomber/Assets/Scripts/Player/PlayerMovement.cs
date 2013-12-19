using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		moving = false;
		movementTargetPos = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	private bool moving;
	private Vector3 movementTargetPos;
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
				}
				else if (mouseDir.x >= 0)
				{
					//move right
					movementTargetPos = new Vector3(this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z);
				}
			}
			else if (Mathf.Abs(mouseDir.x) <= Mathf.Abs(mouseDir.y))
			{
				//Larger z movement
				if (mouseDir.y < 0)
				{
					//move up
					movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
				}
				else if (mouseDir.y >= 0)
				{
					//move down
					movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
				}
			}

			moving = true;
		}

		if (!moving)
		{
			if (Input.GetKey(KeyCode.S))
			{
				movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
				moving = true;
			}
			else if (Input.GetKey(KeyCode.W))
			{
				movementTargetPos = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
				moving = true;
			}
			else if (Input.GetKey(KeyCode.A))
			{
				movementTargetPos = new Vector3(this.transform.position.x - 1f, this.transform.position.y, this.transform.position.z);
				moving = true;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				movementTargetPos = new Vector3(this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z);
				moving = true;
			}
		}

		if (moving)
		{
			var directVector = movementTargetPos - this.transform.position;
			if (directVector.magnitude > 0.025f)
			{
				directVector.Normalize();

				this.transform.Translate(Time.deltaTime * directVector);
				Camera.main.transform.Translate(Time.deltaTime * directVector);

				MoveParalaxBy(Time.deltaTime * directVector);
			}
			else
			{
				moving = false;
				this.transform.Translate(movementTargetPos - this.transform.position);
				Camera.main.transform.Translate(movementTargetPos - this.transform.position);

				MoveParalaxBy(movementTargetPos - this.transform.position);
			}
		}
	}

	void MoveParalaxBy(Vector3 ammt)
	{
		var obj1 = GameObject.FindGameObjectWithTag ("ParalaxBG");

		obj1.transform.Translate (ammt * 0.975f);

		var obj2 = GameObject.FindGameObjectWithTag ("Paralax1");

		obj2.transform.Translate (ammt * 0.95f);

		var obj3 = GameObject.FindGameObjectWithTag ("Paralax2");

		obj3.transform.Translate (ammt * 0.9f);
	}
}
