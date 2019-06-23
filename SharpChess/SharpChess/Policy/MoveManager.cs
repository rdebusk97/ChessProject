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

        // Adds a move to the move history
        public void addToMoveList(Move m)
        {
            moveHistory.Add(m);
        }

        // Clears move history
        public void clearList()
        {
            moveHistory.Clear();
        }

        // Retrieves the most recent move (end of the list)
        public Move getRecentMove()
        {
            return moveHistory.Last();
        }
    }
}
