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

        // Constructor for a piece
        public Piece(PieceAllegiance allegiance)
        {
            this.allegiance = allegiance;
            populateGeneralMoves();
            hasPlayedMove = false;
        }

        // Checks to see if a piece has moved at least once
        public bool hasPlayedFirstMove()
        {
            return hasPlayedMove;
        }

        // Sets the variable to note the piece has moved at least once
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
