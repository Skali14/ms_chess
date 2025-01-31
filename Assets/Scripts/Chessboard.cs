using UnityEngine;

public class Chessboard
{

    public Piece[,] Squares { get; private set; } // 8 * 8 matrix to store pieces

    public Chessboard() {
        Squares = new Piece[8, 8];
        intializeBoard();
    }

    private void intializeBoard()
    {
        Debug.Log("Square length: " + Squares.Length);
        // initialize Pawns
        for (int i = 0; i < Squares.Length - 1; i++)
        {
            Squares[1, i] = new Pawn(false);
            Squares[6, i] = new Pawn(true);
        }

        // initialize Rooks
        Squares[0, 0] = new Rook(false);
        Squares[0, 7] = new Rook(false);
        Squares[7, 0] = new Rook(true);
        Squares[7, 7] = new Rook(true);

        // initialize Knights
        Squares[0, 1] = new Knight(false);
        Squares[0, 6] = new Knight(false);
        Squares[7, 1] = new Knight(true);
        Squares[7, 6] = new Knight(true);

        // initialize Bishops
        Squares[0, 2] = new Bishop(false);
        Squares[0, 5] = new Bishop(false);
        Squares[7, 2] = new Bishop(true);
        Squares[7, 5] = new Bishop(true);

        // initialize Queen
        Squares[0, 3] = new Queen(false);
        Squares[7, 3] = new Queen(true);

        // initialize King
        Squares[0, 4] = new King(false);
        Squares[7, 4] = new King(true);
    }


    public Piece getSquare(int i, char c) {
        if (c >= 'A' && c <=  'H' && i >= 1 && i <= 8) { 
            return Squares[i - 1, c - 65];
        } else
        {
            throw new System.Exception("Out of bounds");
        }
    }
}
