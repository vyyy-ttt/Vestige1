using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager time;

    private float timeElapsed;
    private bool isTracking;

    void Awake()
    {
        if (time == null)
        {
            time = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartTracking();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void StartTracking()
    {
        isTracking = true;
    }

    public void StopTracking()
    {
        isTracking = false;
    }

    public float GetTime()
    {
        return timeElapsed;
    }
}
