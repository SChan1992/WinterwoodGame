using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Camera mainCamera;
    public Camera secondaryCamera;
    public CanvasGroup uiElement;

    //
    public GameObject Nick;
    public GameObject Pan;
    public GameObject Ashe;
    //



    #region private

    [SerializeField]
    private static bool canMove;

    private static GameManager _instance;
    private AudioSource[] allAudioSources;

    #endregion

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        mainCamera.enabled = true;
        mainCamera.GetComponent<AudioListener>().enabled = true;
        secondaryCamera.enabled = false;
        secondaryCamera.GetComponent<AudioListener>().enabled = false;
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    private void Update()
    {
        // debug purpose
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 10.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                canMove = false;
                foreach (AudioSource audioS in allAudioSources)
                {
                    if (audioS.tag == "BGM")
                        audioS.Stop();
                }
            }
            else
            {
                Time.timeScale = 1.0f;
                canMove = true;
                foreach (AudioSource audioS in allAudioSources)
                {
                    if (audioS.tag == "BGM")
                        audioS.Play();
                }
            }
        }
    }

    public void ChangeCamera()
    {
        mainCamera.enabled = true;
        mainCamera.GetComponent<AudioListener>().enabled = true;
        secondaryCamera.enabled = false;
        secondaryCamera.GetComponent<AudioListener>().enabled = false;
        canMove = true;
    }

    public void StopMove()
    {
        canMove = false;
    }


    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1.0f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0.0f));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 2.0f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1)
            {
                percentageComplete = 1.0f;
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        //Debug.Log("done");
    }


}