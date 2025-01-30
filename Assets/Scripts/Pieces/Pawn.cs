using UnityEngine;

public class Pawn : Piece
{
    public Pawn(bool isWhite) : base(isWhite) {}

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares)
    {
        throw new System.NotImplementedException();
    }
}
