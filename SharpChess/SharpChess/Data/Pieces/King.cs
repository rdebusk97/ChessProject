using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public class King : Piece
    {
        public List<Tuple<int, int>> legalMoves;

        public King(PieceAllegiance allegiance) : base(allegiance)
        {
            this.allegiance = allegiance;
        }

        public override string toImage()
        {
            return "/Resources/" + this.allegiance.ToString() + "_KING.png";
        }

        public override char toText()
        {
            return 'K';
        }

        public override string toString()
        {
            return "KING";
        }
    }
}
