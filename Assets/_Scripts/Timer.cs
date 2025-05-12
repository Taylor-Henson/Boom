using System.Threading;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public float time;
    public float maxTime;
    public bool playing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Starts the timer
        time = maxTime;
        timerText.text = time + "/" + maxTime;
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
        }
        else
        {
            // Game Over
            playing = false;
        }
    }
}
