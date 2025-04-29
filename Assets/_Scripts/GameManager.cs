using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
}
