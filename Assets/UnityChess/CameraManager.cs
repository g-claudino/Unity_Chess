using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera cm;
    [SerializeField] [Range(1f, 10f)] private float editorSensitivity;
    [SerializeField] [Range(1f, 10f)] private float zoomSensitivity;
    [SerializeField] private Player player;
    [SerializeField] [Range(1f, 10f)] private float cameraSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private float cameraCode;

    private float sensitivityCode;

    private Vector3 velocity;
    private float zoomCode;


    private void Start()
    {
        cm ??= Camera.main;
        ApplyScale();
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null) FollowPlayer();
    }

    private void OnValidate()
    {
        ApplyScale();
    }

    private void ApplyScale()
    {
        sensitivityCode = editorSensitivity / 10f;
        zoomCode = zoomSensitivity / 10f;
        cameraCode = cameraSpeed / 100f;
    }

    private void FollowPlayer()
    {
        Vector3 cameraPosition = cm.transform.position;
        Vector3 playerPosition = player.transform.position;
        float mouseWheelY = Input.mouseScrollDelta.y;
        float angle = -Input.mousePositionDelta.x * sensitivityCode;
        player.transform.Rotate(Vector3.up, -angle, Space.Self);
        transform.Rotate(Vector3.forward, angle, Space.Self);
        float distance = Mathf.Clamp(cameraPosition.y + mouseWheelY * zoomCode, minDistance, maxDistance);
        Vector3 targetPosition = new Vector3(playerPosition.x, distance, playerPosition.z);
        //Vector3.SmoothDamp(cameraPosition, targetPosition, ref velocity, 0.1f);
        //transform.position = velocity*cameraSpeed+cameraPosition;
        transform.position = targetPosition;
    }
}