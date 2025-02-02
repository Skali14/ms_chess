namespace chessBot
{
    public class Eval
    {
        int totalPoints = 0;
        
        // Method to calculate the total points on the board.
        public int CalculateBoardPoints(Chessboard board, bool isWhite)
        {
            // Iterate over board squares to calculate points
            foreach (var piece in board.Squares)
            {
                if (piece != null && piece.IsWhite == isWhite){
                    totalPoints += GetPieceValue(piece);
                } 
            }

            return totalPoints;
        }

        // Helper method to assign values to pieces
        private static int GetPieceValue(Piece piece)
        {
            return piece switch
            {
                Pawn => 1,
                Knight => 3,
                Bishop => 3,
                Rook => 5,
                Queen => 9,
                _ => 0
            };
        }
    }
}