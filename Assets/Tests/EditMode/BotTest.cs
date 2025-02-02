using NUnit.Framework;
using chessBot;
using UnityEngine;

public class BotTest
{
    private Game _game;
    private Eval _eval;

    [SetUp]
    public void Setup()
    {
        _eval = new Eval();
        _game = new Game();
    }

    [Test]
    public void TestInitEvalWhite()
    {
        var points = _eval.CalculateBoardPoints(_game.Board, true);
        Debug.Log("White: " + points);
    }

    [Test]
    public void TestInitEvalBlack()
    {
        var points = _eval.CalculateBoardPoints(_game.Board, false);
        Debug.Log("White: " + points);
        Assert.AreEqual(points, 39);
    }

    [Test]
    public void TestMiniMaxInit()
    {
        var minimax = new MiniMaxBot();
        var bestMove = minimax.FindBestMove(_game, 3); // Assuming 3 is the depth

        Debug.Log("here10");
        Debug.Log("Best move for white: " + bestMove);

        Assert.NotNull(bestMove, "Expected a valid move but got null");
    }
}