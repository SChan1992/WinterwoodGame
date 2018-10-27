using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera/Fade obstructors by raycast"),
 RequireComponent(typeof(Camera))]
public class FadeObstructors : FadeObstructorsBaseClass
{
    public bool useSpherecast = false;
    public float spherecastRadius = 0.5f;

    private RaycastHit[] hit;

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        // check player
        if (playerTransform == null)
        {
            return;
        }

        // max ray dist
        float maxDist = (playerTransform.position - myTransform.position).magnitude + offset;

        // check query trigger
        QueryTriggerInteraction Querytype = ignoreTriggers ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.Collide;

        if (!useSpherecast)
        {
            hit = Physics.RaycastAll(myTransform.position, myTransform.forward, maxDist, layersToFade, Querytype);
        }
        else
        {
            hit = Physics.SphereCastAll(myTransform.position, spherecastRadius, myTransform.forward, maxDist, layersToFade, Querytype);
        }
        Debug.DrawLine(myTransform.position, playerTransform.position + (myTransform.forward * offset), fadingColorToUse, Time.fixedDeltaTime);
        List<int> renderersIdsHitInThisFrame = new List<int>();
        if (hit != null)
        {
            // check all objects hit
            for (int i = 0; i < hit.Length; i++)
            {
                // ignore trigger
                if (hit[i].collider.isTrigger && ignoreTriggers)
                {
                    continue;
                }

                // Ignore the player :)
                if (!hit[i].collider.CompareTag(playerTag))
                {
                    // Retrieve all the renderers
                    Renderer[] rendererWeHit = hit[i].collider.gameObject.GetComponentsInChildren<Renderer>();

                    // Loop through the renderers
                    for (int j = 0; j < rendererWeHit.Length; j++)
                    {
                        if (rendererWeHit[j] != null) // just to be on the safe side :)
                        {
                            // Store the render's unique Id among those hit during the current frame
                            renderersIdsHitInThisFrame.Add(rendererWeHit[j].GetInstanceID());

                            // If we changed this already we skip it, otherwise we proceed with
                            // the change
                            if (!modifiedShaders.ContainsKey(rendererWeHit[j].GetInstanceID()))
                            {
                                ShaderData shaderData = new ShaderData();
                                FadingManager fade = rendererWeHit[j].gameObject.GetComponent<FadingManager>();
                                if (fade != null)
                                {
                                    fade.GoAway();
                                }

                                //Debug.Log("after go awaay");

                                shaderData.renderer = rendererWeHit[j];
                                shaderData.materials = rendererWeHit[j].materials;
                                Material[] tmpMats = rendererWeHit[j].materials;
                                shaderData.color = new Color[rendererWeHit[j].materials.Length];
                                for (int k = 0; k < tmpMats.Length; k++)
                                {
                                    shaderData.color[k] = tmpMats[k].color;
                                    tmpMats[k] = transparentMaterial;
                                    tmpMats[k].color = fadingColorToUse;
                                    if (replicateTexture)
                                    {
                                        tmpMats[k].mainTexture = rendererWeHit[j].materials[k].mainTexture;
                                    }
                                    else
                                    {
                                        tmpMats[k].mainTexture = null;
                                    }
                                }
                                rendererWeHit[j].materials = tmpMats;
                                // Add the shader to the list of those that have been changed
                                modifiedShaders.Add(rendererWeHit[j].GetInstanceID(), shaderData);
                                fade = rendererWeHit[j].gameObject.AddComponent<FadingManager>();
                                fade.fadingTime = fadingTime;
                                fade.fadingAmount = transparenceValue;
                            }
                        }
                    }
                }
            }
        }
        // Now let's restore those shaders that we changed but now they are no longer in the way
        List<int> renderersToRestore = new List<int>();
        foreach (KeyValuePair<int, ShaderData> elemento in modifiedShaders)
        {
            if (!renderersIdsHitInThisFrame.Contains(elemento.Key))
                renderersToRestore.Add(elemento.Key);
        }
        for (int i = 0; i < renderersToRestore.Count; i++)
        {
            ShaderData sd = modifiedShaders[renderersToRestore[i]];
            modifiedShaders.Remove(renderersToRestore[i]);
            for (int m = 0; m < sd.materials.Length; m++)
            {
                FadingManager fade = sd.renderer.gameObject.GetComponent<FadingManager>();
                if (fade != null)
                {
                    fade.GoAway();
                }
                fade = sd.renderer.gameObject.AddComponent<FadingManager>();
                fade.fadingTime = fadingTime;
                fade.fadingAmount = transparenceValue;
                fade.fadeOut = false;
                fade.matIdx = m;
                fade.oldMat = sd.materials[m];
                fade.oldColor = sd.color[m];
            }
        }
        Resources.UnloadUnusedAssets();
    }
}
