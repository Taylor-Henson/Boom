using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject hud;
    public TextMeshProUGUI runTimeText;
    public Timer timerScript;
    float runTime;

    private void Start()
    {
        hud.SetActive(true);
        runTime = 90;
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.T))
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
        GameManager.instance.deadOrGameOver = true;

        // Unlocks cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Gets rid of hud
        hud.SetActive(false);

        runTime = timerScript.time;
        runTimeText.text = "Time: " + runTime; 
        
        if (runTime > GameManager.instance.highScore)
        {
            print(runTime + " - " + GameManager.instance.highScore);
            GameManager.instance.highScore = runTime;
        }
    }
}
