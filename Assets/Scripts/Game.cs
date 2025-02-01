using UnityEngine;

public class Game
{
    public Chessboard board { get; private set; }
    public bool IsWhiteTurn { get; private set; }

    public (int StartRow, int StartCol, int DestRow, int DestCol, Piece MovedPiece)? LastMove { get; private set; }

    public Game()
    {
        board = new Chessboard();
        IsWhiteTurn = true;
    }

    public bool makeMove(int startRow, char startCol, int destRow, char destCol)
    {
        Piece piece = board.getSquare(startRow, startCol);
        if (piece != null && piece.IsValidMove(startRow, startCol, destRow, destCol, board.Squares, this))
        {
            LastMove = (startRow, startCol, destRow, destCol, piece);
            return false;
        }
        else
        {
            return false;
        }
    }

    private bool isCheckMate()
    {
        return false;
    }

    private bool isStaleMate()
    {
        return false;
    }
}
