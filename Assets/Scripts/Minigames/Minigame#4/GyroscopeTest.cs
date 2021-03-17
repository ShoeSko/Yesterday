using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeTest : MonoBehaviour
{
    Gyroscope m_Gyro;

    void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }

    //This is a legacy function, check out the UI section for other ways to create your UI
    void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + m_Gyro.rotationRate); //Rotation, tip a bucket?
        GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + m_Gyro.attitude); //attitude, uncertain on use
        GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + m_Gyro.enabled); //Is there Gyro, good use it.
    }
}