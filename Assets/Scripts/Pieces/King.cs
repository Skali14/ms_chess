using UnityEngine;

public class King : Piece
{
    public King(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        throw new System.NotImplementedException();
    }
}
