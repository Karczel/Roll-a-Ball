using UnityEngine;

public class ballAnimate : MonoBehaviour
{
    public Transform cameraTransform; // Assign the camera in the Inspector
    public float fadeInAngle = 45f; // Angle at which the ball starts fading in
    public float fullVisibilityAngle = 60f; // Angle at which the ball is fully visible
    public float fadeSpeed = 2f; // Speed of fade-in effect

    private Renderer ballRenderer;
    private Material ballMaterial;
    private bool hasFadedIn = false; // To ensure fade-in only happens once

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballMaterial = ballRenderer.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ballMaterial == null || hasFadedIn) return;

        // Get the camera's forward direction relative to world up
        float cameraAngle = Vector3.Angle(Vector3.forward, cameraTransform.forward);

        // Only start fading in when the camera angle exceeds fadeInAngle
        if (cameraAngle >= fadeInAngle)
        {
            float alpha = Mathf.Clamp01((cameraAngle - fadeInAngle) / (fullVisibilityAngle - fadeInAngle));

            // Apply transparency (fade-in effect)
            Color color = ballMaterial.color;
            color.a = Mathf.Lerp(color.a, alpha, Time.deltaTime * fadeSpeed);
            ballMaterial.color = color;

            // Once fully visible, stop the fade
            if (cameraAngle >= fullVisibilityAngle)
            {
                hasFadedIn = true;
            }
        }
    }
}
