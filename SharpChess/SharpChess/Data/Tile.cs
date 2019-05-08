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

        public Tile(int coordinate)
        {
            this.coordinate = coordinate;
            currentPiece = null;
        }

        public void addPiece(Piece p)
        {
            currentPiece = p;
        }

        public void removePiece()
        {
            currentPiece = null;
        }

        public bool hasPiece()
        {
            if (currentPiece == null)
                return false;
            else
                return true;
        }


    }
}
