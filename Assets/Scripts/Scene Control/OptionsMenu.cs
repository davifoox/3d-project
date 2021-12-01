using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
   
   public AudioMixer musicAudio, fxAudio;

   public Slider musicSlider, fxSlider; 
   
   
   
    // Start is called before the first frame update
    void Start()
    {
        
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        fxSlider.value = PlayerPrefs.GetFloat("FxVol", 0.75f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVol(float musicValue)
    {

        musicAudio.SetFloat("MusicVol", Mathf.Log10(musicValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", musicValue);

    }

    public void SetFxVol(float fxValue)
    {

        fxAudio.SetFloat("FxVol", Mathf.Log10(fxValue) * 20);
        PlayerPrefs.SetFloat("FxVol", fxValue);

    }
}
