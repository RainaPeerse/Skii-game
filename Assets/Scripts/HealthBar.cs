using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider component
    public PlayerController playerController; // Reference to the PlayerController script

    // Start is called before the first frame update
    void Start()
    {
        // Set the maximum value of the slider to the player's maximum health
        slider.maxValue = playerController.playerHealth;
        // Set the current value of the slider to the player's current health
        slider.value = playerController.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the slider's value to the player's current health
        slider.value = playerController.playerHealth;
    }
}