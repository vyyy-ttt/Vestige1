using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingsController : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI timeElapsedText;


    void Start()
    {
        // initialize the slider value 
        sensitivitySlider.value = MouseSetting.mouseSensitivitySet;
        
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void Update()
    {
        float timeElapsed = TimerManager.time.GetTime();
        timeElapsedText.text = "Total Time: " + FormatTime(timeElapsed);
    }
    
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void OnSensitivityChanged(float value)
    {
        // update the static class 
        MouseSetting.mouseSensitivitySet = value;
    }
}
