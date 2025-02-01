using NUnit.Framework;
using UnityEngine;


public class NewTestScript
{
    Chessboard chessboard = new Chessboard();

    [Test]
    public void testWhiteInitSpaces() {
        Debug.Log("testing first row initization");
        //first row 
        for(char c = 'A'; c <= 'H'; c++)
        {
            Piece piece = chessboard.getSquare(1, 'A');
            bool color = piece.IsWhite;
            Assert.IsTrue(color, $"the color of the (1, {c}) ");
        }

        Debug.Log("testing second row initization");
        //second row
        for (char c = 'A'; c <= 'H'; c++)
        {
            Piece piece = chessboard.getSquare(2, 'A');
            bool color = piece.IsWhite;
            Assert.IsTrue(color);
        }
    }

    [Test]
    public void testBlackInitSpaces()
    {
        Debug.Log("testing seventh row initization");
        //seventh row 
        for (char c = 'A'; c <= 'H'; c++)
        {
            Piece piece = chessboard.getSquare(7, 'A');
            bool color = piece.IsWhite;
            Assert.IsTrue(!color);
        }

        Debug.Log("testing eight row initization");
        //eigth row
        for (char c = 'A'; c <= 'H'; c++)
        {
            Piece piece = chessboard.getSquare(8, 'A');
            bool color = piece.IsWhite;
            Assert.IsTrue(!color);
        }
    }

    [Test]
    public void testInitKingWhite() 
    {
        Debug.Log("testing white king posistion");
        Piece king = chessboard.getSquare(1, 'D');
        Debug.Log(king.GetType());
        Assert.NotNull(king, "Expected a kind at (1, D) but found null");
        Assert.IsTrue(king is King, "Piece at (1, D) is not King");
        Assert.IsTrue(king.IsWhite, "Piece at (1, D) is not white");
    }

    [Test]
    public void testInitQueenWhite()
    {
        Debug.Log("testing white king posistion");
        Piece queen = chessboard.getSquare(1, 'E');
        Debug.Log(queen.GetType());
        Assert.NotNull(queen, "Expected a kind at (1, E) but found null");
        Assert.IsTrue(queen is King, "Piece at (1, E) is not King");
        Assert.IsTrue(queen.IsWhite, "Piece at (1, E) is not white");
    }
}
