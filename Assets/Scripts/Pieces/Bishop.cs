using UnityEngine;
using System;

public class Bishop : Piece
{
    public Bishop(bool isWhite, string tag) : base(isWhite, tag)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares, Game game)
    {
        if (startRow == destRow && startCol == destCol) return false;

        // A bishop must move diagonally, so the row difference must be equal to the column difference
        if (Math.Abs(startRow - destRow) != Math.Abs(startCol - destCol))
        {
            return false;
        }

        // Check if destination piece is of the same color
        Piece destPiece = squares[destRow, destCol];
        if (destPiece != null && destPiece.IsWhite == IsWhite)
        {
            return false;
        }

        // Determine the direction of movement (top-right, top-left, bottom-right, bottom-left)
        int rowDirection = destRow > startRow ? 1 : -1;
        int colDirection = destCol > startCol ? 1 : -1;

        int row = startRow + rowDirection;
        int col = startCol + colDirection;

        while (row != destRow && col != destCol)
        {
            if (squares[row, col] != null) // There is a piece blocking the way
            {
                return false;
            }
            row += rowDirection;
            col += colDirection;
        }

        return true;
    }
}
