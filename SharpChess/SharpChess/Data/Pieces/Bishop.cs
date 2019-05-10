using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(PieceAllegiance allegiance) : base(allegiance)
        {
            this.allegiance = allegiance;
        }

        public override string toImage()
        {
            return "/Resources/" + this.allegiance.ToString() + "_BISHOP.png";
        }

        public override string toString()
        {
            return "BISHOP";
        }

        public override char toText()
        {
            return 'B';
        }
    }
}
