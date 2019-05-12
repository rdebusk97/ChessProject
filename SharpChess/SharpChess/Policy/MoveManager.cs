using SharpChess.Data;
using SharpChess.Data.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Policy
{
    public class MoveManager
    {
        private List<Move> whiteMoveHistory = new List<Move>();
        private List<Move> blackMoveHistory = new List<Move>();

        public void addToMoveList(Move m)
        {
            if (m.movedPiece.getAllegiance() == PieceAllegiance.WHITE)
                whiteMoveHistory.Add(m);
            else
                blackMoveHistory.Add(m);
        }
    }
}
