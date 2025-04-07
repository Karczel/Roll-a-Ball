using UnityEngine;

public class TransparencyOnBlock : MonoBehaviour
{
    public Transform ballTransform;   
    public float maxDistance = 100f; 
    public float transparency = 0.2f;

    private MaterialPropertyBlock propertyBlock; // To store material properties temporarily


    void Start()
    {
        // Initialize MaterialPropertyBlock
        propertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        // Cast a ray from the camera (this object) to the ball
        Ray ray = new Ray(transform.position, ballTransform.position - transform.position);
        RaycastHit hit;

        // Draw a debug ray in the scene for visualization
        Debug.DrawRay(transform.position, ballTransform.position - transform.position, Color.red);

        // If something is hit between the camera and the ball
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Log the name of the hit object for debugging
            Debug.Log("Ray Hit: " + hit.collider.gameObject.name);

            // Check if the object is between the camera and the ball
            if (hit.collider.gameObject != ballTransform.gameObject)
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material material = renderer.material;

                    // Set to transparent mode (URP Lit Shader)
                    if (material.HasProperty("_Surface"))
                    {
                        material.SetFloat("_Surface", 1); // Set Surface Type to Transparent
                        material.SetFloat("_Blend", 0);    // Set Blend Mode to Alpha
                        material.SetFloat("_AlphaClip", 1);  // Enable Alpha Clipping
                        material.SetFloat("_AlphaCutoff", 0); // Set Alpha Cutoff to 0 (auto threshold)

                    }

                    // Set the transparency level
                    Color color = material.color;
                    color.a = transparency; // Apply semi-transparency
                    material.color = color;

                    // Log when transparency is set
                    Debug.Log("Set transparency to: " + transparency);
                }
                else
                {
                    Debug.LogWarning("Hit object does not have a Renderer component.");
                }
            }
        }
        else
        {
            // Raycast didn't hit anything, restore to opaque
            RestoreOpaque(hit.collider);
        }
    }

    void RestoreOpaque(Collider hitCollider)
    {
        // If the object is no longer blocking, set it back to opaque
        if (hitCollider != null)
        {
            Renderer renderer = hitCollider.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;

                // Set to opaque mode
                if (material.HasProperty("_Surface"))
                {
                    material.SetFloat("_Surface", 0);  // Set Surface Type back to Opaque
                    material.SetFloat("_AlphaClip", 0); // Disable Alpha Clipping (reset to opaque behavior)
                    material.SetFloat("_AlphaCutoff", 0.5f); // Reset Alpha Cutoff to default


                }

                // Restore the original alpha (fully opaque)
                Color color = material.color;
                color.a = 1f; // Fully opaque
                material.color = color;

                // Log when opaque is restored
                Debug.Log("Restored to opaque.");
            }
        }
    }
}
