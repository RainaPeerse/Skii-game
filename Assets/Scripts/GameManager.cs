using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas finishCanvas; // Assign this in the inspector with the finish canvas
    private bool isGameActive = true;

    private void Start()
    {
        // Ensure the finish canvas is inactive at the start
        finishCanvas.gameObject.SetActive(false);

        // Subscribe to the OnRaceStop event
        GameEvents.OnRaceStop += StopRace;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnRaceStop event to prevent memory leaks
        GameEvents.OnRaceStop -= StopRace;
    }

    public void StopRace()
    {
        if (isGameActive)
        {
            isGameActive = false;
            finishCanvas.gameObject.SetActive(true);
            Time.timeScale = 0; // Freeze the game
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}