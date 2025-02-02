using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public class ChessMoveTest
{
    private Game _game;

    [SetUp]
    public void SetUp()
    {
        _game = new Game();
    }

    [Test]
    public void TestSimplePawnOpeningMove()
    {
        // Act: Move the pawn forward to position (2, 'A')
        var moveResult = _game.MakeMove(2, 'E', 4, 'E');
    
        // Assert: Verify that the pawn has moved successfully and is at the new position
        Assert.IsTrue(moveResult, "The pawn should move one square forward.");
        Assert.IsNull(_game.Board.getSquare(1, 'A'), "The original position should be empty.");
        Assert.IsInstanceOf<Pawn>(_game.Board.getSquare(2, 'A'), "The destination should contain the pawn.");
    }
    
    [Test]
    public void TestSimplePawnOpeningMove_InvalidMove()
    {
        // Act: Move the pawn forward to position (2, 'A')
        var moveResult = _game.MakeMove(2, 'E', 1, 'E');
    
        // Assert: Verify that the pawn has moved successfully and is at the new position
        Assert.IsFalse(moveResult, "The pawn should move one square forward.");
    }
    
    
}
