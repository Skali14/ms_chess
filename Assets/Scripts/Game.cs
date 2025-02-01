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

    public bool makeMove(int startRowRaw, char startColChar, int destRow, char destCol)
    {
        //TODO
        (int startRow, int startCol) = Board.ConvertChessCoordinates(startRowRaw, startColChar);
        Piece piece = Board.Squares[startRow, startCol];
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
        (int kingRow, int kingCol) = FindKingPos(IsWhiteTurn);

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
                        if (!simulateMove(kingRow, kingCol, newRow, newCol))
                        {
                            return false;
                        } 
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
                                if (!simulateMove(i, j, destRow, destCol))
                                {
                                    return false;
                                }
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
        (int kingRow, int kingCol) = FindKingPos(isWhite);

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

    private (int row, int col) FindKingPos(bool isWhite)
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

    private bool IsStaleMate()
    {
        return false;
        //TODO
    }


    //Simulates move and tests resulting board for check
    private bool simulateMove(int startRow, int startCol, int destRow, int destCol)
    {
        Piece movingPiece = Board.Squares[startRow, startCol];
        Piece tempPiece = Board.Squares[destRow, destCol];

        Board.Squares[destRow, destCol] = movingPiece;
        Board.Squares[startRow, startCol] = null;

        bool stillInCheck = isCheck(IsWhiteTurn);

        // Undo move
        Board.Squares[startRow, startCol] = movingPiece;
        Board.Squares[destRow, destCol] = tempPiece;

        return stillInCheck;
    }
}
