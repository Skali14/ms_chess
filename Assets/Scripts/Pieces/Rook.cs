using UnityEngine;

public class Rook : Piece
{
    public Rook(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        throw new System.NotImplementedException();
    }
}
