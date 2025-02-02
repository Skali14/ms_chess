using NUnit.Framework;
using UnityEngine;


public class BoardInitTest
{
    private readonly Chessboard _chessboard = new Chessboard();

    [Test]
    public void TestWhiteInitSpaces()
    {
        Debug.Log("testing first row initiation");
        //first row 
        for (var c = 'A'; c <= 'H'; c++)
        {
            var piece = _chessboard.getSquare(1, 'A');
            var color = piece.IsWhite;
            Assert.IsTrue(color, $"the color of the (1, {c}) ");
        }

        Debug.Log("testing second row initiation");
        //second row
        for (var c = 'A'; c <= 'H'; c++)
        {
            var piece = _chessboard.getSquare(2, 'A');
            var color = piece.IsWhite;
            Assert.IsTrue(color);
        }
    }

    [Test]
    public void TestBlackInitSpaces()
    {
        Debug.Log("testing seventh row initiation");
        //seventh row 
        for (var c = 'A'; c <= 'H'; c++)
        {
            var piece = _chessboard.getSquare(7, 'A');
            var color = piece.IsWhite;
            Assert.IsTrue(!color);
        }

        Debug.Log("testing eight row initiation");
        //eigth row
        for (var c = 'A'; c <= 'H'; c++)
        {
            var piece = _chessboard.getSquare(8, 'A');
            var color = piece.IsWhite;
            Assert.IsTrue(!color);
        }
    }

    [Test]
    public void TestInitKingWhite()
    {
        Debug.Log("testing white king position");
        var king = _chessboard.getSquare(1, 'E');
        Debug.Log(king.GetType());
        Assert.NotNull(king, "Expected a kind at (1, D) but found null");
        Assert.IsTrue(king is King, "Piece at (1, D) is not King");
        Assert.IsTrue(king.IsWhite, "Piece at (1, D) is not white");
    }

    [Test]
    public void TestInitQueenWhite()
    {
        Debug.Log("testing white queen position");
        var queen = _chessboard.getSquare(1, 'D');
        Debug.Log(queen.GetType());
        Assert.NotNull(queen, "Expected a kind at (1, E) but found null");
        Assert.IsTrue(queen is Queen, "Piece at (1, E) is not queen");
        Assert.IsTrue(queen.IsWhite, "Piece at (1, E) is not white");
    }

    [Test]
    public void TestInitQueenBlack()
    {
        Debug.Log("testing black queen position");
        var queen = _chessboard.getSquare(8, 'D');
        Debug.Log(queen.GetType());
        Assert.NotNull(queen, "Expected a kind at (8, E) but found null");
        Assert.IsTrue(queen is Queen, "Piece at (8, E) is not queen");
        Assert.IsTrue(!queen.IsWhite, "Piece at (8, E) is not black");
    }

    [Test]
    public void TestInitKingBlack()
    {
        Debug.Log("testing black king position");
        var king = _chessboard.getSquare(8, 'E');
        Debug.Log(king.GetType());
        Assert.NotNull(king, "Expected a kind at (8, D) but found null");
        Assert.IsTrue(king is King, "Piece at (8, D) is not King");
    }

    [Test]
    public void TestInitRookWhite()
    {
        Debug.Log("testing white rook position");
        var rook = _chessboard.getSquare(1, 'A');
        Debug.Log(rook.GetType());
        Assert.NotNull(rook, "Expected a kind at (1, A) but found null");
        Assert.IsTrue(rook is Rook, "Piece at (1, A) is not rook");
    }

    [Test]
    public void TestInitRookBlack()
    {
        Debug.Log("testing black rook position");
        var rook = _chessboard.getSquare(8, 'A');
        Debug.Log(rook.GetType());
        Assert.NotNull(rook, "Expected a kind at (8, A) but found null");
        Assert.IsTrue(rook is Rook, "Piece at (8, A) is not rook");
    }

    [Test]
    public void TestInitBishopWhite()
    {
        Debug.Log("testing white bishop position");
        var bishop = _chessboard.getSquare(1, 'C');
        Debug.Log(bishop.GetType());
        Assert.NotNull(bishop, "Expected a kind at (1, B) but found null");
        Assert.IsTrue(bishop is Bishop, "Piece at (1, B) is not bishop");
    }

    [Test]
    public void TestInitBishopBlack()
    {
        Debug.Log("testing black bishop position");
        var bishop = _chessboard.getSquare(8, 'C');
        Debug.Log(bishop.GetType());
        Assert.NotNull(bishop, "Expected a kind at (8, B) but found null");
        Assert.IsTrue(bishop is Bishop, "Piece at (8, B) is not bishop");
    }

    [Test]
    public void TestInitKnightWhite()
    {
        Debug.Log("testing white knight position");
        var knight = _chessboard.getSquare(1, 'B');
        Debug.Log(knight.GetType());
        Assert.NotNull(knight, "Expected a kind at (1, B) but found null");
        Assert.IsTrue(knight is Knight, "Piece at (1, B) is not knight");
    }

    [Test]
    public void TestInitKnightBlack()
    {
        Debug.Log("testing black knight position");
        var knight = _chessboard.getSquare(8, 'B');
        Debug.Log(knight.GetType());
        Assert.NotNull(knight, "Expected a kind at (8, B) but found null");
        Assert.IsTrue(knight is Knight, "Piece at (8, B) is not knight");
    }

    [Test]
    public void TestInitPawnsWhite()
    {
        Debug.Log("testing white pawns position");
        for (var c = 'A'; c <= 'H'; c++)
        {
            var pawn = _chessboard.getSquare(2, c);
            //Debug.Log(pawn.GetType());
            Assert.NotNull(pawn, "Expected a kind at (2, {c}) but found null");
            Assert.IsTrue(pawn is Pawn, "Piece at (2, {c}) is not pawn");
        }
    }

    [Test]
    public void TestInitPawnsBlack()
    {
        Debug.Log("testing black pawns position");
        for (var c = 'A'; c <= 'H'; c++)
        {
            var pawn = _chessboard.getSquare(7, c);
            //Debug.Log(pawn.GetType());
            Assert.NotNull(pawn, "Expected a kind at (7, {c}) but found null");
            Assert.IsTrue(pawn is Pawn, "Piece at (7, {c}) is not pawn");
        }
    }
}
