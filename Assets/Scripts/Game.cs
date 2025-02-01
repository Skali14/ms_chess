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

    private bool isCheck(bool isWhite)
    {
        bool foundKing = false;
        int kingRow = -1;
        int kingCol = -1;

        for (int i = 0; i < 8 && !foundKing; i++)  // Stop outer loop when found
        {
            for (int j = 0; j < 8; j++)
            {
                Piece piece = board.Squares[i, j];
                if (piece is King && piece.IsWhite == isWhite)
                {
                    kingRow = i;
                    kingCol = j;
                    foundKing = true;
                    break;
                }
            }
        }

        //check if any opponent piece can attack the king
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece piece = board.Squares[i, j];
                if (piece != null && piece.IsWhite != isWhite && piece.IsValidMove(i, j, kingRow, kingCol, board.Squares, this))
                {
                    return true; //King is in check
                }
            }
        }
        return false;
    }

    private bool isStaleMate()
    {
        return false;
    }
}
