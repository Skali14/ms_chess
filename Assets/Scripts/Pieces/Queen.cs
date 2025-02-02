using UnityEngine;
using System;

public class Queen : Piece
{
    public Queen(bool isWhite, string tag) : base(isWhite, tag)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares, Game game)
    {
        if (startRow == destRow && startCol == destCol) return false;

        // Check if destination is occupied by a piece of the same color
        Piece destPiece = squares[destRow, destCol];
        if (destPiece != null && destPiece.IsWhite == IsWhite)
        {
            return false;
        }

        // Reuse Rook's movement logic (horizontal or vertical) and Bishop's movement logic (diagonal movement)
        return IsValidRookMove(startRow, startCol, destRow, destCol, squares)
            || IsValidBishopMove(startRow, startCol, destRow, destCol, squares);
    }

    private bool IsValidRookMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        if (startRow != destRow && startCol != destCol)
        {
            return false;
        }

        int rowDirection = startRow == destRow ? 0 : (destRow > startRow ? 1 : -1);
        int colDirection = startCol == destCol ? 0 : (destCol > startCol ? 1 : -1);

        int row = startRow + rowDirection;
        int col = startCol + colDirection;

        while (row != destRow || col != destCol)
        {
            if (squares[row, col] != null) return false; // Blocked path
            row += rowDirection;
            col += colDirection;
        }

        return true;
    }

    private bool IsValidBishopMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        if (Math.Abs(startRow - destRow) != Math.Abs(startCol - destCol))
        {
            return false;
        }

        int rowDirection = (destRow > startRow) ? 1 : -1;
        int colDirection = (destCol > startCol) ? 1 : -1;

        int row = startRow + rowDirection;
        int col = startCol + colDirection;

        while (row != destRow && col != destCol)
        {
            if (squares[row, col] != null) return false; // Blocked path
            row += rowDirection;
            col += colDirection;
        }

        return true;
    }
}
