using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager instance;
    
    // Volume Settings and Playerprefs
    public AudioMixer mixer;
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    // Audiosources
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Audioclips
    public AudioClip[] music;
    public AudioClip[] sfx;

    #region Singleton

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Calls LoadVolume
        LoadVolume();
    }

    #endregion

    #region Playerprefs

    void LoadVolume()
    {
        // Volume saved in VolumeSettings
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        // Sets mixer volumes to the converted music volume stored in sliders
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    #endregion

    # region Playing and Stopping audio

    public IEnumerator PlayMusic(int clipNumber)
    {
        // Finds the length of the chosen clip and stores it as length
        float length = music[clipNumber].length;

        while(true)
        {
            print(length);
            // Plays one shot of chosen clip
            musicSource.PlayOneShot(music[clipNumber]);

            // Wait for length amount of time and makes the courotine happen again
            yield return new WaitForSeconds(length);
        }
    }

    public void PlaySFX(int clipNumber)
    {
        // Takes in a clipnumber from the array and plays one shot of that clip
        sfxSource.PlayOneShot(sfx[clipNumber]);
    }

    public void StopMusic()
    {
        // Stops all music
        musicSource.Stop();
    }

    public void StopSFX()
    {
        // Stops all sound effects
        sfxSource.Stop();
    }

    #endregion

}
