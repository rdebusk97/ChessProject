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
        public Piece currentPiece { get; set; }
        public int coordinate { get; set; }
        private bool hasPiece = false;


        public Tile(int coordinate)
        {
            this.coordinate = coordinate;
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


    }
}
