using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSetting : MonoBehaviour
{
    public AudioSource Bgm_AudioSource;
    public AudioSource SE_AudioSource;
    public Slider Bgm_Slider = null;
    public Slider SE_Slider = null;
    bool isBgm = true;
    bool isSE = true;

    public void Bgm_SetMusicVolume(float volume)
    {
        //Debug.Log(volume);
        if (!isBgm) return;
        Bgm_AudioSource.volume = volume;
    }
    public void SE_SetMusicVolume(float volume)
    {
        if (!isSE) return;
        SE_AudioSource.volume = volume;
    }
    public void ON_BGM() 
    {
        Bgm_AudioSource.volume = Bgm_Slider.value;
        UIManager.INSTANCE.BGM_ON_Button.gameObject.SetActive(true);
        UIManager.INSTANCE.BGM_OFF_Button.gameObject.SetActive(false);
        isBgm = true;
    }
    
    public void OFF_BGM()
    {
        Bgm_AudioSource.volume = 0;
        UIManager.INSTANCE.BGM_ON_Button.gameObject.SetActive(false);
        UIManager.INSTANCE.BGM_OFF_Button.gameObject.SetActive(true);
        isBgm = false;
    }
    public void ON_SE()
    {
        SE_AudioSource.volume = SE_Slider.value;
        UIManager.INSTANCE.SE_ON_Button.gameObject.SetActive(true);
        UIManager.INSTANCE.SE_OFF_Button.gameObject.SetActive(false);
        isSE = true;
    }
    public void OFF_SE()
    {
        SE_AudioSource.volume = 0;
        UIManager.INSTANCE.SE_ON_Button.gameObject.SetActive(false);
        UIManager.INSTANCE.SE_OFF_Button.gameObject.SetActive(true);
        isSE = false;
    }
}
