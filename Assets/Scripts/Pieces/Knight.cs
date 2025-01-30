using UnityEngine;
using System;

public class Knight : Piece
{
    public Knight(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        if (startRow == destRow && startCol == destCol) return false;

        Piece destPiece = squares[destRow, destCol];
        if (destPiece != null && destPiece.IsWhite == IsWhite)
        {
            return false;
        }

        //Check if Knight moves in an L shape
        if (Math.Abs(startRow - destRow) == 1 && Math.Abs(startCol - destCol) == 2 || Math.Abs(startRow - destRow) == 2  && Math.Abs(startCol - destCol) == 1)
        {
            return true;
        }
        return false;
    }
}
