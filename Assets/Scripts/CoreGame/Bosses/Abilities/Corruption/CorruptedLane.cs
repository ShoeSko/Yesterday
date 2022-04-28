using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedLane : MonoBehaviour
{
    private float timer;
    private bool IsCorrupting;
    [HideInInspector] public GameObject ActiveLane;

    private void Update()
    {
        if (IsCorrupting)
        {
            timer += Time.deltaTime;
        }

        if(timer >= 16)
        {
            ActiveLane.SetActive(true);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 24);
            IsCorrupting = false;
            timer = 0;
        }
    }

    public void Move()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        IsCorrupting = true;
    }
}
