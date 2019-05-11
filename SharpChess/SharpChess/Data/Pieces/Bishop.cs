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

        public override List<Tuple<int, int>> populateGeneralMoves()
        {
            listOfGeneralMoves.Add(Tuple.Create(-1, -1));
            listOfGeneralMoves.Add(Tuple.Create(-1, 1));
            listOfGeneralMoves.Add(Tuple.Create(1, 1));
            listOfGeneralMoves.Add(Tuple.Create(1, -1));
            return listOfGeneralMoves;
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
