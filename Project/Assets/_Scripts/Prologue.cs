using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    bool allowMove = false;
    //private AudioSource[] allAudioSources;
    // Use this for initialization
    void Start()
    {
        //allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        //foreach (AudioSource audioS in allAudioSources)
        //{
        //    if (audioS.tag == "BGM")
        //        audioS.Stop();
        //}
        GameManager.Instance.CanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMove)
        {
            GameManager.Instance.CanMove = true;
            //Debug.Log(GameManager.Instance.CanMove);
        }

        if (Input.anyKeyDown)
        {
            //    foreach (AudioSource audioS in allAudioSources)
            //    {
            //        if (audioS.tag == "BGM")
            //            audioS.Play();
            //    }
            allowMove = true;
            for (int i = 0; i < gameObject.GetComponentsInChildren<CanvasRenderer>().Length; i++)
                gameObject.GetComponentsInChildren<CanvasRenderer>()[i].SetAlpha(0.0f);
            //StartCoroutine(Moveable());
        }
    }

    //public IEnumerator Moveable()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    gameObject.SetActive(false);
    //}
}
