using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNightOne : MonoBehaviour
{
    AsyncOperation scene;
    // Use this for initialization
    void Start()
    {
        scene = SceneManager.LoadSceneAsync("FirstNight", LoadSceneMode.Single);
        scene.allowSceneActivation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            scene.allowSceneActivation = true;

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
