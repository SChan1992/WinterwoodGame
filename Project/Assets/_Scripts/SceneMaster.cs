using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{

    public Flowchart flowChart;
    AsyncOperation scene;

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            scene = SceneManager.LoadSceneAsync("DayTwo", LoadSceneMode.Single);
            scene.allowSceneActivation = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            scene = SceneManager.LoadSceneAsync("DayThree", LoadSceneMode.Single);
            scene.allowSceneActivation = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            scene = SceneManager.LoadSceneAsync("DayFour", LoadSceneMode.Single);
            scene.allowSceneActivation = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            scene = SceneManager.LoadSceneAsync("DayFive", LoadSceneMode.Single);
            scene.allowSceneActivation = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            scene = SceneManager.LoadSceneAsync("DaySix", LoadSceneMode.Single);
            scene.allowSceneActivation = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            scene = SceneManager.LoadSceneAsync("DaySeven", LoadSceneMode.Single);
            scene.allowSceneActivation = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 14)
        {
            scene = SceneManager.LoadSceneAsync("EndScene", LoadSceneMode.Single);
            scene.allowSceneActivation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(loadNextDay);
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (flowChart.GetBooleanVariable("isDay"))
            {
                scene.allowSceneActivation = true;
                flowChart.SetBooleanVariable("isDay", false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (flowChart.GetBooleanVariable("isDay"))
            {
                scene.allowSceneActivation = true;
                flowChart.SetBooleanVariable("isDay", false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (flowChart.GetBooleanVariable("isDay"))
            {
                scene.allowSceneActivation = true;
                flowChart.SetBooleanVariable("isDay", false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            if (flowChart.GetBooleanVariable("isDay"))
            {
                scene.allowSceneActivation = true;
                flowChart.SetBooleanVariable("isDay", false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            if (flowChart.GetBooleanVariable("isDay"))
            {
                scene.allowSceneActivation = true;
                flowChart.SetBooleanVariable("isDay", false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            if (flowChart.GetBooleanVariable("isDay"))
            {
                scene.allowSceneActivation = true;
                flowChart.SetBooleanVariable("isDay", false);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 14) // end game
        {
            if (flowChart.GetBooleanVariable("isDay"))
            {
                scene.allowSceneActivation = true;
                flowChart.SetBooleanVariable("isDay", false);
            }
        }
    }
}
