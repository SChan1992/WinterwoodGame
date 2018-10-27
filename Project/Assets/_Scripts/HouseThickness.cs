using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseThickness : MonoBehaviour
{
    #region Public Properties

    [Range(0.0f, 100.0f)]
    public float radius = 3.0f;

    public Transform interactionTransform;
    //public GameManager gameManager;

    #endregion

    #region Private Properties

    GameObject mPlayer;

    #endregion

    #region Unity CallBacks

    void Update()
    {
        mPlayer = GameObject.FindWithTag("Player");
        if (mPlayer != null && interactionTransform != null)
        {
            float distance = Vector3.Distance(mPlayer.transform.position, interactionTransform.position);
            if (distance <= radius)
            {
                GameManager.Instance.CanMove = false;
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

}
