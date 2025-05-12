using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public float fps;
    public TextMeshProUGUI fpsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Repeats the fps method from the beginning of the game every 0.5 seconds
        InvokeRepeating("FPS", 0, 0.5f);
    }

    void FPS()
    {
        // Converts the fps float to an integer, then calculates it
        fps = (int) (1/Time.unscaledDeltaTime);

        // Sets the text to the fps by converting it to a string
        fpsText.text = fps.ToString();
    }
}
