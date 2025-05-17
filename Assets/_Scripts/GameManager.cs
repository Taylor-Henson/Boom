using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool deadOrGameOver;
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
        
    }
}
