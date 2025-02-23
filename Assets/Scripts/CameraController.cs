using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target1;
    public Transform Target2;
    public Vector3 PositionOffset;
    public float AccelerationTime;
    public float OrthographicSizeOffset;

    private Vector3 _velocitySmoothing;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 center = (Target1.transform.position + Target2.transform.position) * 0.5f;
       
        transform.position = Vector3.SmoothDamp(transform.position, center + PositionOffset, ref _velocitySmoothing, AccelerationTime);
    
        SetCameraSize(OrthographicSizeOffset);
    }

    private void SetCameraSize(float offset)
    {
        //horizontal size is based on actual screen ratio
        int ratio = Screen.width / Screen.height;
        float maxSizeX = offset + ratio;
        
        //minimum aspect ratio that fits both targets
        float width = Mathf.Abs(Target1.position.x - Target2.position.x) * 0.5f;
        float height = Mathf.Abs(Target1.position.y - Target2.position.y) * 0.5f;
        
        //computing the size
        float camSizeX = Mathf.Max(width * ratio, maxSizeX);
        
        _mainCamera.orthographicSize = Mathf.Max(height + offset, camSizeX + offset);
    }
    
}
