using UnityEngine;

public class Knight : Piece
{
    public Knight(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        throw new System.NotImplementedException();
    }
}
