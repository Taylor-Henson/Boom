using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject deathScreen;
    public GameObject hud;
    public TextMeshProUGUI runTimeText;
    public Timer timerScript;
    float runTime;

    private void Start()
    {
        GameManager.instance.gameOver = false;
        hud.SetActive(true);
        runTime = 90;
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.F))
        {
            WinGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WinGame();
    }

    void WinGame()
    {
        // Sets win screen 
        winScreen.SetActive(true);
        GameManager.instance.gameOver = true;

        // Unlocks cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Gets rid of hud
        hud.SetActive(false);
        deathScreen.SetActive(false);

        runTime = 90 - timerScript.time;
        runTimeText.text = "Time: " + runTime; 
        
        if (runTime < GameManager.instance.highScore)
        {
            GameManager.instance.highScore = runTime;
            GameManager.instance.SavePlayerPrefs();
        }
    }
}
