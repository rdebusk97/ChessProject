using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public class Queen : Piece
    {
        public Queen(PieceAllegiance allegiance) : base(allegiance)
        {
            this.allegiance = allegiance;
        }

        public override List<Tuple<int, int>> populateGeneralMoves()
        {
            listOfGeneralMoves.Add(Tuple.Create(-1, -1));
            listOfGeneralMoves.Add(Tuple.Create(0, -1));
            listOfGeneralMoves.Add(Tuple.Create(1, -1));
            listOfGeneralMoves.Add(Tuple.Create(-1, 0));
            listOfGeneralMoves.Add(Tuple.Create(1, 0));
            listOfGeneralMoves.Add(Tuple.Create(-1, 1));
            listOfGeneralMoves.Add(Tuple.Create(0, 1));
            listOfGeneralMoves.Add(Tuple.Create(1, 1));
            return listOfGeneralMoves;
        }

        public override string toImage()
        {
            return "/Resources/" + this.allegiance.ToString() + "_QUEEN.png";
        }

        public override string toString()
        {
            return "QUEEN";
        }

        public override char toText()
        {
            return 'Q';
        }
    }
}
