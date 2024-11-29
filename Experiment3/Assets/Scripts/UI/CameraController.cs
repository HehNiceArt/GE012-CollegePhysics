using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private float minYPosition = 0f;
    [SerializeField] private float normalSmoothTime = 0.2f;
    [SerializeField] private float fastSmoothTime = 0.05f;
    [SerializeField] private float bottomThreshold = 0.2f; 

    private void Update()
    {
        float playerVerticalSpeed = target.GetComponent<Rigidbody2D>().velocity.y;

        float cameraBottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, -offset.z)).y;

        bool isPlayerNearBottom = target.position.y <= cameraBottomEdge + bottomThreshold;

        float currentSmoothTime = (playerVerticalSpeed < 0 && isPlayerNearBottom) ? fastSmoothTime : normalSmoothTime;

        float targetYPosition = Mathf.Max(target.position.y + offset.y, minYPosition);
        Vector3 targetPosition = new Vector3(transform.position.x, targetYPosition, offset.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, currentSmoothTime);
    }
}
