using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Tile : MonoBehaviour
{
    public static event Action wonGame;

    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private bool isWalkable;

    [SerializeField] private int stopsMovementLayerNum;

    public BaseUnit occupiedUnit; //the character in this space
    public bool Walkable => isWalkable && occupiedUnit == null;

    //for observer; start listening to invoke method
    private void OnEnable()
    {
        BaseHero.playerMove += ChangeTileColor;
    }

    //for observer; stop listening to invoke method
    private void OnDisable()
    {
        BaseHero.playerMove -= ChangeTileColor;
    }

    public virtual void Init(int x, int y)
    {
        
    }

    //when observer notices the player has moved, change the color of the new tile to red
    public void ChangeTileColor(Tile tile, BaseUnit baseUnit)
    {
        tile.GetComponent<SpriteRenderer>().color = Color.red;
        tile.gameObject.layer = stopsMovementLayerNum;
        baseUnit.OccupiedTile = this;
        
        //SetUnit(baseUnit);

        if (tile.occupiedUnit != null && tile.occupiedUnit.Faction == Faction.Enemy)
        {
            var enemy = (BaseEnemy)tile.occupiedUnit;
            Debug.Log(enemy);
            Destroy(enemy.gameObject);
            wonGame?.Invoke();
        }
        
        tile.occupiedUnit = baseUnit;
    }

    //highlights the tile when cursor is over it
    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        //MenuManager.Instance.ShowTileInfo(this);
    }

    //unhighlights the tile when cursor leaves it
    private void OnMouseExit()
    {
        highlight.SetActive(false);
        //MenuManager.Instance.ShowTileInfo(null);
    }

    /*
    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.HeroesTurn) return;

        if(occupiedUnit != null)
        {
            if (occupiedUnit.Faction == Faction.Hero) 
                UnitManager.Instance.SetSelectedHero((BaseHero)occupiedUnit);
            else
            {
                if(UnitManager.Instance.selectedHero != null)
                {
                    var enemy = (BaseEnemy)occupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if(UnitManager.Instance.selectedHero != null)
            {
                SetUnit(UnitManager.Instance.selectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
    }

    //set the position of the hero/enemy
    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.occupiedUnit = null;

        //unit.transform.position = transform.position;
        occupiedUnit = unit;
        unit.OccupiedTile = this;
    }
    */
}
