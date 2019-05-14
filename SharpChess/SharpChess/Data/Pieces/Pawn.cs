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

        public override List<Tuple<int, int>> populateGeneralMoves()
        {
            listOfGeneralMoves.Clear();
            int allegianceDirection = getAllegianceValue();
            listOfGeneralMoves.Add(Tuple.Create(0, allegianceDirection));
            if (!hasPlayedFirstMove())
                listOfGeneralMoves.Add(Tuple.Create(0, allegianceDirection * 2));
            listOfGeneralMoves.Add(Tuple.Create(-1, allegianceDirection));
            listOfGeneralMoves.Add(Tuple.Create(1, allegianceDirection));
            return listOfGeneralMoves;
        }

        public List<Tuple<int, int>> addEnPassantMove()
        {
            return listOfGeneralMoves;
        }

        private int getAllegianceValue()
        {
            if (allegiance == PieceAllegiance.WHITE)
                return -1;
            else
                return 1;
        }
    }
}
