using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    
    private void OnEnable()
    {
        mixer.GetFloat("MasterVol", out float startMasterValue);
        mixer.GetFloat("MusicVol", out float startMusicVolume);
        mixer.GetFloat("SFXVol", out float startSfxVolume);

        masterSlider.value = ConvertToSliderValue(startMasterValue);
        musicSlider.value = ConvertToSliderValue(startMusicVolume);
        sfxSlider.value = ConvertToSliderValue(startSfxVolume);
    }


    /*
    ====================================================================================================
    Controlling Game Volume
    ====================================================================================================
    */
    public void SetMasterVolume()
    {
        mixer.SetFloat("MasterVol", ConvertToDB(masterSlider.value));
    }
    public void SetMusicVolume()
    {
        mixer.SetFloat("MusicVol", ConvertToDB(musicSlider.value));
    }
    public void SetSfxVolume()
    {
        mixer.SetFloat("SFXVol", ConvertToDB(sfxSlider.value));
    }


    /*
    ====================================================================================================
    Utility
    ====================================================================================================
    */
    private float ConvertToDB(float sliderValue)
    {
        float db = Mathf.Log(sliderValue);
        return (db * 20);
    }

    private float ConvertToSliderValue(float db)
    {
        float sliderValue = db / 20;
        return Mathf.Exp(sliderValue);
    }
}
