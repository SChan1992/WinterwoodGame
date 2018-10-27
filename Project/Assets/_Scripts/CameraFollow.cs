using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Public Properties

    // The position that that camera will be following.
    public Transform target;

    // The speed with which the camera will be following.
    public float smoothing;
    public float pitch;
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    //public float yawSpeed;
    //public float minYyaw;
    //public float maxYyaw;

    #endregion

    //

    #region Private Properties

    private Vector3 offset;

    // The initial offset from the target.
    float currentZoom;

    //float yawInput;

    //private float currentYawX;
    //private float currentYawY;

    #endregion

    //

    #region Unity Callbacks

    void Start()
    {
        // Calculate the initial offset.
        //offset = transform.position - target.position;
        offset = new Vector3(0.0f, -0.6f, 1.0f);

        smoothing = 5.0f;
        pitch = 2.0f;

        // zoom
        currentZoom = 10.0f;
        zoomSpeed = 4.0f;
        minZoom = 5.0f;
        maxZoom = 15.0f;

        //// yaw
        //yawSpeed = 100.0f;
        ////yawInput = 0.0f;
        //minYyaw = -10.0f;
        //maxYyaw = 60.0f;
        //currentYawX = 0.0f;
        //currentYawY = 0.0f;
    }

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        //if (Input.GetMouseButton(1))
        //{
        //currentYawX += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
        //currentYawY -= Input.GetAxis("Mouse Y") * yawSpeed * Time.deltaTime;
        //currentYawY = Mathf.Clamp(currentYawY, minYyaw, maxYyaw);
        //}
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        //transform.RotateAround(target.position, Vector3.up, currentYawX);
        //transform.RotateAround(target.position, transform.right, currentYawY);
    }

    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

    }

    #endregion


}
