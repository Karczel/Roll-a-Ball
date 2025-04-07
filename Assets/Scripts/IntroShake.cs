using UnityEngine;

public class IntroShake : MonoBehaviour
{
    public float maxRotation = 30f;
    public float speed = 2f; // Adjust speed as needed
    private float timeCounter = 0f;
    private float rotationOffset;

    private RectTransform rectTransform; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Get UI RectTransform
        rotationOffset = rectTransform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation angle using a sine wave
        float angle = maxRotation * Mathf.Sin(timeCounter * speed);
        
        // Apply the rotation to the object
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationOffset + angle);

        // Increment time counter
        timeCounter += Time.deltaTime;
    }
}
