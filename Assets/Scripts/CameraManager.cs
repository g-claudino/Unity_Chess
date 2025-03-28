using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class CameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera cm;
    [FormerlySerializedAs("sensitivity")] [SerializeField,Range(1f,10f)] private float editorSensitivity;
    [SerializeField,Range(1f,10f)] private float zoomSensitivity;
    private float sensitivityCode;
    private float zoomCode;
    private float cameraCode;
    private Vector3 velocity;
    [SerializeField] private Player player;
    [SerializeField,Range(1f,10f)] private float cameraSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private void Start()
    {
        cm ??= Camera.main;
        ApplyScale();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 cameraPosition = cm.transform.position;
        Vector3 playerPosition = player.transform.position;
        float mouseWheelY = Input.mouseScrollDelta.y; 
        float angle = -Input.mousePositionDelta.x * sensitivityCode;
        player.transform.Rotate(Vector3.up, -angle, Space.Self);
        transform.Rotate(Vector3.forward, angle, Space.Self);
        float distance = Mathf.Clamp(cameraPosition.y+mouseWheelY * zoomCode,minDistance,maxDistance);
        Vector3 targetPosition = new Vector3(playerPosition.x, distance , playerPosition.z);
        //Vector3.SmoothDamp(cameraPosition, targetPosition, ref velocity, 0.1f);
        //transform.position = velocity*cameraSpeed+cameraPosition;
        transform.position = targetPosition;
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
}
