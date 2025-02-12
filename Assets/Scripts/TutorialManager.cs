using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f; // Freeze the game
        gameObject.SetActive(true);
    }

     void Update()
    {
        // Detect any mouse click and dismiss the tutorial
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            DismissTutorial();
        }
    }

    public void DismissTutorial()
    {
        Time.timeScale = 1f; // Unfreeze the game
        gameObject.SetActive(false);
    }
}
