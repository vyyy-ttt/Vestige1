using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsController : MonoBehaviour
{
    public Slider sensitivitySlider;

    void Start()
    {
        // initialize the slider value 
        sensitivitySlider.value = MouseSetting.mouseSensitivitySet;
        
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void OnSensitivityChanged(float value)
    {
        // update the static class 
        MouseSetting.mouseSensitivitySet = value;
    }
}
