using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionText : MonoBehaviour
{

    private AudioSource[] allAudioSources;
    //public CanvasGroup uiElement;

    bool allowMove = false;

    // Use this for initialization
    void Start()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS.tag == "BGM")
                audioS.Stop();
        }

        GameManager.Instance.CanMove = false;
    }

    void Update()
    {
        if (allowMove)
        {
            GameManager.Instance.CanMove = true;
        }

        else if (Input.anyKeyDown && !allowMove)
        {
            allowMove = true;
            //FadeOut();
            foreach (AudioSource audioS in allAudioSources)
            {
                if (audioS.tag == "BGM")
                    audioS.Play();
            }
            for (int i = 0; i < gameObject.GetComponentsInChildren<CanvasRenderer>().Length; i++)
            {
                gameObject.GetComponentsInChildren<CanvasRenderer>()[i].SetAlpha(0.0f);
            }
        }
    }

    //public void FadeOut()
    //{
    //    StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0.0f));
    //}

    //public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 2.0f)
    //{
    //    float _timeStartedLerping = Time.time;
    //    float timeSinceStarted = Time.time - _timeStartedLerping;
    //    float percentageComplete = timeSinceStarted / lerpTime;

    //    while (true)
    //    {
    //        timeSinceStarted = Time.time - _timeStartedLerping;
    //        percentageComplete = timeSinceStarted / lerpTime;

    //        float currentValue = Mathf.Lerp(start, end, percentageComplete);

    //        cg.alpha = currentValue;

    //        if (percentageComplete >= 1)
    //        {
    //            percentageComplete = 1.0f;
    //            break;
    //        }

    //        yield return new WaitForEndOfFrame();
    //    }
    //}

}
