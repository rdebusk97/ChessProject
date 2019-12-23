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
        protected int movesPlayed;
        protected List<Tuple<int, int>> listOfGeneralMoves = new List<Tuple<int, int>>();

        // Constructor for a piece
        public Piece(PieceAllegiance allegiance)
        {
            this.allegiance = allegiance;
            populateGeneralMoves();
            movesPlayed = 0;
        }

        // If move is played, move count incremented
        public void doneMove()
        {
            movesPlayed++;
        }

        // If move undone, move count decremented
        public void undoneMove()
        {
            movesPlayed--;
        }

        public int getMovesPlayed() => movesPlayed;
        public PieceAllegiance getAllegiance() => allegiance;
        public List<Tuple<int, int>> getListOfGeneralMoves() => listOfGeneralMoves;

        public abstract List<Tuple<int, int>> populateGeneralMoves();
        public abstract char toText();
        public abstract string toImage();
        public abstract int toValue();
    }
}
