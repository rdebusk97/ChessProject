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
        public PieceAllegiance allegiance { get; set; }

        public Piece(PieceAllegiance allegiance)
        {
            this.allegiance = allegiance;
        }

        public abstract char toText();

        public abstract string toImage();
    }
}
