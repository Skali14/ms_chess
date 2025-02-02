using UnityEngine;
using System;

public class King : Piece
{
    public bool wasMoved = false; //needed for castling checks

    public bool justCastled = false;

    public King(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares, Game game)
    {
        if (startRow == destRow && startCol == destCol) return false;

        int rowForCastling = IsWhite ? 7 : 0;

        if (!wasMoved && destCol == 0 && destRow == rowForCastling && !game.IsCheck(IsWhite)) //allow castling for clicking on rook - long side
        {
            if (squares[rowForCastling, 1] == null || squares[rowForCastling, 2] == null || squares[rowForCastling, 3] == null)
            {
                return false;
            }
            if (game.SimulateMove(startRow, startCol, destRow, 2) || game.SimulateMove(startRow, startCol, destRow, 3))
            {
                return false;
            }

            Piece rook = squares[destRow, destCol];

            if (rook is not Rook || (rook as Rook).wasMoved)
            {
                return false;
            }

            squares[destRow, 2] = this;
            squares[destRow, 3] = rook;
            if (game.IsCheck(IsWhite))
            {
                squares[destRow, 2] = null;
                squares[startRow, startCol] = this;
                squares[destRow, 3] = null;
                squares[destRow, destCol] = rook;
                return false;
            }

            justCastled = true;
            wasMoved = true;
            return true;
        }

        if (!wasMoved && destCol == 7 &&  destRow == rowForCastling && !game.IsCheck(IsWhite)) //allow castling for clicking on rook - short side
        {
            if (squares[rowForCastling, 5] == null || squares[rowForCastling, 6] == null)
            {
                return false;
            }
            if (game.SimulateMove(startRow, startCol, destRow, 5) || game.SimulateMove(startRow, startCol, destRow, 6))
            {
                return false;
            }

            Piece rook = squares[destRow, destCol];

            if (rook is not Rook || (rook as Rook).wasMoved)
            {
                return false;
            }

            squares[destRow, 6] = this;
            squares[destRow, 5] = rook;
            if (game.IsCheck(IsWhite))
            {
                squares[destRow, 6] = null;
                squares[startRow, startCol] = this;
                squares[destRow, 5] = null;
                squares[destRow, destCol] = rook;
                return false;
            }

            justCastled = true;
            wasMoved = true;
            return true;
        }

        if (Math.Abs(startRow - destRow) > 1 || Math.Abs(startCol - destCol) > 1)
        {
            return false;
        }

        return true;
    }
}
