using UnityEngine;

public class King : Piece
{
    public bool wasMoved = false; //needed for castling checks

    public King(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares, Game game)
    {
        throw new System.NotImplementedException();
    }
}
