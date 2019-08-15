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
        private Move move;

        // Adds a move to the move history
        public void addToMoveList(Move m)
        {
            moveHistory.Add(m);
            move = m;
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

        public void executeMove(Move m)
        {
            moveHistory.Add(m);
            m.endTile.setPiece(m.movedPiece);
            m.startTile.removePiece();
            m.movedPiece.doneMove();
        }

        public void unexecuteMove()
        {
            Move m = moveHistory.Last();
            if (m.capturedPiece != null)
                m.endTile.setPiece(m.capturedPiece);
            else
                m.endTile.removePiece();
            m.startTile.setPiece(m.movedPiece);
            m.movedPiece.undoneMove();
            moveHistory.Remove(moveHistory.Last());
        }

        public Move getUndoMove()
        {
            if (move != null)
                return move;
            return null;
        }

        public void removeLastMove()
        {
            moveHistory.Remove(moveHistory.Last());
        }
    }
}
