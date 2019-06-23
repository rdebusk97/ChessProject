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

        public override string ToString()
        {
            return "KING";
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

        public override int toValue()
        {
            return 10;
        }
    }
}
