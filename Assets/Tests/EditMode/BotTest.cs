using NUnit.Framework;
using chessBot;
using UnityEngine;

namespace Tests.EditMode
{
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
        }
    }
}