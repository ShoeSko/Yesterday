using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheretoMouse : MonoBehaviour
{
    private Vector3 mousePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -2.25f;
            transform.position = mousePos;
        }
    }
}
