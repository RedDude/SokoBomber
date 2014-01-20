using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Net;

public class AnalyticsHelper : MonoBehaviour {

    private static AnalyticsHelper _instance = null;
    public static AnalyticsHelper Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else if (_instance == null)
        {
            _instance = this;

            SendEvent("game", "open", 0, "v0.5");
        }
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void logEvent(string evt, string subevt, int val)
    {
        SendEvent(evt, subevt, val, " ");
    }

    public void logEvent(string evt, string subevt, string val)
    {
        SendEvent(evt, subevt, 0, val);
    }

    private void SendEvent(string evt, string subevt, int val1, string val2)
    {
        string pform = Application.platform.ToString();
        string lang = Application.systemLanguage.ToString();

        Thread t = new Thread(new ThreadStart(() => {
            try
            {
                string parms = evt + "|" + subevt + "|" + val1.ToString() + "|" + val2 + "|" + pform + "|" + lang;

                string escaped = Uri.EscapeUriString("http://ernestloveland.co.za/analyze.php?q=" + parms);

                WebClient wc = new WebClient();
                wc.OpenReadCompleted += wc_OpenReadCompleted;
                wc.OpenReadAsync(new Uri(escaped));
            }
            catch { }

            //wc.OpenReadCompleted += wc_OpenReadCompleted;
            //Debug.Log("Analyitics: " + tdx);
        } ));

        t.Start();
    }

    private void SendEventBlocked(string evt, string subevt, int val1, string val2)
    {
        try
        {
            Debug.Log("Start final analytics log");

            string pform = Application.platform.ToString();
            string lang = Application.systemLanguage.ToString();

            string parms = evt + "|" + subevt + "|" + val1.ToString() + "|" + val2 + "|" + pform + "|" + lang;

            string escaped = Uri.EscapeUriString("http://ernestloveland.co.za/analyze.php?q=" + parms);

            WebClient wc = new WebClient();
            wc.OpenReadCompleted += wc_OpenReadCompleted;
            ORComplete = false;
            wc.OpenReadAsync(new Uri(escaped));

            int a = 0;
            while (!ORComplete)
            {
                a = 0;
            }

            Debug.Log("End final analytics log");
        }
        catch
        {

        }
    }

    bool ORComplete = false;

    void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
    {
        ORComplete = true;
        Debug.Log("Analytics success. " + e.ToString());
    }

    void OnDestroy()
    {
        SendEventBlocked("game", "close", (int)Time.time, " ");
        //new WaitForSeconds(4);
    }
}
