using UnityEngine;
using System.Collections;

public class ProgressController : MonoBehaviour {

	public int CompletionProgress = 0;
    public int LoadedLevel = -1;
    public int TotalLevelsCount = 6;

	private static ProgressController _instance;
	public static ProgressController Instance
	{
		get {
			return _instance;
				}
	}

	//called before Start! Important for order!
	void Awake() {
		if (_instance != null && _instance != this) {
						Destroy (gameObject);
						return;
				} else {
						_instance = this;
				}
		DontDestroyOnLoad (gameObject);

		//Load level progress!
        if (PlayerPrefs.HasKey("Progress_Beta"))
        {
            CompletionProgress = PlayerPrefs.GetInt("Progress_Beta");
        }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CompleteLevel(int num)
    {
        if (num == CompletionProgress + 1 && num < TotalLevelsCount) //max - 1
        {
            CompletionProgress += 1;
            PlayerPrefs.SetInt("Progress_Beta", CompletionProgress);
        }
    }
}
