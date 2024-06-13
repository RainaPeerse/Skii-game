using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // This is required for working with UI elements

public class MainMenu : MonoBehaviour
{

    // Assuming you have a button that calls this method when clicked
    public void PlayGame()
    {
        // Load the next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Assuming you have a button that calls this method when clicked
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    
}