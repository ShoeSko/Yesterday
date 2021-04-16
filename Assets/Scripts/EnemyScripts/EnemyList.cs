using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New Enemy List", menuName ="Enemy List")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> Enemies = new List<GameObject>();
}
