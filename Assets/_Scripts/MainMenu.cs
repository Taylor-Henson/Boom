using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider sliderX;
    public Slider sliderY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Checks for Y key, if so gets the pref, else sets the pref to default
        if (PlayerPrefs.HasKey("SensXPrefs"))
        {
            PlayerPrefs.GetFloat("SensXPrefs");
        }
        else
        {
            PlayerPrefs.SetFloat("SensXPrefs", 0.5f);
        }

        // Checks for  X key, if so gets the pref, else sets the pref to default
        if (PlayerPrefs.HasKey("SensYPrefs"))
        {
            PlayerPrefs.GetFloat("SensYPrefs");
        }
        else
        {
            PlayerPrefs.SetFloat("SensYPrefs", 0.5f);
        }
    }

    private void Start()
    {
        // Initially sets sliders to pref value
        sliderX.value = PlayerPrefs.GetFloat("SensXPrefs");
        sliderY.value = PlayerPrefs.GetFloat("SensYPrefs");
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
}
