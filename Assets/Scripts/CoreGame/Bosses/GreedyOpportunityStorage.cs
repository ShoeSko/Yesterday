using UnityEngine;
[CreateAssetMenu(fileName = "New Hand ", menuName = "HandTemplate")]
public class GreedyOpportunityStorage : ScriptableObject
{
    [Header("Hand Controls")]
    public float moveSpeed;
    public int handHealth;
    public string obstacleTags;
    public string projectileTags;

    [Header("Hand Attack")]
    [Tooltip("How long before greed strikes and the hand grabs the unit infront.")] public float timeBeforeGreed;

    [Header("Other")]
    public int quackDamage = 80; //Is it as usefull on him
}
