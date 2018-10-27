using System;
using System.Collections.Generic;
using UnityEngine;

public class ShaderData
{
    Renderer _renderer;
    Material[] _materials;
    Color[] _color;

    #region Public Properties
    public Renderer renderer
    {
        get
        {
            return _renderer;
        }
        set
        {
            _renderer = value;
        }
    }
    public Material[] materials
    {
        get
        {
            return _materials;
        }
        set
        {
            _materials = value;
        }
    }
    public Color[] color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
        }
    }
    #endregion 
}

[RequireComponent(typeof(Camera))]
public abstract class FadeObstructorsBaseClass : MonoBehaviour
{
    public Material transparentMaterial;
    public bool replicateTexture = false;
    public float fadingTime = 0.3f;
    public float transparenceValue = 0.3f;
    public bool ignoreTriggers = true;
    public Color fadingColorFullWhite = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color fadingColorToUse = new Color(1.0f, 1.0f, 1.0f, 0.3f);
    public LayerMask layersToFade = (LayerMask)(-1);
    public Transform playerTransform;
    public float offset = -0.5f;
    public string playerTag = "Player";

    protected Transform myTransform;
    protected Dictionary<int, ShaderData> modifiedShaders = new Dictionary<int, ShaderData>();

    // Use this for initialization
    public virtual void Start()
    {
        myTransform = transform;
        // Find the player if the target has not been assigned
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Transform>();
        if (playerTransform == null)
        {
            Debug.LogError("Player's transform not set and can't find any object in the scene with tag " + playerTag);
            return;
        }
    }
}
