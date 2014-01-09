using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void TurnTick()
    {
        var obj = GameObject.FindGameObjectWithTag("Player");

        if ((obj.transform.position - this.transform.position).magnitude < 0.01f)
        {
            //player found me!
            ProgressController.Instance.CompleteLevel(ProgressController.Instance.LoadedLevel);

            //Application.LoadLevel("ChooseLevelScene");
        }
    }
}
