using UnityEngine;
using System.Collections;

public class ProgressController : MonoBehaviour
{

    public int CompletionProgress = 0;
    public int LoadedLevel = -1;
    public int TotalLevelsCount = 6;

    public GameObject LevelEndObject = null;

    public GUIStyle ButtonStyle = new GUIStyle();
    public GUIStyle SuccessNoticeStyle = new GUIStyle();
    public GUIStyle FailNoticeStyle = new GUIStyle();
    public GUIStyle SuccessButtonStyle = new GUIStyle();
    public GUIStyle FailButtonStyle = new GUIStyle();

    private static ProgressController _instance;
    public static ProgressController Instance
    {
        get
        {
            return _instance;
        }
    }

    //called before Start! Important for order!
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        //Load level progress!
        if (PlayerPrefs.HasKey("Progress_Beta"))
        {
            CompletionProgress = PlayerPrefs.GetInt("Progress_Beta");
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CompleteLevel(int num)
    {
        if (num == CompletionProgress + 1 && num < TotalLevelsCount) //max - 1
        {
            CompletionProgress += 1;
            PlayerPrefs.SetInt("Progress_Beta", CompletionProgress);
        }

        var o = Instantiate(LevelEndObject) as GameObject;
        if (Application.loadedLevelName == "Level2Scene")
        {
            o.SendMessage("Success", 1, SendMessageOptions.DontRequireReceiver);
        }
        else if (Application.loadedLevelName == "Level9Scene")
        {
            o.SendMessage("Success", 2, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            o.SendMessage("Success", 0, SendMessageOptions.DontRequireReceiver); //default success!
        }
    }

    public void FailLevel(int num)
    {
        var o = Instantiate(LevelEndObject) as GameObject;
        o.SendMessage("Failure", num, SendMessageOptions.DontRequireReceiver); //default success!
    }
}
