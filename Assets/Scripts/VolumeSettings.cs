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
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

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

    private void Start()
    {
        // Sets music Slider value to stored playerprefs key, if not set to 100%
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    private void OnDisable()
    {
        // When disabled, updated the key to slider values
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    #endregion

}


