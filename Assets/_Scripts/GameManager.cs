using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool dead;
    public bool gameOver;
    public float highScore;

    #region Singleton

    void Awake()
    {
        // Singleton method
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    private void Start()
    {
        if (PlayerPrefs.HasKey("HIGHSCORE"))
        {
            highScore = PlayerPrefs.GetFloat("HIGHSCORE");
        }
        else
        {
            PlayerPrefs.SetFloat("HIGHSCORE", 90);
            highScore = PlayerPrefs.GetFloat("HIGHSCORE");
        }
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetFloat("HIGHSCORE", highScore);
    }
}
