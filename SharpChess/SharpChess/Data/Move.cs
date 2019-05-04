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

        public Move(Tile startTile, Tile endTile, Piece movedPiece)
        {
            this.startTile = startTile;
            this.endTile = endTile;
            this.movedPiece = movedPiece;
        }
    }
}
