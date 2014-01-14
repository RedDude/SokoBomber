using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public GameObject BronzeStarPrefab = null;
    public GameObject SilverStarPrefab = null;
    public GameObject GoldStarPrefab = null;

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

        PopulateStarRequirements();
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

        int ans = NewStarLevel(LoadedLevel);
        SpawnStar(ans);
    }

    public void FailLevel(int num)
    {
        var o = Instantiate(LevelEndObject) as GameObject;
        o.SendMessage("Failure", num, SendMessageOptions.DontRequireReceiver); //default success!

        int ans = ReadStarLevel(LoadedLevel);
        SpawnStar(ans);
    }

    void SpawnStar(int type)
    {
        Debug.Log("Spawning Star: " + type.ToString());
        switch (type)
        {
            case 2: Instantiate(GoldStarPrefab, Camera.main.transform.position - new Vector3(3, 0, -10), Quaternion.identity); break;
            case 1: Instantiate(SilverStarPrefab, Camera.main.transform.position - new Vector3(3, 0, -10), Quaternion.identity); break;
            default: Instantiate(BronzeStarPrefab, Camera.main.transform.position - new Vector3(3, 0, -10), Quaternion.identity); break;
        }
    }

    public void SpawnStarAt(int type, Vector3 location)
    {
        switch (type)
        {
            case 2: Instantiate(GoldStarPrefab, location, Quaternion.identity); break;
            case 1: Instantiate(SilverStarPrefab, location, Quaternion.identity); break;
            default: Instantiate(BronzeStarPrefab, location, Quaternion.identity); break;
        }
    }

    List<Vector2> StarRequirements = new List<Vector2>();
    void PopulateStarRequirements()
    {
        StarRequirements.Add(new Vector2(6,8)); //1
        StarRequirements.Add(new Vector2(2,2)); //2
        StarRequirements.Add(new Vector2(10,13)); //3
        StarRequirements.Add(new Vector2(47,55)); //4
        StarRequirements.Add(new Vector2(37,43)); //5
        StarRequirements.Add(new Vector2(48,56)); //6
        StarRequirements.Add(new Vector2(33,39)); //7
        StarRequirements.Add(new Vector2(66,74)); //8
        StarRequirements.Add(new Vector2(2,2)); //9
        StarRequirements.Add(new Vector2(47,55)); //10

        StarRequirements.Add(new Vector2(33,39)); //11
        StarRequirements.Add(new Vector2(75,88)); //12
        StarRequirements.Add(new Vector2(37,43)); //13
        StarRequirements.Add(new Vector2(61,71)); //14
        StarRequirements.Add(new Vector2(15,15)); //15
        StarRequirements.Add(new Vector2(98,112)); //16
        //StarRequirements.Add(new Vector2(,)); //17
        //StarRequirements.Add(new Vector2(,)); //18
        //StarRequirements.Add(new Vector2(,)); //19
        //StarRequirements.Add(new Vector2(,)); //20

    }

    public int NewStarLevel(int level)
    {
        int idx = level - 1;
        if (idx > -1 && idx < StarRequirements.Count)
        {
            Vector2 reqs = StarRequirements[idx];

            var overlord = GameObject.FindGameObjectWithTag("Overlord");
            var cmpt = overlord.GetComponent<Overlord>();

            int current_turns = cmpt.GetTurnCount();

            int previous = 999999;
            if (PlayerPrefs.HasKey("StarLevel" + level.ToString()))
            {
                previous = PlayerPrefs.GetInt("StarLevel" + level.ToString());
            }

            if (current_turns < previous)
            {
                PlayerPrefs.SetInt("StarLevel" + level.ToString(), current_turns);
                previous = current_turns;
            }

            Debug.Log("Finding star level: " + previous.ToString() + ", " + reqs.ToString());
            if (previous < reqs.x)
            {
                Debug.Log("Gold Star!");
                return 2; //gold
            }

            if (previous < reqs.y)
            {
                Debug.Log("Silver star!");
                return 1; //silver
            }
        }
        return 0; //bronze - default
    }

    public int ReadStarLevel(int level)
    {
        int idx = level - 1;
        if (idx > -1 && idx < StarRequirements.Count)
        {
            Vector2 reqs = StarRequirements[idx];

            int previous = 999999;
            if (PlayerPrefs.HasKey("StarLevel" + level.ToString()))
            {
                previous = PlayerPrefs.GetInt("StarLevel" + level.ToString());
            }

            if (previous < reqs.x)
            {
                return 2; //gold
            }

            if (previous < reqs.y)
            {
                return 1; //silver
            }
        }

        return 0; //bronze is default
    }
}
