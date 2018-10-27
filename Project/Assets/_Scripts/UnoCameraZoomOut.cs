using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnoCameraZoomOut : MonoBehaviour
{
    Vector3 finalPos;
    float smoothing = 0.05f;

    AsyncOperation scene;


    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(-6.0f, 2.3f, -1.92f);
        finalPos = new Vector3(-13.0f, 9.9f, -5.7f);

        scene = SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Single);
        scene.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPos, smoothing * Time.deltaTime);

        //if (transform.position.x <= -9.2)
        //{
        //    GameManager.Instance.FadeIn();
        //}

        if (transform.position.x <= -9.7f)
        {
            //Debug.Log("bye");
            scene.allowSceneActivation = true;
        }
    }

}
