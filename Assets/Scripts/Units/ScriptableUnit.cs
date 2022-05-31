using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    public Faction faction;         //allow for the ability to set the character's designation as either enemy or hero
    public BaseUnit UnitPrefab;     //grab the unit's prefab
}

public enum Faction
{   
    Hero = 0,
    Enemy = 1
}

