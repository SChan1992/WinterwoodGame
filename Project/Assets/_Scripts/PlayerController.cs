using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    #region Public Properties

    public LayerMask movementMask;
    public LayerMask objectMask;
    public LayerMask houseMask;
    public float maxRayDist;
    public float maxDoorDist;

    [SerializeField]
    public GameObject JackMesh;
    public GameObject Arrow;

    #endregion

    //

    #region Private Properties

    [SerializeField]
    Interactable focus;

    [SerializeField]
    Vector3 mMovement;
    //float mSpeed;

    //Rigidbody mPlayerRigidBody;
    [SerializeField]
    Camera cam;

    [SerializeField]
    PlayerMotor motor;


    [SerializeField]
    Animator doorAnimator;

    [SerializeField]
    RaycastHit hit;

    [SerializeField]
    Vector3 temp;
    //
    //private Vector3 origin;
    //private Vector3 direction;

    [SerializeField]
    GameObject arrowObj;

    [SerializeField]
    bool hasInstance;

    //private float currentHitDistance;
    [SerializeField]
    private string houseName;

    Animator jackAnimator;
    //Rigidbody jackRB;

    NavMeshAgent navAgent;

    #endregion

    //

    #region Unity CallBacks

    private void Start()
    {
        // player movement
        //mSpeed = 6.0f;

        //
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        //mPlayerRigidBody = GetComponent<Rigidbody>();
        maxRayDist = 100.0f;
        maxDoorDist = 20.0f;

        GameManager.Instance.CanMove = true;
        //maxDistance = 10.0f;
        //sphereRadius = 2.0f;
        houseName = "";

        jackAnimator = JackMesh.GetComponentInChildren<Animator>();

        navAgent = GetComponent<NavMeshAgent>();

        //Debug.Log(jackAnimator);
        hasInstance = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, navAgent.destination) > navAgent.stoppingDistance)
        {
            //Debug.Log("up");
            jackAnimator.SetTrigger("JackMoveTrigger");
        }
        else
        {
            //Debug.Log("down");
            jackAnimator.SetTrigger("JackNotMove");

            if (arrowObj != null)
            {
                DestroyImmediate(arrowObj, false);
                hasInstance = false;
            }

        }

    }

    private void LateUpdate()
    {


        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxRayDist, movementMask) && GameManager.Instance.CanMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                // Move our player to what we hit

                motor.MoveToPoint(hit.point);
                //moveStartTime = Time.time;
                // stop focusing any objects

                //Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log(clickedPosition);
                //Instantiate(Arrow, clickedPosition, Quaternion.identity);
                if (!hasInstance)
                {
                    Vector3 pointToSpawn = hit.point;
                    Vector3 prefabOffset = new Vector3(0.0f, 2.0f, 0.0f);
                    arrowObj = Instantiate(Arrow, pointToSpawn + prefabOffset, Quaternion.identity) as GameObject;
                    hasInstance = true;
                }
                else
                {
                    if (arrowObj != null)
                    {
                        DestroyImmediate(arrowObj, false);
                        //hasInstance = false;

                        Vector3 pointToSpawn = hit.point;
                        Vector3 prefabOffset = new Vector3(0.0f, 2.0f, 0.0f);
                        arrowObj = Instantiate(Arrow, pointToSpawn + prefabOffset, Quaternion.identity) as GameObject;
                        hasInstance = true;
                    }
                }
                RemoveFocus();

            }
        }

        if (Physics.Raycast(ray, out hit, maxRayDist, objectMask) && GameManager.Instance.CanMove)
        {

            if (Input.GetMouseButtonDown(0))
            {
                motor.MoveToPoint(transform.position);
                // check if we hit an interactable
                // if we did set it as our focus
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                //Debug.Log(interactable);
                if (interactable != null)
                {
                    //Debug.Log("interactable not null");
                    SetFocus(interactable);
                }
            }

            //if (hit.collider.name == ")
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, maxDoorDist, houseMask) && GameManager.Instance.CanMove)
            {
                motor.MoveToPoint(gameObject.transform.position);
                //Debug.Log(hit.collider.gameObject.tag);
                //Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.gameObject.tag == "door")
                {
                    //Debug.Log("hit a door!");
                    houseName = hit.collider.gameObject.name;

                    // play sound
                    if (!hit.collider.gameObject.GetComponent<AudioSource>().isPlaying)
                    {
                        hit.collider.gameObject.GetComponent<AudioSource>().Play();
                    }

                    // disable collider to prevent knocking again
                    hit.collider.gameObject.GetComponent<MeshCollider>().enabled = false;

                    // Play Animation
                    // door slight opens 
                    // move character back 2 steps
                    doorAnimator = hit.collider.gameObject.GetComponent<Animator>();
                    doorAnimator.SetTrigger("OpeningDoor");

                    // fade black
                    GameManager.Instance.FadeIn();
                    StartCoroutine(waitForFadeOut(2.5f));

                }
            }

        }
        //}
    }

    #endregion

    //

    #region Private Methods


    void SetFocus(Interactable newFocus)
    {
        //Debug.Log("Set Focus");
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(gameObject);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();

    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draws a 5 unit long red line in front of the object
    //    Gizmos.color = Color.red;
    //    Debug.DrawLine(origin, origin + direction * currentHitDistance);
    //    Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    //}

    IEnumerator waitForFadeOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // TODO door should not swing open
        GameManager.Instance.FadeOut();
        doorAnimator.SetBool("Opened", true);
        switch (houseName)
        {
            case "NickHouse":
                GameManager.Instance.Nick.gameObject.SetActive(true);
                break;
            case "PanHouse":
                GameManager.Instance.Pan.gameObject.SetActive(true);
                break;
            case "AsheHouse":
                GameManager.Instance.Ashe.gameObject.SetActive(true);
                break;
        }
    }

    #endregion

}