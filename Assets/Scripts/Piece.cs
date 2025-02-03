using UnityEngine;

public abstract class Piece
{
    public bool IsWhite { get; private set; }

    public string Tag;

    public Piece(bool isWhite, string tag)
    {
        IsWhite = isWhite;
        Tag = tag;
    }

    public abstract bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares, Game game);
}
