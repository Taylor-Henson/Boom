using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Sliders")]
    // References
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Channels
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    #region Changing Volume

    private void Awake()
    {
        // On value changed, change sound volume
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetMusicVolume(float value)
    {
        // Set Music volume to converted value
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20f);
    }

    void SetSFXVolume(float value)
    {
        // Set SFX volume to converted value
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20f);
    }

    #endregion

    #region PlayerPrefs



    #endregion

}


