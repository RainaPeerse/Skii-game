using UnityEngine;
using UnityEngine.UI; // Add this to use the UI namespace
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f; // Speed of rotation around the Y-axis
    private float rotationLimit = 70f; // Maximum rotation in degrees
    private float currentRotation = 0f; // Current rotation around the Y-axis
    public float boostMultiplier = 2f; // Speed multiplier for the boost
    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public float playerHealth = 100f; // Player's health
    public Text playerHealthUI; // Reference to the UI Text element for health display

    private Rigidbody rb;
    private Animator animator; // Reference to the Animator component
    private float originalSpeed; // Store the original speed for resetting after boost
    private Coroutine speedBoostCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get the Animator component
        originalSpeed = moveSpeed; // Store the original speed
        UpdatePlayerHealthUI(); // Initialize the health UI
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal input (A and D keys or left and right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Check if the player is pressing the shift key or the "W" key
        bool isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.W);

        // Set the isBoosting parameter in the Animator
        animator.SetBool("isBoosting", isBoosting);

        // Calculate the movement direction based on the camera's forward vector
        Vector3 movement = transform.forward;

        // Apply the boost multiplier if the shift key or the "W" key is pressed
        float effectiveMoveSpeed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;

        // Apply a constant force to the player in the direction of the camera's forward vector
        rb.AddForce(movement * effectiveMoveSpeed, ForceMode.Force);

        // Check if the player is pressing "A" or "D" and if they are grounded
        if (horizontalInput != 0 && IsGrounded())
        {
            // Calculate the new rotation angle
            float rotationAngle = horizontalInput > 0 ? -rotationSpeed * Time.deltaTime : rotationSpeed * Time.deltaTime;

            // Check if the new rotation would exceed the limit
            if (Mathf.Abs(currentRotation + rotationAngle) <= rotationLimit)
            {
                // Rotate the player around the Y-axis within the limit
                transform.Rotate(0f, rotationAngle, 0f, Space.World);
                currentRotation += rotationAngle; // Update the current rotation
            }
        }
    }

    // Check if the player is grounded
    bool IsGrounded()
    {
        // Perform a raycast downwards to check for ground
        return Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedFlag"))
        {
            // Start the speed boost coroutine
            if (speedBoostCoroutine != null)
            {
                StopCoroutine(speedBoostCoroutine);
            }
            speedBoostCoroutine = StartCoroutine(SpeedBoost(1f)); // Boost lasts for 1 second
            Destroy(other.gameObject); // Destroy the speed flag after collection
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= boostMultiplier; // Apply the speed boost
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed; // Reset the speed after the duration
        speedBoostCoroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Decrease player health when colliding with an obstacle
            playerHealth -= 20f; // Adjust the value as needed
            UpdatePlayerHealthUI(); // Update the health UI

            // Check if the player has no health left
            if (playerHealth <= 0)
            {
                // Handle player death
                Debug.Log("Player has died!");
                // You can add code to respawn the player or end the game
            }
        }
    }

    // Method to update the health UI
private void UpdatePlayerHealthUI()
{
    if (playerHealthUI != null)
    {
        playerHealthUI.text = playerHealth.ToString();
        
        // Check if the player's health is zero or less
        if (playerHealth <= 0)
        {
            // Destroy the player game object
            Destroy(gameObject);
            
            // Restart the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
}