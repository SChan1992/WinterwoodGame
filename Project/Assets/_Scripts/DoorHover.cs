using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHover : MonoBehaviour
{

    public Behaviour halo;

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        halo.enabled = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        halo.enabled = false;
    }
}
