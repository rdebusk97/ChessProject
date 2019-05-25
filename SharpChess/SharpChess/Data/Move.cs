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
            switch (xValue)
            {
                case 0:
                    return 'A';
                case 1:
                    return 'B';
                case 2:
                    return 'C';
                case 3:
                    return 'D';
                case 4:
                    return 'E';
                case 5:
                    return 'F';
                case 6:
                    return 'G';
                default:
                    return 'A';
            }
        }
    }
}
