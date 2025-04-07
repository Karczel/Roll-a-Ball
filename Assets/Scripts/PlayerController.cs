using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 

    private float movementX;
    private float movementY;

    public float speed = 0; 
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public Button playAgain;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Smoother control
        rb.mass = 1f;
        rb.linearDamping = 0.01f; 
        rb.angularDamping = 0.01f; 
        rb.interpolation = RigidbodyInterpolation.Interpolate; 

        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        playAgain.gameObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
         // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        { // Win
            winTextObject.SetActive(true);
            DisableEnemies();
            playAgain.gameObject.SetActive(true);
        }
    }

    void DisableEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

    }

    void FixedUpdate()
    {
         // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

         // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
