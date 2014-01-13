using UnityEngine;
using System.Collections;

public class ScreenShakeManager : MonoBehaviour {

    private static ScreenShakeManager _instance;
    public static ScreenShakeManager Instance
    {
        get
        {
            return _instance;
        }
    }

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
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CameraShake();
	}

    public static float shakeInt = 0.0f;
    float decrease = 2.0f;
    float magnitude = 0.65f;

    Vector3 shakeTotal = new Vector3();

    void CameraShake()
    {
        var cam = Camera.main.transform;
        if (shakeInt != 0)
        {
            shakeInt -= 1 * decrease * shakeInt * Time.deltaTime;
            if (shakeInt < 0.1f)
            {
                shakeInt = 0;
            }
        }
        if (shakeInt > 0)
        {
            cam.Rotate(Random.Range(-magnitude * shakeInt, magnitude * shakeInt), Random.Range(-magnitude * shakeInt, magnitude * shakeInt), Random.Range(-magnitude * shakeInt, magnitude * shakeInt));

            Vector3 translateBy = new Vector3(Random.Range(-magnitude * shakeInt , magnitude * shakeInt), Random.Range(-magnitude * shakeInt, magnitude * shakeInt), 0f);
            shakeTotal -= translateBy;

            cam.Translate(translateBy);
        }

        if (shakeInt == 0)
        {
            cam.rotation = Quaternion.Lerp(cam.rotation, Quaternion.identity, 0.1f);

            Vector3 v3Lerp = Vector3.Lerp(shakeTotal, Vector3.zero, 0.1f);
            Vector3 v3diff = shakeTotal - v3Lerp;

            shakeTotal -= v3diff;
            cam.Translate(v3diff);
        }

    }
}