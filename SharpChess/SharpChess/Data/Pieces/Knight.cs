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

        public override List<Tuple<int, int>> populateGeneralMoves()
        {
            listOfGeneralMoves.Add(Tuple.Create(1, -2));
            listOfGeneralMoves.Add(Tuple.Create(2, -1));
            listOfGeneralMoves.Add(Tuple.Create(2, 1));
            listOfGeneralMoves.Add(Tuple.Create(1, 2));
            listOfGeneralMoves.Add(Tuple.Create(-1, 2));
            listOfGeneralMoves.Add(Tuple.Create(-2, 1));
            listOfGeneralMoves.Add(Tuple.Create(-2, -1));
            listOfGeneralMoves.Add(Tuple.Create(-1, -2));
            return listOfGeneralMoves;
        }

        public override string toImage()
        {
            return "/Resources/" + allegiance.ToString() + "_KNIGHT.png";
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
