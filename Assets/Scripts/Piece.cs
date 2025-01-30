using UnityEngine;

public abstract class Piece
{
    public bool IsWhite { get; private set; }

    public Piece(bool isWhite)
    {
        IsWhite = isWhite;
    }

    public abstract bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares);
}
