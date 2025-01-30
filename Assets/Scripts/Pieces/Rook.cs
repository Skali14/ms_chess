using System;
using UnityEngine;

public class Rook : Piece
{
    public Rook(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        // Ensure move is either horizontal or vertical and startPos is different from destPos
        if (startRow != destRow && startCol != destCol || startRow == destRow && startCol == destCol)
        {
            return false;
        }

        Piece destPiece = squares[destRow, destCol];
        if (destPiece != null && destPiece.IsWhite == IsWhite)
        {
            return false;
        }

        // Determine direction and check for obstructions
        if (startRow == destRow) // Horizontal move
        {
            int minCol = Math.Min(startCol, destCol) + 1;
            int maxCol = Math.Max(startCol, destCol);
            for (int col = minCol; col < maxCol; col++)
            {
                if (squares[startRow, col] != null)
                {
                    return false;
                }
            }
        }
        else // Vertical move
        {
            int minRow = Math.Min(startRow, destRow) + 1;
            int maxRow = Math.Max(startRow, destRow);
            for (int row = minRow; row < maxRow; row++)
            {
                if (squares[row, startCol] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
