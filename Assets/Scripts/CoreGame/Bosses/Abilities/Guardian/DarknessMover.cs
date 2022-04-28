using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessMover : MonoBehaviour
{
    public bool UsedEclipse;
    private Rigidbody2D rb;
    private float EclipseDuration;

    private Vector3 DarknessStartingPos;
    private Vector3 DarknessEndingPos;
    private float DarknessSpeed = 3.4f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DarknessStartingPos = transform.position;
        DarknessEndingPos = new Vector3(7.42f, DarknessStartingPos.y, DarknessStartingPos.z);
    }

    private void Update()
    {
        if (UsedEclipse == true)//The "Eclipse" ability
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, DarknessEndingPos, DarknessSpeed * Time.deltaTime);
            EclipseDuration += Time.deltaTime;

            if (EclipseDuration >= 15)
                UsedEclipse = false;
        }
        else
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, DarknessStartingPos, DarknessSpeed * Time.deltaTime);
            EclipseDuration = 0;
        }
    }
}
