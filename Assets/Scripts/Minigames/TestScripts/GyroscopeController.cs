using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeController : MonoBehaviour
{
    public GameObject objectOfGyro; //The Object to be affected by Gyro.

    private bool gyroEnabled; //Us the Gyro usable?
    private Gyroscope gyro;
    private Quaternion rot;

    private void Start()
    {
        //objectOfGyro = new GameObject("Gyro Object");
        //objectOfGyro.transform.position = transform.position;
        //transform.SetParent(objectOfGyro.transform);


        gyroEnabled = EnableGyro(); //Sets the bool equal to the gyro
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope) //Does Gyro work on the system?
        {
            gyro = Input.gyro; //Use the Gyro input
            gyro.enabled = true; //Activate the Gyro function

            objectOfGyro.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            rot = new Quaternion(0, 0, 1, 0);

            return true; //If this worked, Gyro is enabled
        }
        return false; //If it did not work, it is false
    }

    private void Update()
    {
        GyroFunction();
    }

    private void GyroFunction()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
        else
        {
            //In here is the alternative controls for the game.
            //If pc, keystrokes?
            //Touch but no gyro, hmmm how about tap to shake the world or something?
            Debug.Log("Did not have Gyro");
            objectOfGyro.SetActive(false);//Makes sure you know that you do not have Gyro enabled.
        }
    }

}
