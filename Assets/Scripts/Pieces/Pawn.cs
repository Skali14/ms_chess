using UnityEngine;
using System;

public class Pawn : Piece
{
    public Pawn(bool isWhite) : base(isWhite) { }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares, Game game)
    {
        if (startRow == destRow && startCol == destCol) return false;

        int direction = IsWhite ? 1 : -1; // White moves up (+1), Black moves down (-1)

        // Normal one-square move forward
        if (destRow - startRow == direction && destCol == startCol && squares[destRow, destCol] == null)
        {
            return true;
        }

        // Capture diagonally
        if (destRow - startRow == direction && Math.Abs(destCol - startCol) == 1)
        {
            Piece destPiece = squares[destRow, destCol];
            if (destPiece != null && destPiece.IsWhite != IsWhite)
            {
                return true; // Normal capture
            }
        }

        // Two-square move forward (first move only)
        if (destRow - startRow == direction * 2 && destCol == startCol)
        {
            int startRowCondition = IsWhite ? 1 : 6; // White starts at row 1, Black starts at row 6
            if (startRow == startRowCondition && squares[startRow + direction, startCol] == null && squares[destRow, destCol] == null)
            {
                return true;
            }
        }


        //en passant - move diagonal and capture other Pawn
        var (lastStartRow, lastStartCol, lastDestRow, lastDestCol, lastPiece) = game.LastMove.Value;

        if (lastPiece is Pawn && Math.Abs(lastStartRow - lastDestRow) == 2) // Moved two squares forward
        {
            if (lastDestRow == startRow && Math.Abs(lastDestCol - startCol) == 1 && destRow == lastDestRow + direction && destCol == lastDestCol)
            {
                return true; // En passant capture is valid
            }
        }

        return false;
    }
}
