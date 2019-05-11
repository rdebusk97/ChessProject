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
        protected Tuple<int, int> borderCoordinates;

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
