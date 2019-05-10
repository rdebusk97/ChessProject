using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public class Knight : Piece
    { 
        public Knight(PieceAllegiance allegiance) : base(allegiance)
        {
            this.allegiance = allegiance;
        }

        public override string toImage()
        {
            return "/Resources/" + this.allegiance.ToString() + "_KNIGHT.png";
        }

        public override string toString()
        {
            return "KNIGHT";
        }

        public override char toText()
        {
            return 'N';
        }
    }
}
