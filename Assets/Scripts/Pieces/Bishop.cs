using UnityEngine;

public class Bishop : Piece
{
    public Bishop(bool isWhite) : base(isWhite)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        throw new System.NotImplementedException();
    }
}
