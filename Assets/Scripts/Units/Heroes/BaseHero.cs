using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseHero : BaseUnit
{
    public static event Action<Tile, BaseUnit> playerMove;
    public static event Action gameOverResults;

    public float moveSpeed = 5f;
    public Transform movePoint;
    public bool gameOver;

    public LayerMask whatStopsMovement;
    [SerializeField] int stopsMovementLayerNum;

    private void Start()
    {
        movePoint.parent = null;
        gameOver = false;
    }

    private void Update()
    {
        //move player to desired location
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        //If distance between current location and desired location is within .05f, continue 
        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            //if player hits a key to move horizontally...
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                //check if barrier is in the way
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    Tile tile = GridManager.Instance.GetTileAtPosition(new Vector2(movePoint.position.x, movePoint.position.y)); 
                    playerMove?.Invoke(tile, this); //tell any listeners that the player moved
                    gameOver = CanMove(tile);
                    if (gameOver)
                    {
                        gameOverResults?.Invoke(); //tell any listeners that the player lost
                    }
                }

            }
            //use else statement to prevent diagonal moves
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                //check if barrier is in the way
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    Tile tile = GridManager.Instance.GetTileAtPosition(new Vector2(movePoint.position.x, movePoint.position.y));
                    playerMove?.Invoke(tile, this); //tell any listeners that the player moved
                    gameOver = CanMove(tile);
                    if(gameOver)
                    {
                        gameOverResults?.Invoke(); //tell any listeners that the player lost
                    }
                }
                    
            }
        }
    }

    public bool CanMove(Tile tile)
    {
        float x = tile.transform.position.x;
        float y = tile.transform.position.y;
        bool cantMoveAgain = true;
        for(int i = 0; i < 4; i++)
        {
            if(i == 0)
            {
                Tile adjacentTile = GridManager.Instance.GetTileAtPosition(new Vector2(x + 1, y));
                if(adjacentTile.gameObject.layer != stopsMovementLayerNum)
                {
                    cantMoveAgain = false;
                }
            }
            if (i == 1)
            {
                Tile adjacentTile = GridManager.Instance.GetTileAtPosition(new Vector2(x, y + 1));
                if(adjacentTile.gameObject.layer != stopsMovementLayerNum)
                {
                    cantMoveAgain = false;
                }
            }
            if (i == 2)
            {
                Tile adjacentTile = GridManager.Instance.GetTileAtPosition(new Vector2(x - 1, y));
                if (adjacentTile.gameObject.layer != stopsMovementLayerNum)
                {
                    cantMoveAgain = false;
                }
            }
            if (i == 3)
            {
                Tile adjacentTile = GridManager.Instance.GetTileAtPosition(new Vector2(x, y - 1));
                if (adjacentTile.gameObject.layer != stopsMovementLayerNum)
                {
                    cantMoveAgain = false;
                }
            }

        }
        return cantMoveAgain;
        
    }
}
