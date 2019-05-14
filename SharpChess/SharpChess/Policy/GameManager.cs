using SharpChess.Data;
using SharpChess.Data.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Policy
{
    public class GameManager
    {
        public BoardManager boardManager { get; private set; }
        public MoveManager moveManager { get; private set; }

        private bool GAME_OVER = false;
        private PieceAllegiance pieceTurn = PieceAllegiance.WHITE;

        public GameManager()
        {
            boardManager = new BoardManager();
            moveManager = new MoveManager();
        }

        public void playMove(Piece piece, Tile startTile, Tile endTile)
        {
            endTile.setPiece(piece);
            startTile.removePiece();
            if (!piece.hasPlayedFirstMove())
            {
                piece.setMovedTrue();
                piece.populateGeneralMoves();
            }
            moveManager.addToMoveList(new Move(startTile, endTile, piece));
        }

        public void newTurn()
        {
            if (pieceTurn == PieceAllegiance.WHITE)
                pieceTurn = PieceAllegiance.BLACK;
            else
                pieceTurn = PieceAllegiance.WHITE;
        }

        public PieceAllegiance getTurn()
        {
            return pieceTurn;
        }
    }
}
