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
        Assert.IsNull(_game.Board.getSquare(2, 'E'), "The original position should be empty.");
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

    [Test]
    public void TestSimpleKnight1OpeningMove()
    {
        // Act: Move the knight from (1, 'B') to (3, 'C')
        var moveResult = _game.MakeMove(1, 'B', 3, 'C');
    
        // Assert: Verify that the knight has moved successfully and is at the new position
        Assert.IsTrue(moveResult, "The knight should move from (1, 'B') to (3, 'C').");
        Assert.IsNull(_game.Board.getSquare(1, 'B'), "The original position should be empty.");
        Assert.IsInstanceOf<Knight>(_game.Board.getSquare(3, 'C'), "The destination should contain the knight.");
    }

    [Test]
    public void TestSimpleKnight1OpeningMove_InvalidMove()
    {
        // Act: Move the knight from (1, 'B') to (3, 'C')
        var moveResult = _game.MakeMove(1, 'B', 3, 'D');
    
        // Assert: Verify that the knight has moved successfully and is at the new position
        Assert.IsFalse(moveResult, "The knight should move from (1, 'B') to (3, 'C').");
    }

    [Test]
    public void TestSimpleBlackFirst_invalidMove()
    {
        // Act: Move the knight from (1, 'B') to (3, 'C')
        var moveResult = _game.MakeMove(7, 'B', 6, 'C');
    
        // Assert: Verify that the knight has moved successfully and is at the new position
        Assert.IsFalse(moveResult, "The knight should move from (1, 'B') to (3, 'C').");
    }

    [Test]
    public void TestSimple2Moves()
    {
        var moveResult = _game.MakeMove(2, 'E', 4, 'E');
        
        // Assert: Verify that the pawn has moved successfully and is at the new position
        Assert.IsTrue(moveResult, "The pawn should move one square forward.");
        Assert.IsNull(_game.Board.getSquare(2, 'E'), "The original position should be empty.");
        Assert.IsInstanceOf<Pawn>(_game.Board.getSquare(4, 'E'), "The destination should contain the pawn.");
        
        //second move
        moveResult = _game.MakeMove(7, 'E', 5, 'E');
        Assert.IsTrue(moveResult, "The pawn should move one square forward.");
        Assert.IsNull(_game.Board.getSquare(7, 'E'), "The original position should be empty.");
        Assert.IsInstanceOf<Pawn>(_game.Board.getSquare(5, 'E'), "The destination should contain the pawn.");
    }
}
