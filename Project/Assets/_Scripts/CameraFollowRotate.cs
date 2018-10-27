using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRotate : MonoBehaviour
{

    //[SerializeField]
    //private GameObject target;

    //[SerializeField]
    //private Vector3 offsetPosition;

    //[SerializeField]
    //private Space offsetPositionSpace = Space.Self;

    //[SerializeField]
    //private bool lookAt = true;

    //[SerializeField]
    //float currentZoom;

    //#region Public Properties

    //// The speed with which the camera will be following.
    //public float smoothing;
    //public float pitch;
    //public float zoomSpeed;
    //public float minZoom;
    //public float maxZoom;
    //public float smoothTime;

    //// raycast related
    //public float maxRayDist;
    //public LayerMask movementMask;
    //public GameObject currentHitObject;

    //#endregion

    //#region Private Properties

    ////RaycastHit hit;
    ////Camera cam;
    //float currentHitDistance;
    //Vector3 origin;
    //Vector3 direction;

    //#endregion

    //void Start()
    //{
    //    smoothing = 100.0f;
    //    pitch = 2.0f;

    //    // zoom
    //    currentZoom = 10.0f;
    //    zoomSpeed = 4.0f;
    //    minZoom = 5.0f;
    //    maxZoom = 15.0f;
    //    smoothTime = 2.5f;

    //    maxRayDist = 5.0f;
    //    //cam = GetComponent<Camera>();
    //}

    ////void Update()
    ////{
    ////    currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
    ////    currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

    ////    //origin = transform.position;
    ////    ////Vector3 rayHeightOffset = new Vector3(0.0f, 0.5f, 0.0f);
    ////    ////direction = target.gameObject.transform.position - origin;
    ////    //direction = gameObject.transform.forward * 3.0f;
    ////    ////Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    ////    //RaycastHit hit;
    ////    //if (Physics.Raycast(origin, direction, out hit, maxRayDist, movementMask, QueryTriggerInteraction.UseGlobal) && GameManager.Instance.CanMove)
    ////    //{
    ////    //    currentHitObject = hit.transform.gameObject;
    ////    //    currentHitDistance = hit.distance;
    ////    //    //Debug.Log(hit.transform.gameObject);
    ////    //    if (hit.transform.tag == "door")
    ////    //    {
    ////    //        Debug.Log(hit.transform.gameObject);
    ////    //        hit.transform.gameObject.SetActive(false);
    ////    //    }
    ////    //}
    ////    //else
    ////    //{
    ////    //    currentHitDistance = maxRayDist;
    ////    //    currentHitObject = null;
    ////    //}

    ////}


    //private void LateUpdate()
    //{
    //    Refresh();
    //}

    //public void Refresh()
    //{
    //    if (target == null)
    //    {
    //        Debug.LogWarning("Missing target ref !", this);

    //        return;
    //    }

    //    // compute position
    //    if (offsetPositionSpace == Space.Self)
    //    {
    //        transform.position = target.transform.TransformPoint(offsetPosition);
    //    }
    //    else
    //    {
    //        transform.position = target.transform.position + offsetPosition * currentZoom;
    //        transform.LookAt(target.transform.position + Vector3.up * pitch);
    //        Vector3 targetCamPos = transform.position;
    //        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;

    //        transform.position = Vector3.Slerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    //        transform.position = Vector3.SmoothDamp(transform.position, targetCamPos, ref targetVelocity, smoothTime);
    //    }

    //    // compute rotation
    //    if (lookAt)
    //    {
    //        transform.LookAt(target.transform);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, smoothing * Time.deltaTime);
    //        //TODO possible clamp
    //        // need Angular Velocity (need RB)

    //    }
    //}

    ////private void CameraRotate()
    ////{

    ////}

    ////private void OnDrawGizmosSelected()
    ////{
    ////    Gizmos.color = Color.red;
    ////    Debug.DrawLine(origin, origin + currentHitDistance * direction);
    ////    Gizmos.DrawWireSphere(origin + direction * currentHitDistance, 3.0f);
    ////}

    //////////////////////////////////////
    //public Transform player;
    //public Vector3 lookOffset = new Vector3(0, 4, 0);
    //public float distance = 10;
    //public float cameraSpeed = 5;

    //void Update()
    //{
    //    Vector3 lookPosition = player.position + lookOffset;
    //    this.transform.LookAt(lookPosition);

    //    if (Vector3.Distance(this.transform.position, lookPosition) > distance)
    //    {
    //        this.transform.Translate(0, 0, cameraSpeed * Time.deltaTime);
    //    }
    //}
    //////////////////////////////////////
    /// 

    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    Vector3 followPos;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    public Vector3 lookOffset = new Vector3(0, 4, 0);
    public float distance = 10.0f;
    public float cameraSpeed = 5.0f;

    private bool rightClicked = false;
    private bool leftClicked = false;

    private void Start()
    {
        // camera should use euler
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        // lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            leftClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftClicked = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightClicked = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightClicked = false;
        }

    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        //if (leftClicked)
        //{
        Vector3 lookPosition = PlayerObj.transform.position + lookOffset;
        this.transform.LookAt(lookPosition);

        if (Vector3.Distance(this.transform.position, lookPosition) > distance)
        {
            this.transform.Translate(0, 0, cameraSpeed * Time.deltaTime);
        }
        //}

        if (rightClicked)
        {
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputZ = Input.GetAxis("RightStickVertical");
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            finalInputX = inputX + mouseX;
            finalInputZ = inputZ + mouseY;

            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX += finalInputZ * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }
}