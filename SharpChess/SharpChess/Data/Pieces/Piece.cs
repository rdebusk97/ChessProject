using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public abstract class Piece
    {
        protected PieceAllegiance allegiance;

        private int[] topRow = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private int[] bottomRow = { 57, 58, 59, 60, 61, 62, 63, 64 };
        private int[] leftColumn = { 1, 9, 17, 25, 33, 41, 49, 57 };
        private int[] rightColumn = { 8, 16, 24, 32, 40, 48, 56, 64 };

        public Piece(PieceAllegiance allegiance)
        {
            this.allegiance = allegiance;
        }

        public PieceAllegiance getAllegiance()
        {
            return allegiance;
        }

        public abstract char toText();
        public abstract string toString();
        public abstract string toImage();

    }
}
