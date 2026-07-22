using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Horizontal Follow")]
    [SerializeField] private float smoothSpeed = 0.2f;
    [SerializeField] private float aheadDistance = 2f;
    [SerializeField] private float lookAheadSpeed = 3f;

    [Header("Vertical Follow")]
    [SerializeField] private float verticalAmount = 0.3f;
    [SerializeField] private float verticalSmooth = 2f;

    private float lookAhead;
    private Vector3 velocity = Vector3.zero;

    private float startPlayerY;
    private float startCameraY;

    private void Start()
    {
        startPlayerY = player.position.y;
        startCameraY = transform.position.y;
    }

    private void LateUpdate()
    {
        // Look ahead horizontal
        lookAhead = Mathf.Lerp(
            lookAhead,
            aheadDistance * player.localScale.x,
            Time.deltaTime * lookAheadSpeed
        );

        // Selisih tinggi player dari posisi awal
        float heightDifference = player.position.y - startPlayerY;

        // Kamera ikut naik sedikit
        float targetY = startCameraY + (heightDifference * verticalAmount);

        Vector3 targetPosition = new Vector3(
            player.position.x + lookAhead,
            targetY,
            transform.position.z
        );

        // Smooth movement
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed
        );
    }
}