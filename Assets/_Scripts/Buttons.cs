using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [Header("Arrays")]
    public GameObject[] menus; 

    public void LoadScene(int sceneNumber)
    {
        // Loads scene based off of integer chosen on the button
        SceneManager.LoadScene(sceneNumber);
        AudioManager.instance.PlaySFX(10);
    }

    public void QuitGame()
    {
        // Quits build
        Application.Quit();
    }

    public void LoadMenu(int menuNumber)
    {
        // Sets every object in the array to disabled
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        // Enables a menu from an array based off of the integer chosen on the button
        menus[menuNumber].gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(10);
    }
}
