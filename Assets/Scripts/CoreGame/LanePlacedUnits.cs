using UnityEngine;

public class LanePlacedUnits : MonoBehaviour
{
    public static GameObject[] s_lane1 = new GameObject[8];
    public static GameObject[] s_lane2 = new GameObject[8];
    public static GameObject[] s_lane3 = new GameObject[8];
    public static GameObject[] s_lane4 = new GameObject[8];

    static public int s_HighestDamageTyping = 2; //the highest value a damage type can be given.
    static public int s_DamageDivisionModule = 4; //What the damage increase/Decrease is divided by.

    public static void PlaceNewUnitInList(GameObject theUnit, int laneNumber, int lanePlacement)
    {
        //Debug.Log("List was started and the values are = " + laneNumber + " lane and " + lanePlacement + " as position");

        if (laneNumber == 1)
        {
            s_lane1[lanePlacement] = theUnit;  //Places the unit given from TowerSpotScript in the appropriate slot on the list.

            //Debug.Log(LanePlacedUnits.s_lane1[lanePlacement].name + " is in Lane " + laneNumber + " Position " + lanePlacement);
        }
        if (laneNumber == 2)
        {
            s_lane2[lanePlacement] = theUnit;  //Places the unit given from TowerSpotScript in the appropriate slot on the list.

            //Debug.Log(LanePlacedUnits.s_lane2[lanePlacement].name + " is in Lane " + laneNumber + " Position " + lanePlacement);
        }
        if (laneNumber == 3)
        {
            s_lane3[lanePlacement] = theUnit;  //Places the unit given from TowerSpotScript in the appropriate slot on the list.

            //Debug.Log(LanePlacedUnits.s_lane3[lanePlacement].name + " is in Lane " + laneNumber + " Position " + lanePlacement);
        }
        if (laneNumber == 4)
        {
            s_lane4[lanePlacement] = theUnit;  //Places the unit given from TowerSpotScript in the appropriate slot on the list.

            //Debug.Log(LanePlacedUnits.s_lane4[lanePlacement].name + " is in Lane " + laneNumber + " Position " + lanePlacement);
        }
    }
    [ContextMenu("Press")]
    public void PressMe()
    {
        Debug.Log(s_lane1[1].name);
    }
}
