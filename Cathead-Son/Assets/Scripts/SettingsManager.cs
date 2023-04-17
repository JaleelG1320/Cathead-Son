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


    [Header("FPS Counter Variables")]
    public Toggle fpsToggle;
    public TMP_Text fpsText;
    public GameObject fpsCounterObject;
    [HideInInspector] public int fpsInt;


    [Header("Black White Filter")]
    public Toggle bwToggle;
    [HideInInspector] public int bwInt;

    private void Awake()
    {
        //Check if there is a key for the playerprefs for the fps counter and set the int depending on it
        if(GameManager.instance.currentLevel.sceneName != "MainMenuLevel")
        {
            if (PlayerPrefs.HasKey("FpsToggleState"))
                fpsInt = PlayerPrefs.GetInt("FpsToggleState");
            else
                fpsInt = 1;
            if (fpsInt == 1)
            {
                fpsToggle.isOn = true;
                fpsCounterObject.SetActive(true);
            }
            else
            {
                fpsToggle.isOn = false;
                fpsCounterObject.SetActive(false);
            }
        }
        

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

        //Check if there is a key for the playerprefs for the vsync and set the int depending on it
        if (PlayerPrefs.HasKey("VFXToggleState"))
            bwInt = PlayerPrefs.GetInt("VFXToggleState");
        else
            bwInt = 1;

        if (bwInt == 1)
        {
            bwToggle.isOn = true;
        }
        else
        {
            bwToggle.isOn = false;
            
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

    public void AdjustFpsCounter(bool isFPSOn)
    {
        if (!isFPSOn)
        {
            PlayerPrefs.SetInt("FpsToggleState", 0);
            fpsCounterObject.SetActive(false);
            Debug.Log("FPS Counter is Off");
        }
        else
        {
            PlayerPrefs.SetInt("FpsToggleState", 1);
            fpsCounterObject.SetActive(true);
            Debug.Log("FPS Counter is On");
        }
    }
    public void AdjustVFX(bool isVFXOn)
    {
        if (!isVFXOn)
        {
            PlayerPrefs.SetInt("VFXToggleState", 0);
            Debug.Log("VFX Are Off");
        }
        else
        {
            PlayerPrefs.SetInt("VFXToggleState", 1);
            Debug.Log("VFX Are On");
        }
    }
    public void AdjustVysnc(bool isVsyncOn)
    {
        if (isVsyncOn == false)
        {
            PlayerPrefs.SetInt("VsyncToggleState", 0);
            QualitySettings.vSyncCount = 0;
            Debug.Log("The Vsync is Off");
        }
        else
        {
            PlayerPrefs.SetInt("VsyncToggleState", 1);
            QualitySettings.vSyncCount = 1;
            Debug.Log("The Vsync is On");
        }
    }

    void Update()
    {
        //Calculating the FPS
        float fps = 1 / Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + fps.ToString("F0");
    }

}
