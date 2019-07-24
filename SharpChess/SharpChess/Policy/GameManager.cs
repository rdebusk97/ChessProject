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

        // Makes instances of the boardManager and moveManager
        public GameManager()
        {
            boardManager = new BoardManager();
            moveManager = new MoveManager();
        }

        // Resets a game
        public void reset()
        {
            GAME_OVER = false;
            pieceTurn = PieceAllegiance.WHITE;
            moveManager.clearList();
        }

        // Initiates a move within the game and adds it to the move list for history/tracking
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
            setNewTurn();
        }

        public Tuple<int, int> testCheck(List<Tuple<int, int>> potentialCoordinates)
        {
            Tuple<int, int> kingCheckCoordinate = boardManager.getKingCoordinate(getTurn());
            foreach (Tuple<int, int> coordinate in potentialCoordinates)
                if (coordinate.Item1 == kingCheckCoordinate.Item1 && coordinate.Item2 == kingCheckCoordinate.Item2)
                    return coordinate;
            return null;
        }

        // Sets the piece turn to whichever allegiance
        private void setNewTurn()
        {
            if (pieceTurn == PieceAllegiance.WHITE)
                pieceTurn = PieceAllegiance.BLACK;
            else
                pieceTurn = PieceAllegiance.WHITE;
        }

        // Gets the allegiance whom which turn it is
        public PieceAllegiance getTurn()
        {
            return pieceTurn;
        }

        public PieceAllegiance getAntiTurn()
        {
            if (pieceTurn == PieceAllegiance.WHITE)
                return PieceAllegiance.BLACK;
            else
                return PieceAllegiance.WHITE;
        }
    }
}
