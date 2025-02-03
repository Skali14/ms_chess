using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class movePieces : MonoBehaviour
{
    public Sprite queenSprite;
    [HideInInspector]
    private bool dragging = false;
    private Vector3 offset;
    public float squareSize = 1.099949f;
    public Vector3 chessboardOrigin = new Vector3(-4.888336f, 1.805004f, 0f);
    private Vector3 initialPos;
    private Vector3 endPos;
    private Game game;
    private bool stillInGame = true;
    private int curSortingOrder;
    private bool justCastled;

    private void Start()
    {
        game = Game.instance;
        SnapToNearestCenter(transform.position);
        
        
    }

    private void Update()
    {
        //TODO
        if(!game.GameEnd)
        {
            if(dragging && stillInGame) 
            {
                if (game.IsWhiteTurn && tag.StartsWith("w") || !game.IsWhiteTurn && tag.StartsWith("b"))
                {
                    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                }
            }

            
        }
    }

    public void OnMouseDown()
    {
        //TODO
        if(stillInGame && !game.GameEnd && (game.IsWhiteTurn && tag.StartsWith("w") || !game.IsWhiteTurn && tag.StartsWith("b")))
        {
            curSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            GetComponent<SpriteRenderer>().sortingOrder = 100;
            initialPos = transform.position;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
        
    }

    public void OnMouseUp()
    {
        //TODO
        if(stillInGame && !game.GameEnd && (game.IsWhiteTurn && tag.StartsWith("w") || !game.IsWhiteTurn && tag.StartsWith("b")))
        {
            GetComponent<SpriteRenderer>().sortingOrder = curSortingOrder;
            dragging = false;
            endPos = transform.position;
            (int, char) initialField = CalculateField(initialPos);
            (int, char) endField = CalculateField(endPos);
            if(endField.Item2 == 'X' || endField.Item1 > 8 || endField.Item1 < 1)
            {
                SnapToNearestCenter(initialPos);
            } else if (game.MakeMove(initialField.Item1, initialField.Item2, endField.Item1, endField.Item2)) 
            {
                if(justCastled)
                {
                    justCastled = false;
                    return;
                }
                SnapToNearestCenter(endPos);
            } else
            {
                SnapToNearestCenter(initialPos);
            }
        }
        
    }

    private void SnapToNearestCenter(Vector3 position)
    {
        // Calculate the nearest center position
        Vector3 nearestCenter = CalculateNearestCenter(position);

        // Move the piece to the nearest center
        transform.position = nearestCenter;
    }

    private Vector3 CalculateNearestCenter(Vector3 position)
    {
        // Calculate the grid indices (floored to the nearest integer)
        int gridX = Mathf.FloorToInt((position.x - chessboardOrigin.x) / squareSize);
        int gridY = Mathf.FloorToInt((position.y - chessboardOrigin.y) / squareSize);

        // Calculate the center of the nearest square
        Vector3 nearestCenter = new Vector3(
            chessboardOrigin.x + (gridX * squareSize) + (squareSize / 2),
            chessboardOrigin.y + (gridY * squareSize) + (squareSize / 2),
            position.z // Keep the original z position
        );

        // Check if the piece is closer to the next square in X or Y direction
        float deltaX = position.x - nearestCenter.x;
        float deltaY = position.y - nearestCenter.y;

        // Adjust the nearest center if the piece is closer to the next square
        if (Mathf.Abs(deltaX) > squareSize / 2)
        {
            nearestCenter.x += Mathf.Sign(deltaX) * squareSize;
        }
        if (Mathf.Abs(deltaY) > squareSize / 2)
        {
            nearestCenter.y += Mathf.Sign(deltaY) * squareSize;
        }

        return nearestCenter;
    }

    private (int, char) CalculateField(Vector3 position)
    {
        int gridX = Mathf.FloorToInt((position.x - chessboardOrigin.x) / squareSize) + 1;
        int gridY = Mathf.FloorToInt((position.y - chessboardOrigin.y) / squareSize) + 1;

        return (gridY, ConvertIntToChar(gridX));
    }

    private char ConvertIntToChar(int value)
    {
        switch (value)
        {
            case 1: return 'A';
            case 2: return 'B';
            case 3: return 'C';
            case 4: return 'D';
            case 5: return 'E';
            case 6: return 'F';
            case 7: return 'G';
            case 8: return 'H';
            default: return 'X';
        }
    }

    private int ConvertCharToInt(char value) 
    {
        switch (value)
        {
            case 'A': return 1;
            case 'B': return 2;
            case 'C': return 3;
            case 'D': return 4;
            case 'E': return 5;
            case 'F': return 6;
            case 'G': return 7;
            case 'H': return 8;
            default: return 100;
        }
    }

    public void MoveToCapturedArea()
    {
        stillInGame = false;
        switch(tag)
        {
            case "wp_1": transform.position = new Vector3(-6.0326572f, 4.38f, 0f); break;
            case "wp_2": transform.position = new Vector3(-5.8116572f, 4.38f, 0f); break;
            case "wp_3": transform.position = new Vector3(-5.5906572f, 4.38f, 0f); break;
            case "wp_4": transform.position = new Vector3(-5.3696572f, 4.38f, 0f); break;

            case "wp_5": transform.position = new Vector3(-6.0326572f, 3.536999f, 0f); break;
            case "wp_6": transform.position = new Vector3(-5.8116572f, 3.536999f, 0f); break;
            case "wp_7": transform.position = new Vector3(-5.5906572f, 3.536999f, 0f); break;
            case "wp_8": transform.position = new Vector3(-5.3696572f, 3.536999f, 0f); break;

            case "wr_1": transform.position = new Vector3(-6.1026572f, 2.573f, 0f); break;
            case "wr_2": transform.position = new Vector3(-5.8816572f, 2.573f, 0f); break;
            case "wn_1": transform.position = new Vector3(-5.5606572f, 2.609f, 0f); break;
            case "wn_2": transform.position = new Vector3(-5.3396572f, 2.609f, 0f); break;

            case "wb_1": transform.position = new Vector3(-6.1226572f, 1.587999f, 0f); break;
            case "wb_2": transform.position = new Vector3(-5.9016572f, 1.587999f, 0f); break;
            case "wq_1": transform.position = new Vector3(-5.5806572f, 1.603f, 0f); break;
            case "wk_2": transform.position = new Vector3(-5.3596572f, 1.603f, 0f); break;

            case "bp_1": transform.position = new Vector3(-6.032657f, -0.9100003f, 0f); break;
            case "bp_2": transform.position = new Vector3(-5.811657f, -0.9100003f, 0f); break;
            case "bp_3": transform.position = new Vector3(-5.590657f, -0.9100003f, 0f); break;
            case "bp_4": transform.position = new Vector3(-5.369657f, -0.9100003f, 0f); break;

            case "bp_5": transform.position = new Vector3(-6.032657f, -1.753001f, 0f); break;
            case "bp_6": transform.position = new Vector3(-5.811657f, -1.753001f, 0f); break;
            case "bp_7": transform.position = new Vector3(-5.590657f, -1.753001f, 0f); break;
            case "bp_8": transform.position = new Vector3(-5.369657f, -1.753001f, 0f); break;

            case "br_1": transform.position = new Vector3(-6.102657f, -2.717f, 0f); break;
            case "br_2": transform.position = new Vector3(-5.881658f, -2.717f, 0f); break;
            case "bn_1": transform.position = new Vector3(-5.560658f, -2.681f, 0f); break;
            case "bn_2": transform.position = new Vector3(-5.339657f, -2.681f, 0f); break;

            case "bb_1": transform.position = new Vector3(-6.122657f, -3.702001f, 0f); break;
            case "bb_2": transform.position = new Vector3(-5.901657f, -3.702001f, 0f); break;
            case "bq_1": transform.position = new Vector3(-5.580657f, -3.702001f, 0f); break;
            case "bk_2": transform.position = new Vector3(-5.359657f, -3.698f, 0f); break;
        }
    }

    public void PromoteToQueen()
    {
        GetComponent<SpriteRenderer>().sprite = queenSprite;
    }

    public void Castle(string type)
    {
        switch(type)
        {
            case "queenside_b":
                {
                    switch (tag)
                    {
                        case "br_1": SnapToNearestCenter(new Vector3(-0.6f, 4.13f, 0)); break;
                        case "bk_1": SnapToNearestCenter(new Vector3(-1.7f, 4.11f, 0)); justCastled = true; break;
                    }
                }
                break;
            case "queenside_w":
                {
                    switch (tag)
                    {
                        case "wr_1": SnapToNearestCenter(new Vector3(-0.58f, -3.54f, 0)); break;
                        case "wk_1": SnapToNearestCenter(new Vector3(-1.65f, -3.56f, 0)); justCastled = true; break;
                    }
                }
                break;
            case "kingside_b":
                {
                    
                    switch (tag)
                    {
                        case "br_2": SnapToNearestCenter(new Vector3(1.6f, 4.11f, 0)); break;
                        case "bk_1": SnapToNearestCenter(new Vector3(2.7f, 4.13f, 0)); justCastled = true; break;
                    }
                }
                break;
            case "kingside_w":
                {
                    switch (tag)
                    {
                        case "wr_2": SnapToNearestCenter(new Vector3(1.61f, -3.52f, 0)); break;
                        case "wk_1": SnapToNearestCenter(new Vector3(2.67f, -3.55f, 0)); justCastled = true; break;
                    }
                }
                break;
        }
        
    }
}
