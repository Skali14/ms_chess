using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class movePieces : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    public float squareSize = 1.099949f;
    public Vector3 chessboardOrigin = new Vector3(-4.888336f, 1.805004f, 0f);
    private Vector3 initialPos;
    private Vector3 endPos;
    private Game game;

    private void Start()
    {
        game = Game.instance;
    }

    private void Update()
    {
        //TODO
        if(dragging && !game.GameEnd)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    public void OnMouseDown()
    {
        //TODO
        if(!game.GameEnd)
        {
            initialPos = transform.position;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
        
    }

    public void OnMouseUp()
    {
        //TODO
        if(!game.GameEnd)
        {
            dragging = false;
            endPos = transform.position;
            (int, char) initialField = CalculateField(initialPos);
            (int, char) endField = CalculateField(endPos);
            Debug.Log("Initial" + initialField);
            Debug.Log("End" + endField);
            if (game.MakeMove(initialField.Item1, initialField.Item2, endField.Item1, endField.Item2)) 
            {
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
}
