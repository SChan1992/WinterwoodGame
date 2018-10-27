using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNightTwo : MonoBehaviour
{
    AsyncOperation scene;
    // Use this for initialization
    void Start()
    {
        scene = SceneManager.LoadSceneAsync("NightTwo", LoadSceneMode.Single);
        scene.allowSceneActivation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 3) // day 2
            {
                scene.allowSceneActivation = true;
            }

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
