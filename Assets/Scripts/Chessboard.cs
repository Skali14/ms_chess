using System;
using System.Linq;
using UnityEngine;

public class Chessboard
{

    public Piece[,] Squares { get; } // 8 * 8 matrix to store pieces

    public Chessboard() {
        Squares = new Piece[8, 8];
        intializeBoard();
    }

    private void intializeBoard()
    {
        // initialize Pawns
        for (int i = 0; i < 8; i++)
        {
            Squares[1, i] = new Pawn(false, "bp_" + (i+1));
            Squares[6, i] = new Pawn(true, "wp_" + (i+1));
        }

        // initialize Rooks
        Squares[0, 0] = new Rook(false, "br_1");
        Squares[0, 7] = new Rook(false, "br_2");
        Squares[7, 0] = new Rook(true, "wr_1");
        Squares[7, 7] = new Rook(true, "wr_2");

        // initialize Knights
        Squares[0, 1] = new Knight(false, "bn_1");
        Squares[0, 6] = new Knight(false, "bn_2");
        Squares[7, 1] = new Knight(true, "wn_1");
        Squares[7, 6] = new Knight(true, "wn_2");

        // initialize Bishops
        Squares[0, 2] = new Bishop(false, "bb_1");
        Squares[0, 5] = new Bishop(false, "bb_2");
        Squares[7, 2] = new Bishop(true, "wb_1");
        Squares[7, 5] = new Bishop(true, "wb_2");

        // initialize Queen
        Squares[0, 3] = new Queen(false, "bq_1");
        Squares[7, 3] = new Queen(true, "wq_1");

        // initialize King
        Squares[0, 4] = new King(false, "bk_1");
        Squares[7, 4] = new King(true, "wk_1");
    }


    public Piece getSquare(int i, char c) {
        if (c >= 'A' && c <=  'H' && i >= 1 && i <= 8) { 
            return Squares[Math.Abs(i - 1 - 7), c - 65];
        } else
        {
            throw new System.Exception("Out of bounds");
        }
    }

    public bool isValidPosition(int row, int col)
    {
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }

    public (int,  int) ConvertChessCoordinates(int startRow, char startCol)
    {
        return (Math.Abs(startRow - 1 - 7), startCol - 65);
    }
    
    public void PrintSquares()
    {
        for (var row = 0; row < Squares.GetLength(0); row++)
        {
            var str =  "";
            for (var col = 0; col < Squares.GetLength(1); col++)
            {
                
                var piece = Squares[row, col];
                if (piece == null)
                {
                    str += "[   ]";
                }
                else
                {
                    if (piece.IsWhite)
                    {
                        str += $"[{piece.GetType().Name[0]} W]";
                    }
                    else
                    {
                        str += $"[{piece.GetType().Name[0]} B]";
                    }
                }
            } 
            Debug.Log(str);
        }
    }
    
}
