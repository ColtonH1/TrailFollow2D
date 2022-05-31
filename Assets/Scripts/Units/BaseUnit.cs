using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public string UnitName; //name of unit...
    public Tile OccupiedTile; //what tile the unit is on
    public Faction Faction; //whether the unit is friend or foe (hero or enemy)
}
