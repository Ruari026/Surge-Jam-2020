using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    public void SetLevel (float SliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10 (SliderValue) *20 );
    }


    private void OnEnable()
    {
        mixer.GetFloat("MusicVol", out float startValue);
        //gameMixer.GetFloat("Music", out startMusicVolume);
        //gameMixer.GetFloat("SFX", out startSfxVolume);

        slider.value = ConvertToSliderValue(startValue);
        //musicSlider.value = ConvertToSliderValue(startMusicVolume);
        //sfxSlider.value = ConvertToSliderValue(startSfxVolume);
    }


    /*
    ====================================================================================================
    Controlling Game Volume
    ====================================================================================================
    */
    public void SetMasterVolume()
    {
        mixer.SetFloat("MusicVol", ConvertToDB(slider.value));
    }
    /*public void SetMusicVolume()
    {
        gameMixer.SetFloat("Music", ConvertToDB(musicSlider.value));
    }
    public void SetSfxVolume()
    {
        gameMixer.SetFloat("SFX", ConvertToDB(sfxSlider.value));
    }*/


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
