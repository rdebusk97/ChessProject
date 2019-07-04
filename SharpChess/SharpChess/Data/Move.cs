using SharpChess.Data;
using SharpChess.Data.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Policy
{
    public class Move
    {
        private const int ASCII_VAL = 65;

        public Tile startTile { get; private set; }
        public Tile endTile { get; private set; }
        public Piece movedPiece { get; private set; }

        // Constructor for a move, with start/end tile and the moved piece
        public Move(Tile startTile, Tile endTile, Piece movedPiece)
        {
            this.startTile = startTile;
            this.endTile = endTile;
            this.movedPiece = movedPiece;
        }

        // Converts coordinate notation to letter notation
        public char convertCoordinate(int xValue)
        {
            return Convert.ToChar((ASCII_VAL + xValue));
        }

        public override string ToString()
        {
            return movedPiece.getAllegiance().ToString() + ": " + convertCoordinate(startTile.x) + (startTile.y + 1) + " - "
                + convertCoordinate(endTile.x) + (endTile.y + 1) + " [ " + movedPiece.ToString() + " ]\n";
        }
    }
}
