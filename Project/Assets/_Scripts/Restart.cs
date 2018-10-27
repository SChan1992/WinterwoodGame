using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    AsyncOperation scene;
    void Start()
    {
        scene = SceneManager.LoadSceneAsync("StartMenu", LoadSceneMode.Single);
        scene.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            scene.allowSceneActivation = true;
        }
    }
}
