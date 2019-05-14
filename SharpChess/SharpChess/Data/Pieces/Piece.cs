using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Pieces
{
    public abstract class Piece
    {
        protected PieceAllegiance allegiance;
        protected bool hasPlayedMove;
        protected List<Tuple<int, int>> listOfGeneralMoves = new List<Tuple<int, int>>();

        public Piece(PieceAllegiance allegiance)
        {
            this.allegiance = allegiance;
            populateGeneralMoves();
            hasPlayedMove = false;
        }

        public bool hasPlayedFirstMove()
        {
            return hasPlayedMove;
        }

        public void setMovedTrue()
        {
            hasPlayedMove = true;
        }

        public PieceAllegiance getAllegiance() => allegiance;
        public List<Tuple<int, int>> getListOfGeneralMoves() => listOfGeneralMoves;

        public abstract List<Tuple<int, int>> populateGeneralMoves();
        public abstract char toText();
        public abstract string toString();
        public abstract string toImage();
    }
}
