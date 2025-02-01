using NUnit.Framework;
using UnityEngine;


public class NewTestScript
{
    [Test]
    public void TestBoardEval() {
        Debug.Log("test");
        Chessboard chessboard = new Chessboard();
        Piece piece = chessboard.getSquare(0, 'A');
        bool color = piece.IsWhite;
        Assert.IsTrue(color);
    }
}
