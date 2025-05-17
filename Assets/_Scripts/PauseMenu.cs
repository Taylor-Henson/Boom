using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject hud;
    public GameObject pauseMenu;
    public bool menu;

    // Update is called once per frame
    void Update()
    {
        // Checks if menu is on
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.deadOrGameOver)
        {
            if (menu)
            {
                // Turns off menu
                menu = false;

                // Turns on and off respective canvases
                hud.SetActive(true);
                pauseMenu.SetActive(false);

                // Locks cursor and stops it being visible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // Turns time back on
                Time.timeScale = 1;
            }
            else
            {
                // Turns on menu
                menu = true;

                // Turns on and off respective canvases
                hud.SetActive(false);
                pauseMenu.SetActive(true);

                // Unlocks cursor and makes it visible
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Turns time off
                Time.timeScale = 0;
            }
        }
    }

    public void LoadScene(int sceneNumber)
    {
        // Turns off menu
        menu = false;

        // Turns on and off respective canvases
        hud.SetActive(true);
        pauseMenu.SetActive(false);

        // Locks cursor and stops it being visible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Turns time back on
        Time.timeScale = 1;

        // Loads scene based off of integer chosen on the button
        SceneManager.LoadScene(sceneNumber);
        AudioManager.instance.PlaySFX(10);
    }
}
