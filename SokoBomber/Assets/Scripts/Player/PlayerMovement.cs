using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float MoveSpeedModifier = 2f;
	public AudioClip pushClip = null;

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
    private Vector3 slideEndPos = new Vector3();
    private bool sliding = false;
	void Update () {
        var o = GameObject.FindGameObjectWithTag("Finish");
        if (o != null)
        {
            return;
        }

		Vector3 InWorldPos = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z)).GetPoint(10);

		if (Input.GetMouseButton(0) && !moving && !sliding) //On click/tap
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
					moving = IsCollisionFreeAlt(movableTargetPos);
					if (moving)
					{
						AudioSource.PlayClipAtPoint(pushClip, this.transform.position,0.5f);
					}
				}
			}
			movingCollision = false;
		}

        if (moving)
        {
            var directVector = movementTargetPos - this.transform.position;
            if (directVector.magnitude > 0.025f)
            {
                var magn = directVector.magnitude;
                directVector.Normalize();

                var mainDelta = MoveSpeedModifier * Time.deltaTime;

                var moving_vector = mainDelta * directVector;

                if (moving_vector.magnitude > magn)
                {
                    moving_vector = movementTargetPos - this.transform.position;
                }

                this.transform.Translate(moving_vector);
                Camera.main.transform.Translate(moving_vector);

                MoveMovableBy(moving_vector);
            }
            else
            {
                moving = false;

                Camera.main.transform.Translate(movementTargetPos - this.transform.position);

                MoveMovableBy(movementTargetPos - this.transform.position);

                this.transform.Translate(movementTargetPos - this.transform.position);

                var ice_o = FindIceAt(this.transform.position);
                if (ice_o != null)
                {
                    var movedDir = movableTargetPos - movementTargetPos; //we arent sliding, but check if there is a bomb that needs to slide:

                    if (movableObject != null)
                    {
                        var ice_obj = FindIceAt(this.transform.position + movedDir);

                        if (ice_obj != null)
                        {
                            movableObject.SendMessage("Slide", movedDir, SendMessageOptions.DontRequireReceiver);
                        }
                    }

                    var slideDir = movableTargetPos - movementTargetPos;

                    bool foundLast = false;
                    int len = 10;

                    int slide_mod = 0;

                    while (!foundLast && len > 0)
                    {
                        len--;
                        slide_mod++;

                        var checkingPos = this.transform.position + (slide_mod * slideDir);

                        var coll = FindCollidableAt(checkingPos); //IsCollisionFree is boolean, nub Ernest
                        var mov = FindMovableAt(checkingPos);
                        var ice_coll = FindIceAt(checkingPos);

                        if (ice_coll == null)
                        {
                            foundLast = true;
                        }

                        if (coll != null)
                        {
                            foundLast = true;
                            slide_mod -= 1;
                        }
                        else if (mov != null)
                        {
                            foundLast = true;
                            slide_mod -= 1;
                        }

                    }

                    var tmp_vect = this.transform.position + ((slide_mod) * slideDir);
                    slideEndPos.Set(tmp_vect.x, tmp_vect.y, tmp_vect.z);

                    sliding = true;
                }
                else
                {
                    var movedDir = movableTargetPos - movementTargetPos; //we arent sliding, but check if there is a bomb that needs to slide:

                    if (movableObject != null)
                    {
                        var ice_obj = FindIceAt(this.transform.position + movedDir);

                        if (ice_obj != null)
                        {
                            movableObject.SendMessage("Slide", movedDir, SendMessageOptions.DontRequireReceiver);
                        }
                    }

                    var obj = GameObject.FindGameObjectWithTag("Overlord");

                    obj.SendMessage("NextTurn");
                }
            }
        }
        
        if (sliding) //slide lerp
        {
            Vector3 new_pos = Vector3.Lerp(this.transform.position, slideEndPos, 0.2f);
            Vector3 trans_pos = this.transform.position - new_pos;
            this.transform.Translate(-trans_pos);
            Camera.main.transform.Translate(-trans_pos);

            if ((this.transform.position - slideEndPos).magnitude < 0.01f)
            {
                Camera.main.transform.Translate(slideEndPos - this.transform.position);
                this.transform.Translate(slideEndPos - this.transform.position); //end up at the EXACT co-ord
                sliding = false;

                var obj = GameObject.FindGameObjectWithTag("Overlord");

                obj.SendMessage("NextTurn");
            }

        }
	}

    bool IsCollisionFree(Vector3 pos)
    {
        var collidables = GameObject.FindGameObjectsWithTag("Collidable");

        for (int i = 0; i < collidables.Length; i++)
        {
            var dist = (collidables[i].transform.position - pos).magnitude;
            if (dist < 0.1f)
            {
                return false;
            }
        }

        collidables = GameObject.FindGameObjectsWithTag("Destructible");

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

    bool IsCollisionFreeAlt(Vector3 pos)
    {
        var collidables = GameObject.FindGameObjectsWithTag("Collidable");

        for (int i = 0; i < collidables.Length; i++)
        {
            var dist = (collidables[i].transform.position - pos).magnitude;
            if (dist < 0.1f)
            {
                return false;
            }
        }

        collidables = GameObject.FindGameObjectsWithTag("Destructible");

        for (int i = 0; i < collidables.Length; i++)
        {
            var dist = (collidables[i].transform.position - pos).magnitude;
            if (dist < 0.1f)
            {
                return false;
            }
        }

        collidables = GameObject.FindGameObjectsWithTag("Movable");

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

        if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 100, 100), "Quit", ProgressController.Instance.ButtonStyle))
        {
            Application.LoadLevel("ChooseLevelScene");
        }
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 100, 100, 100), "Restart", ProgressController.Instance.ButtonStyle))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
	}

	public void Die()
	{
        ProgressController.Instance.FailLevel(0);

		Destroy (this.gameObject);
	}

    private GameObject FindIceAt(Vector3 pos)
    {
        var objs = GameObject.FindGameObjectsWithTag("Ice");

        for (int i = 0; i < objs.Length; i++)
        {
            if ((objs[i].transform.position - pos).magnitude < 0.01f)
            {
                return objs[i];
            }
        }

        return null;
    }

    private GameObject FindMovableAt(Vector3 pos)
    {
        var objs = GameObject.FindGameObjectsWithTag("Movable");

        for (int i = 0; i < objs.Length; i++)
        {
            if ((objs[i].transform.position - pos).magnitude < 0.01f)
            {
                return objs[i];
            }
        }

        return null;
    }

    private GameObject FindCollidableAt(Vector3 pos)
    {
        var objs = GameObject.FindGameObjectsWithTag("Collidable");

        for (int i = 0; i < objs.Length; i++)
        {
            if ((objs[i].transform.position - pos).magnitude < 0.01f)
            {
                return objs[i];
            }
        }

        return null;
    }
}
