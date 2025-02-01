using System.ComponentModel;
using Codice.CM.Client.Differences;
using UnityEngine;

public class Game
{
    public Chessboard Board { get; private set; }
    public bool IsWhiteTurn { get; private set; }

    public bool StaleMate { get; private set; }

    public (int StartRow, int StartCol, int DestRow, int DestCol, Piece MovedPiece)? LastMove { get; private set; }

    public Game()
    {
        Board = new Chessboard();
        IsWhiteTurn = true;
    }

    public bool makeMove(int startRow, char startCol, int destRow, char destCol)
    {
        //TODO
        Piece piece = Board.getSquare(startRow, startCol);
        if (piece != null && piece.IsValidMove(startRow, startCol, destRow, destCol, Board.Squares, this))
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
        if (!isCheck(IsWhiteTurn))
        {
            return false;
        }

        //find kings position
        (int kingRow, int kingCol) = findKingPos(IsWhiteTurn);

        Piece king = Board.Squares[kingRow, kingCol];

        // Check if the king can escape
        int[] directions = { -1, 0, 1 };
        foreach (int dRow in directions)
        {
            foreach (int dCol in directions)
            {
                if (dRow == 0 && dCol == 0) continue; // Skip current position

                int newRow = kingRow + dRow;
                int newCol = kingCol + dCol;

                if (Board.isValidPosition(newRow, newCol)) // Check board boundaries
                {
                    Piece temp = Board.Squares[newRow, newCol];

                    if (temp == null || temp.IsWhite != IsWhiteTurn) // If empty or capturable
                    {
                        // Simulate the king's move
                        Board.Squares[kingRow, kingCol] = null;
                        Board.Squares[newRow, newCol] = king;

                        bool stillInCheck = isCheck(IsWhiteTurn);

                        // Undo move
                        Board.Squares[kingRow, kingCol] = king;
                        Board.Squares[newRow, newCol] = temp;

                        if (!stillInCheck)
                            return false; // If king can escape, it's not checkmate
                    }
                }
            }
        }

        //Check if any piece can block the check or capture the attacker
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece piece = Board.Squares[i, j];

                if (piece != null && piece.IsWhite == IsWhiteTurn) // Current player's piece
                {
                    for (int destRow = 0; destRow < 8; destRow++)
                    {
                        for (int destCol = 0; destCol < 8; destCol++)
                        {
                            if (piece.IsValidMove(i, j, destRow, destCol, Board.Squares, this))
                            {
                                // Simulate the move
                                Piece temp = Board.Squares[destRow, destCol];
                                Board.Squares[destRow, destCol] = piece;
                                Board.Squares[i, j] = null;

                                bool stillInCheck = isCheck(IsWhiteTurn);

                                // Undo move
                                Board.Squares[i, j] = piece;
                                Board.Squares[destRow, destCol] = temp;

                                if (!stillInCheck)
                                    return false; // If any move can save the king, not checkmate
                            }
                        }
                    }
                }
            }
        }

        // If no escape, it's checkmate
        return true;
    }

    private bool isCheck(bool isWhite)
    {
        (int kingRow, int kingCol) = findKingPos(isWhite);

        //check if any opponent piece can attack the king
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece piece = Board.Squares[i, j];
                if (piece != null && piece.IsWhite != isWhite && piece.IsValidMove(i, j, kingRow, kingCol, Board.Squares, this))
                {
                    return true; //King is in check
                }
            }
        }
        return false;
    }

    private (int row, int col) findKingPos(bool isWhite)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece piece = Board.Squares[i, j];
                if (piece is King && piece.IsWhite == isWhite)
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    private bool isStaleMate()
    {
        return false;
        //TODO
    }
}
