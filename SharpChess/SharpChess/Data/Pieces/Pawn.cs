using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PieceAllegiance allegiance) : base(allegiance)
        {
            this.allegiance = allegiance;
        }

        public override string toImage()
        {
            return "/Resources/" + this.allegiance.ToString() + "_PAWN.png";
        }

        public override string toString()
        {
            return "PAWN";
        }

        public override char toText()
        {
            return 'P';
        }
    }
}
