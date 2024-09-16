using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene
        // Assume "Game" is the name of your main game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
