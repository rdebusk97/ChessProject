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
        private List<Move> moveHistory = new List<Move>();
        private List<Move> blackMoveHistory = new List<Move>();

        public void addToMoveList(Move m)
        {
            moveHistory.Add(m);
        }

        public void clearLists()
        {
            moveHistory.Clear();
        }

        public Move getRecentMove(PieceAllegiance allegiance)
        {
            return moveHistory.Last();
        }
    }
}
