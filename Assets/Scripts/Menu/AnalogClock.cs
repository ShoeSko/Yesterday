using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform clockHandHourTransform;
    [SerializeField] private Transform clockHandMinuteTransform;
    [SerializeField] private Transform clockHandSecondsTransform;
    #endregion
    private void Update()
    {
        HourHandMovement();
        MinuteHandMovement();
        SecondsHandMovement();
    }
    #region HourHand
    private void HourHandMovement()
    {
        int hourRotationalValue = 360 / 12; //Divides max value of an hour into one that goes 360 (Ignoring a 24H format)
        int currentHour = System.DateTime.Now.Hour; //Gets the Hours from the local system

        if(currentHour <= 12)
        {
            clockHandHourTransform.eulerAngles = new Vector3(0, 0, -(currentHour * hourRotationalValue)); //Rotates HourHand Z axis (negative for clockwise rotation) by the current time.
        }
        else
        {
            clockHandHourTransform.eulerAngles = new Vector3(0, 0, -((currentHour-12) * hourRotationalValue)); //Keeps the timeframe within 12 hours by removing 12 from the equation when passing 12 (13-23)(00 is a mystery).
        }
    }
    #endregion
    #region MinuteHand
    private void MinuteHandMovement()
    {
        int minuteRotationalValue = 360 / 60; //Divides max value of a minute into one that goes 360
        int currentMinute = System.DateTime.Now.Minute; //Gets the Minutes from the local system

        clockHandMinuteTransform.eulerAngles = new Vector3(0, 0, -(currentMinute*minuteRotationalValue)); //Rotates MinuteHand Z axis (negative for clockwise rotation) by the current time.
    }
    #endregion
    #region SecondsHand
    private void SecondsHandMovement()
    {
        int secondsRotationalValue = 360 / 60; //Divides max value of a second into one that goes 360
        int currentSecond = System.DateTime.Now.Second; //Gets the Seconds from the local system

        clockHandSecondsTransform.eulerAngles = new Vector3(0, 0, -(currentSecond * secondsRotationalValue)); //Rotates SecondsHand Z axis (negative for clockwise rotation) by the current time.
    }
    #endregion
}
