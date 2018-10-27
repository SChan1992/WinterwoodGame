using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [SerializeField]
    private Color highlightColor = Color.white;
    [SerializeField]
    private Renderer ownRenderer = null;

    private Color[] originalColors;
    private void Start()
    {
        Transform door = gameObject.transform.Find("door_door");
        if (ownRenderer == null)
        {
            ownRenderer = door.GetComponent<Renderer>();
        }
        StoreOriginalColor();
    }


    private void StoreOriginalColor()
    {
        if (ownRenderer != null)
        {
            Material[] materials = ownRenderer.materials;
            originalColors = new Color[materials.Length];
            for (int i = 0; i < materials.Length; ++i)
            {
                originalColors[i] = materials[i].color;
            }
        }
    }
    private void OnMouseEnter()
    {
        //Transform door = gameObject.transform.Find("door_door");
        //door.gameObject.GetComponent<Renderer>().material.color = highlightColor;

        if (ownRenderer != null)
        {
            Material[] materials = ownRenderer.materials;
            for (int i = 0; i < materials.Length; ++i)
            {
                materials[i].color = highlightColor;
            }
            ownRenderer.materials = materials;
        }

    }
    private void OnMouseExit()
    {
        if (ownRenderer != null)
        {
            Material[] materials = ownRenderer.materials;
            for (int i = 0; i < materials.Length; ++i) { materials[i].color = originalColors[i]; }
            ownRenderer.materials = materials;
        }

        //Transform door = gameObject.transform.Find("door_door");
        //door.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}