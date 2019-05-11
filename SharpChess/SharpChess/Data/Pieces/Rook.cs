using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public class Rook : Piece
    {
        public Rook(PieceAllegiance allegiance) : base(allegiance)
        {
            this.allegiance = allegiance;
        }

        public override List<Tuple<int, int>> populateGeneralMoves()
        {
            throw new NotImplementedException();
        }

        public override string toImage()
        {
            return "/Resources/" + this.allegiance.ToString() + "_ROOK.png";
        }

        public override string toString()
        {
            return "ROOK";
        }

        public override char toText()
        {
            return 'R';
        }
    }
}
