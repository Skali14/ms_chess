
public class Rook : Piece
{
    public bool wasMoved = false; // needed for castling checks

    public Rook(bool isWhite, string tag) : base(isWhite, tag)
    {
    }

    public override bool IsValidMove(int startRow, int startCol, int destRow, int destCol, Piece[,] squares, Game game)
    {
        // Ensure move is either horizontal or vertical and startPos is different from destPos
        if (startRow != destRow && startCol != destCol || startRow == destRow && startCol == destCol)
        {
            return false;
        }

        Piece destPiece = squares[destRow, destCol];
        if (destPiece != null && destPiece.IsWhite == IsWhite)
        {
            return false;
        }

        // Determine direction and check for obstructions
        int rowDirection = startRow == destRow ? 0 : (destRow > startRow ? 1 : -1);
        int colDirection = startCol == destCol ? 0 : (destCol > startCol ? 1 : -1);

        int row = startRow + rowDirection;
        int col = startCol + colDirection;

        while (row != destRow || col != destCol)
        {
            if (squares[row, col] != null) return false; // Blocked path
            row += rowDirection;
            col += colDirection;
        }

        return true;
    }
}
