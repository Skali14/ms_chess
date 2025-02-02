using System.Collections.Generic;
using System.ComponentModel;
using Codice.CM.Client.Differences;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Chessboard Board { get; private set; }

    public bool IsWhiteTurn;

    public bool StaleMate;
    public bool GameEnd;

    public (int StartRow, int StartCol, int DestRow, int DestCol, Piece MovedPiece)? LastMove { get; private set; }


    public static Game instance;
    public List<Piece> CapturedPieces; 

    public Game()
    {
        Board = new Chessboard();
        IsWhiteTurn = true;
        //dummy lastmove
        LastMove = (0, 0, 0, 0, new Rook(true));
        instance = this;
    }

    public bool MakeMove(int startRowRaw, char startColChar, int destRowRaw, char destColChar)
    {
        (int startRow, int startCol) = Board.ConvertChessCoordinates(startRowRaw, startColChar);
        (int destRow, int destCol) = Board.ConvertChessCoordinates(destRowRaw, destColChar);

        Piece piece = Board.Squares[startRow, startCol];
        if (piece != null && piece.IsWhite == IsWhiteTurn && piece.IsValidMove(startRow, startCol, destRow, destCol, Board.Squares, this))
        {
            if (piece is not King || !(piece as King).justCastled)
            {
                // Simulate the move to ensure it doesn’t leave the king in check
                if (SimulateMove(startRow, startCol, destRow, destCol))
                {
                    return false; // Move would result in check, so it's not allowed
                }

                //Store captured pieces
                Piece captured = Board.Squares[destRow, destCol];
                if (captured != null)
                {
                    CapturedPieces.Add(captured);
                }

                // Move the piece
                Board.Squares[destRow, destCol] = piece;
                Board.Squares[startRow, startCol] = null;

                if (piece is Rook rook)
                {
                    rook.wasMoved = true;
                }
                else if (piece is King king)
                {
                    king.wasMoved = true;
                }
            } else if (piece is King king && king.justCastled)
            {
                king.justCastled = false;
            }

            // If the piece is a pawn reaching the last rank, promote it (for simplicity, to a queen)
            if (piece is Pawn && (destRow == 0 || destRow == 7))
            {
                Board.Squares[destRow, destCol] = new Queen(piece.IsWhite);
            }

            LastMove = (startRow, startCol, destRow, destCol, piece);

            // Switch turns
            IsWhiteTurn = !IsWhiteTurn;

            // Check for checkmate or stalemate
            if (IsCheckMate())
            {
                Debug.Log(IsWhiteTurn ? "Black wins by checkmate!" : "White wins by checkmate!");
                GameEnd = true;
            }
            else if (IsStaleMate())
            {
                Debug.Log("Stalemate! The game is a draw.");
                StaleMate = true;
                GameEnd = true;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsCheckMate()
    {
        if (!IsCheck(IsWhiteTurn))
        {
            return false;
        }

        Debug.Log("Here before Kings options");

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
                        if (!SimulateMove(kingRow, kingCol, newRow, newCol))
                        {
                            return false;
                        } 
                    }
                }
            }
        }

        Debug.Log("Here after Kings options");

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
                                if (!SimulateMove(i, j, destRow, destCol))
                                {
                                    Debug.Log($"Valid move: i={i}, j={j}, destRow={destRow}, destCol={destCol}");
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("No escape");

        // If no escape, it's checkmate
        return true;
        
    }

    public bool IsCheck(bool isWhite)
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
        if (IsCheck(IsWhiteTurn))
        {
            return false; // If the king is in check, it's not stalemate.
        }

        // Iterate over all pieces of the current player
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece piece = Board.Squares[i, j];

                if (piece != null && piece.IsWhite == IsWhiteTurn) // Current player's piece
                {
                    // Try all possible moves for this piece
                    for (int destRow = 0; destRow < 8; destRow++)
                    {
                        for (int destCol = 0; destCol < 8; destCol++)
                        {
                            if (piece.IsValidMove(i, j, destRow, destCol, Board.Squares, this))
                            {
                                if (!SimulateMove(i, j, destRow, destCol))
                                {
                                    return false; // Found at least one legal move, not stalemate
                                }
                            }
                        }
                    }
                }
            }
        }

        // If no legal move exists and the king is not in check, it's stalemate
        return true;
    }


    //Simulates move and tests resulting board for check
    public bool SimulateMove(int startRow, int startCol, int destRow, int destCol)
    {
        Piece movingPiece = Board.Squares[startRow, startCol];
        Piece tempPiece = Board.Squares[destRow, destCol];

        Board.Squares[destRow, destCol] = movingPiece;
        Board.Squares[startRow, startCol] = null;

        bool stillInCheck = IsCheck(IsWhiteTurn);

        // Undo move
        Board.Squares[startRow, startCol] = movingPiece;
        Board.Squares[destRow, destCol] = tempPiece;

        return stillInCheck;
    }
}
