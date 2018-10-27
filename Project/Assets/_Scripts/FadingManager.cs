using UnityEngine;
using System.Collections;

public class FadingManager : MonoBehaviour
{
    #region public variables

    public float fadingTime;
    public float fadingAmount;
    public bool fadeOut = true;
    public Material oldMat;
    public int matIdx;
    public Color oldColor;

    private Renderer myRenderer;
    private Color tmpColor;
    private float delta;
    private bool done = false;
    private Material[] mats;

    #endregion

    // Use this for initialization
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        if (myRenderer == null)
        {
            Debug.Log("Can't get renderer");
            Destroy(this);
        }

        mats = myRenderer.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            tmpColor = mats[i].color;
            if (fadeOut)
            {
                tmpColor.a = 1.0f;
            }
            else
            {
                tmpColor.a = fadingAmount;
            }
            mats[i].color = tmpColor;
        }
        myRenderer.materials = mats;
        delta = (1 - fadingAmount) / fadingTime;
    }

    public void GoAway()
    {
        Debug.Log(mats.Length);
        for (int i = 0; i < mats.Length; i++)
        {
            tmpColor = mats[i].color;
            if (fadeOut)
            {
                tmpColor.a = fadingAmount;
                mats[i].color = tmpColor;
            }
            else
            {
                if (i == matIdx)
                {
                    mats[i] = oldMat;
                    mats[i].color = oldColor;
                }
            }
        }
        myRenderer.materials = mats;
        done = true;
        Resources.UnloadUnusedAssets();
        Destroy(this);
    }

    private void Update()
    {
        if (done)
        {
            Resources.UnloadUnusedAssets();
            Destroy(this);
        }

        mats = myRenderer.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            tmpColor = mats[i].color;
            if (fadeOut)
            {
                FadeOut(i);
            }
            else
            {
                FadeIn(i);
            }
        }
        myRenderer.materials = mats;
    }

    private void FadeOut(int i)
    {
        if (tmpColor.a < fadingAmount)
        {
            tmpColor.a = fadingAmount;
            mats[i].color = tmpColor;
            done = true;
            return;
        }
        tmpColor.a -= delta * Time.deltaTime;
        mats[i].color = tmpColor;
    }
    private void FadeIn(int i)
    {
        if (i != matIdx) return;
        if (tmpColor.a >= 1f)
        {
            mats[i] = oldMat;
            mats[i].color = oldColor;
            done = true;
            return;
        }
        tmpColor.a += delta * Time.deltaTime;
        mats[i].color = tmpColor;
    }
}
