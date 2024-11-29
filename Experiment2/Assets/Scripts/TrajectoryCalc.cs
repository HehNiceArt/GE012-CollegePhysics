using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryCalc : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] float initialVelocity;
    [SerializeField] float angle;
    [SerializeField] TextMeshProUGUI timeDisplay, velDisplay, angleDisplay, distanceDisplay;
    public bool isPressed;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPressed = true;
            LaunchItem(item, initialVelocity, angle);

            float timeTaken = CalculateTimeofFlight(initialVelocity, angle);
            timeDisplay.SetText("Time: " + Mathf.Abs(timeTaken) + " s");

            float dist = DistanceTraveled(initialVelocity, angle);
            distanceDisplay.SetText("Distance: " + Mathf.Abs(dist) + " m");

        }
        angleDisplay.SetText("Angle:" + " 32°");
        velDisplay.SetText("Speed:" + " 15 m/s");
    }
    public void LaunchItem(GameObject obj, float initialVelocity, float angle)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float angleRad = angle * Mathf.Deg2Rad;

            Vector2 velocity = new Vector2(initialVelocity * Mathf.Cos(angleRad), initialVelocity * Mathf.Sin(angleRad));

            rb.velocity = velocity;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object has collided with the ground
        if (collision.gameObject.CompareTag("Ground")) // Ensure your ground has the "Ground" tag
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Freeze the Rigidbody2D's position to stop sliding
                rb.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze all movement
            }
        }
    }
    public float CalculateTimeofFlight(float initialVelocity, float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;

        float timeOfFlight = 2 * initialVelocity * Mathf.Sin(angleRad) / Physics.gravity.y;
        return timeOfFlight;
    }
    public float DistanceTraveled(float initialVelocity, float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        float distance = (initialVelocity * initialVelocity * Mathf.Sin(angleRad)) / Physics.gravity.y;

        return distance;
    }
}
