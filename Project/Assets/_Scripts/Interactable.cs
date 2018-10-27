using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    #region Public Properties

    [Range(0.0f, 5.0f)]
    public float radius = 3.0f;

    public Transform interactionTransform;
    //public GameManager gameManager;

    #endregion

    #region Private Properties

    [SerializeField]
    bool mIsFocus = false;

    [SerializeField]
    bool hasInteracted = false;

    [SerializeField]
    GameObject mPlayer;


    #endregion

    #region Unity CallBacks

    private void Start()
    {
        mPlayer = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //Debug.Log("mIsFocus: " + mIsFocus + " hasInteracted: " + hasInteracted + ", mPlayer " + mPlayer + " interaction transform " + interactionTransform);
        if (mIsFocus && !hasInteracted && mPlayer != null && interactionTransform != null)
        {
            float distance = Vector3.Distance(mPlayer.transform.position, interactionTransform.position);
            //Debug.Log("distance: " + distance + " radius" + radius);
            if (distance <= radius)
            {
                //Debug.Log("INTERACT");
                GameManager.Instance.CanMove = false;
                Interact();
                hasInteracted = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    #endregion

    public virtual void Interact()
    {
        //Debug.Log("interact");
        //Debug.Log(GameManager.Instance.mainCamera);
        //Debug.Log(GameManager.Instance.secondaryCamera);
        GameManager.Instance.mainCamera.enabled = false;
        GameManager.Instance.mainCamera.GetComponent<AudioListener>().enabled = false;
        GameManager.Instance.secondaryCamera.enabled = true;
        GameManager.Instance.secondaryCamera.GetComponent<AudioListener>().enabled = true;

        switch (transform.tag)
        {
            case "Nick":
                if (SceneManager.GetActiveScene().buildIndex == 1)
                    Fungus.Flowchart.BroadcastFungusMessage("NickDayOne");
                else if (SceneManager.GetActiveScene().buildIndex == 3)
                    Fungus.Flowchart.BroadcastFungusMessage("NickDayTwo");
                else if (SceneManager.GetActiveScene().buildIndex == 5)
                    Fungus.Flowchart.BroadcastFungusMessage("NickDayThree");
                else if (SceneManager.GetActiveScene().buildIndex == 7)
                    Fungus.Flowchart.BroadcastFungusMessage("NickDayFour");
                else if (SceneManager.GetActiveScene().buildIndex == 9)
                    Fungus.Flowchart.BroadcastFungusMessage("NickDayFive");
                else if (SceneManager.GetActiveScene().buildIndex == 11)
                    Fungus.Flowchart.BroadcastFungusMessage("NickDaySix");
                else if (SceneManager.GetActiveScene().buildIndex == 13)
                    Fungus.Flowchart.BroadcastFungusMessage("NickDaySeven");

                gameObject.transform.GetComponent<Interactable>().enabled = false;
                mPlayer.transform.position = new Vector3(-8.7f, 10.5f, -26.0f);
                mPlayer.transform.rotation = new Quaternion(0.0f, -0.8f, 0.0f, 0.7f);
                //Debug.Log(mPlayer.transform.position);
                //Debug.Log(mPlayer.transform.rotation);
                break;
            case "Pan":
                if (SceneManager.GetActiveScene().buildIndex == 1)
                    Fungus.Flowchart.BroadcastFungusMessage("PanDayOne");
                else if (SceneManager.GetActiveScene().buildIndex == 3)
                    Fungus.Flowchart.BroadcastFungusMessage("PanDayTwo");
                else if (SceneManager.GetActiveScene().buildIndex == 5)
                    Fungus.Flowchart.BroadcastFungusMessage("PanDayThree");
                else if (SceneManager.GetActiveScene().buildIndex == 7)
                    Fungus.Flowchart.BroadcastFungusMessage("PanDayFour");
                else if (SceneManager.GetActiveScene().buildIndex == 9)
                    Fungus.Flowchart.BroadcastFungusMessage("PanDayFive");
                else if (SceneManager.GetActiveScene().buildIndex == 11)
                    Fungus.Flowchart.BroadcastFungusMessage("PanDaySix");
                else if (SceneManager.GetActiveScene().buildIndex == 13)
                    Fungus.Flowchart.BroadcastFungusMessage("PanDaySeven");

                gameObject.transform.GetComponent<Interactable>().enabled = false;
                mPlayer.transform.position = new Vector3(-31.6f, 0.4f, 27.7f);
                mPlayer.transform.rotation = new Quaternion(0.0f, -0.3f, 0.0f, 1.0f);
                //Debug.Log(mPlayer.transform.position);
                //Debug.Log(mPlayer.transform.rotation);
                break;
            case "Ashe":
                if (SceneManager.GetActiveScene().buildIndex == 1)
                    Fungus.Flowchart.BroadcastFungusMessage("AsheDayOne");
                else if (SceneManager.GetActiveScene().buildIndex == 3)
                    Fungus.Flowchart.BroadcastFungusMessage("AsheDayTwo");
                else if (SceneManager.GetActiveScene().buildIndex == 5)
                    Fungus.Flowchart.BroadcastFungusMessage("AsheDayThree");
                else if (SceneManager.GetActiveScene().buildIndex == 7)
                    Fungus.Flowchart.BroadcastFungusMessage("AsheDayFour");
                else if (SceneManager.GetActiveScene().buildIndex == 9)
                    Fungus.Flowchart.BroadcastFungusMessage("AsheDayFive");
                else if (SceneManager.GetActiveScene().buildIndex == 11)
                    Fungus.Flowchart.BroadcastFungusMessage("AsheDaySix");
                else if (SceneManager.GetActiveScene().buildIndex == 13)
                    Fungus.Flowchart.BroadcastFungusMessage("AsheDaySeven");

                gameObject.transform.GetComponent<Interactable>().enabled = false;
                mPlayer.transform.position = new Vector3(7.9f, 5.2f, 37.5f);
                mPlayer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                //Debug.Log(mPlayer.transform.position);
                //Debug.Log(mPlayer.transform.rotation);
                break;
            case "Uno":
                if (SceneManager.GetActiveScene().buildIndex == 2)
                    Fungus.Flowchart.BroadcastFungusMessage("UnoNightOne");
                else if (SceneManager.GetActiveScene().buildIndex == 4)
                    Fungus.Flowchart.BroadcastFungusMessage("UnoNightTwo");
                else if (SceneManager.GetActiveScene().buildIndex == 6)
                    Fungus.Flowchart.BroadcastFungusMessage("UnoNightThree");
                else if (SceneManager.GetActiveScene().buildIndex == 8)
                    Fungus.Flowchart.BroadcastFungusMessage("UnoNightFour");
                else if (SceneManager.GetActiveScene().buildIndex == 10)
                    Fungus.Flowchart.BroadcastFungusMessage("UnoNightFive");
                else if (SceneManager.GetActiveScene().buildIndex == 12)
                    Fungus.Flowchart.BroadcastFungusMessage("UnoNightSix");
                else if (SceneManager.GetActiveScene().buildIndex == 14)
                    Fungus.Flowchart.BroadcastFungusMessage("UnoNightSeven");

                //mPlayer.transform.position = new Vector3(7.9f, 5.2f, 37.5f);
                //mPlayer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                //Debug.Log(mPlayer.transform.position);
                //Debug.Log(mPlayer.transform.rotation);
                break;
            default:
                break;
        }
    }

    public void OnDefocused()
    {
        mIsFocus = false;
        mPlayer = null;
        hasInteracted = false;
    }

    public void OnFocused(GameObject playerTransform)
    {
        //
        //Debug.Log("in OnFocused");
        //
        mIsFocus = true;
        mPlayer = playerTransform;
        hasInteracted = false;
    }


    // for mouse hover
    void OnMouseEnter()
    {
        if (transform.tag == "Nick")
        {
            Transform character = gameObject.transform.Find("model:model:nick_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.1f);

        }
        else if (transform.tag == "Pan")
        {
            Transform character = gameObject.transform.Find("model:model:nick_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.1f);
        }
        else if (transform.tag == "Ashe")
        {
            Transform character = gameObject.transform.Find("model:model:nick_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.1f);
        }
        else if (transform.tag == "Uno")
        {
            Transform character = gameObject.transform.Find("model:uno_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.1f);
        }

    }

    void OnMouseExit()
    {
        if (transform.tag == "Nick")
        {
            Transform character = gameObject.transform.Find("model:model:nick_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);

        }
        else if (transform.tag == "Pan")
        {
            Transform character = gameObject.transform.Find("model:model:nick_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);
        }
        else if (transform.tag == "Ashe")
        {
            Transform character = gameObject.transform.Find("model:model:nick_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);
        }
        else if (transform.tag == "Uno")
        {
            Transform character = gameObject.transform.Find("model:uno_model");
            character.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);
        }

    }

    void SetJackPos()
    {

    }

}
