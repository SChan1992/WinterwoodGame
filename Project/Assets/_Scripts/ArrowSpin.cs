using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpin : MonoBehaviour
{

    float y;

    // Update is called once per frame
    void Update()
    {
        y += Time.deltaTime * 60;
        transform.rotation = Quaternion.Euler(0.0f, y, 0.0f);

    }
}
