using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverHighlight : MonoBehaviour
{
    public GUISkin gameSkin;
    public string objectName;

    #region Private Properties

    private Color mStartColor;
    private bool mDisplayObjectName = false;

    #endregion

    #region Unity GUI and Mouse
    void OnGUI()
    {
        GUI.skin = gameSkin;

        DisplayName();
    }

    void OnMouseEnter()
    {
        mStartColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.yellow;
        mDisplayObjectName = true;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = mStartColor;
        mDisplayObjectName = false;
    }
    #endregion

    #region Public Methods

    public void DisplayName()
    {
        if (mDisplayObjectName == true)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x - 155,
                             Event.current.mousePosition.y, 150,
                             25),
                             objectName,
                             "Box2 Style");
        }
    }

    #endregion
}
