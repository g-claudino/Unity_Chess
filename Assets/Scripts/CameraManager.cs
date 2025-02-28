using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera cm;
    [FormerlySerializedAs("sensitivity")] [SerializeField,Range(1f,10f)] private float editorSensitivity;
    private float sensitivityCode;
    private void Start()
    {
        if (cm == null)
        {
            cm = Camera.main;
        }
        sensitivityCode = editorSensitivity / 10f;
    }

    // Update is called once per frame
    private void Update()
    {
        float angle = Input.mousePositionDelta.x*sensitivityCode;
        transform.Rotate(Vector3.up, angle);
    }

    private void OnValidate()
    {
        sensitivityCode = editorSensitivity / 10f;
    }
}
