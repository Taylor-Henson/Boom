using System.Threading;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI menuTimerText;
    public float time;
    public float maxTime;
    public bool playing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Start music
        StartCoroutine(AudioManager.instance.PlayMusic(3));

        // Starts the timer
        time = maxTime;
        timerText.text = time + "/" + maxTime;
        menuTimerText.text = "Time = " + time + "/" + maxTime;
        playing = true;
    }

    // Update is called once per frame
    void Update()
    { 
        if (time > 0 && playing)
        {   
            // Counts down and displays
            time -= Time.deltaTime;
            float roundedTime = Mathf.Round(time * 100f) / 100f;
            timerText.text = roundedTime + "/" + maxTime;
            menuTimerText.text = "Time = " + roundedTime + "/" + maxTime;
        }
        else
        {
            // Game Over
            playing = false;
        }
    }
}
