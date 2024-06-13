using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishButton : MonoBehaviour
{
    private void Start()
    {
        // Attempt to find the GameData instance and set its UI elements
        GameData gameData = FindObjectOfType<GameData>();
        if (gameData != null)
        {
            // If GameData is found, attempt to find the UI elements by name
            Text completedRacesText = GameObject.Find("CompletedRacesText")?.GetComponent<Text>();
            Text raceTimesText = GameObject.Find("RaceTimesText")?.GetComponent<Text>();

            // If the UI elements are found, set them in the GameData instance
            if (completedRacesText != null && raceTimesText != null)
            {
                gameData.SetUIElements(completedRacesText, raceTimesText);
            }
            else
            {
                Debug.LogWarning("CompletedRacesText or RaceTimesText not found in the scene. UI elements will not be set.");
            }
        }
        else
        {
            Debug.LogWarning("GameData instance not found in the scene. UI elements will not be set.");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void RestartGame()
    {
        Debug.Log("Restarting the game...");
        // Unpause the game
        Time.timeScale = 1;
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}