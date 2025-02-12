using UnityEngine;
using UnityEngine.SceneManagement;  // Import SceneManagement for scene loading

public class StartPlaying : MonoBehaviour
{
    // Timer variables
    private float timer = 0f;
    private bool isTimerRunning = false;

    // Method to change scene and start the timer
    public void StartGame()
    {
        // Start the timer when the game starts
        isTimerRunning = true;

        // Load the game scene (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("Minigame");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            // Update the timer if it's running
            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer.ToString("F2")); // You can replace this with UI display if needed
        }
    }
}
