using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider sliderX;
    public Slider sliderY;
    public Toggle fpsToggle;

    public int toggleInt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Y SENS KEY
        if (PlayerPrefs.HasKey("SensXPrefs"))
        {
            PlayerPrefs.GetFloat("SensXPrefs");
        }
        else
        {
            PlayerPrefs.SetFloat("SensXPrefs", 0.5f);
        }

        // X SENS KEY
        if (PlayerPrefs.HasKey("SensYPrefs"))
        {
            PlayerPrefs.GetFloat("SensYPrefs");
        }
        else
        {
            PlayerPrefs.SetFloat("SensYPrefs", 0.5f);
        }

        // FPS TOGGLE KEY
        if (PlayerPrefs.HasKey("FPSTogglePrefs"))
        {
            toggleInt = PlayerPrefs.GetInt("FPSTogglePrefs");
        }
        else
        {
            PlayerPrefs.SetInt("FPSTogglePrefs", 0);
            toggleInt = PlayerPrefs.GetInt("FPSToggleInt");
        }
    }

    private void Start()
    {
        // Plays music
        int random = Random.Range(0, 3);
        StartCoroutine(AudioManager.instance.PlayMusic(random));

        // Unlocks cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Initially sets sliders to pref value
        sliderX.value = PlayerPrefs.GetFloat("SensXPrefs");
        sliderY.value = PlayerPrefs.GetFloat("SensYPrefs");

        // Sets toggle value at start
        if (toggleInt == 1)
        {
            fpsToggle.isOn = true;
        }
        else
        {
            fpsToggle.isOn = false;
        }
    }

    public void OnSensXChanged(float value)
    {
        // Sets pref to value on change
        PlayerPrefs.SetFloat("SensXPrefs", value);
    }

    public void OnSensYChanged(float value)
    {
        // Sets pref to value on change
        PlayerPrefs.SetFloat("SensYPrefs", value);
    }

    public void OnToggleChanged(bool value)
    {
        if (value)
        {
            toggleInt = 1;
        }
        else
        {
            toggleInt = 0;
        }

        PlayerPrefs.SetInt("FPSTogglePrefs", toggleInt);
        AudioManager.instance.PlaySFX(10);
    }
}
