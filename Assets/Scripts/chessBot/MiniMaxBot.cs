using System.Collections.Generic;

namespace chessBot
{
    public class MiniMaxBot
    {
        private Eval _eval = new Eval();

        public (int StartRow, int StartCol, int DestRow, int DestCol) FindBestMove(Game game, int depth)
        {
            return (-1, -1, -1, -1);
        }

        private int MiniMax(Game game, int depth, int alpha, int beta, bool isMaximizing)
        {

            return -1;
        }

        private List<(int StartRow, int StartCol, int DestRow, int DestCol)> GenerateAllMoves(Game game, bool isWhite)
        {
            List<(int StartRow, int StartCol, int DestRow, int DestCol)> moves = new List<(int, int, int, int)>();

            for (var startRow = 0; startRow < 8; startRow++)
            {
                for (var startCol = 0; startCol < 8; startCol++)
                {
                    var piece = game.Board.Squares[startRow, startCol];
                    if (piece != null && piece.IsWhite == isWhite)
                    {
                        for (var destRow = 0; destRow < 8; destRow++)
                        {
                            for (var destCol = 0; destCol < 8; destCol++)
                            {
                                if (piece.IsValidMove(startRow, startCol, destRow, destCol, game.Board.Squares,
                                        game))
                                {
                                    moves.Add((startRow, startCol, destRow, destCol));
                                }
                            }
                        }
                    }
                }
            }

            return moves;
        }
    }
}