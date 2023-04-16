using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Variables")]
    public AudioMixer theMixer;
    public Slider masterSlider;
    public TMP_Text masterValueText;

    [Header("Vsync Variables")]
    public Toggle vSyncToggle;
    [HideInInspector] public int vsyncInt;

    [Header("Black White Filter")]
    public Toggle bwToggle;
    [HideInInspector] public int bwInt;

    private void Awake()
    {
        //Check if there is a key for the playerprefs for the vsync and set the int depending on it
        if (PlayerPrefs.HasKey("VsyncToggleState"))
            vsyncInt = PlayerPrefs.GetInt("VsyncToggleState");
        else
            vsyncInt = 1;

        if (vsyncInt == 1)
        {
            vSyncToggle.isOn = true;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            vSyncToggle.isOn = false;
            QualitySettings.vSyncCount = 0;
        }
    }

    void Start()
    {
        //Check if the master volume has a key via playerprefs and adjusts it value according to value stored in key
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            theMixer.SetFloat("MasterVol", ConvertToLog(PlayerPrefs.GetFloat("MasterVolume")));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
    }

     public float ConvertToLog(float oldValue)
     {
        float newValue = ((oldValue - -80f) / (20f - -80f)) * (1f - 0.0001f) + 0.0001f;

        return Mathf.Log10(newValue) * 20;
     }

    //Function to adjust the master volume by readjusting the value text and slider and setting the float for playerprefs
    public void AdjustMasterVolume()
    {
        masterValueText.text = (masterSlider.value + 80).ToString() + "%";
        theMixer.SetFloat("MasterVol", ConvertToLog(masterSlider.value));
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

}
