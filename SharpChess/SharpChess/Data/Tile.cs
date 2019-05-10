using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpChess.Data.Pieces;

namespace SharpChess.Data
{
    public class Tile
    {
        public int x { get; private set; }
        public int y { get; private set; }

        private bool hasPiece = false;
        private Piece currentPiece;


        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            currentPiece = null;
        }

        public void setPiece(Piece p)
        {
            currentPiece = p;
            hasPiece = true;
        }

        public void removePiece()
        {
            currentPiece = null;
            hasPiece = false;
        }

        public bool hasPlacedPiece()
        {
            return hasPiece;
        }

        public Piece getCurrentPiece()
        {
            return currentPiece;
        }


    }
}
