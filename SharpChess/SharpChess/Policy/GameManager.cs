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
        private int movesPlayed = 0;
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
            movesPlayed = 0;
        }

        public void executeMove(Piece piece, Tile startTile, Tile endTile)
        {
            Piece capturedPiece = null;
            if (endTile.getCurrentPiece() != null)
                capturedPiece = endTile.getCurrentPiece();
            Move m = new Move(startTile, endTile, piece, capturedPiece);
            moveManager.executeMove(m);
            movesPlayed++;
            nextTurnSetup(piece);
        }

        public void unexecuteMove()
        {
            Move m = moveManager.getRecentMove();
            moveManager.unexecuteMove();
            movesPlayed--;
            nextTurnSetup(m.movedPiece);
        }

        private void nextTurnSetup(Piece piece)
        {
            if (piece.getMovesPlayed() <= 1)
                piece.populateGeneralMoves();
            setNewTurn();
        }

        public Tuple<int, int> testCheck(List<Tuple<int, int>> potentialCoordinates)
        {
            Tuple<int, int> kingCheckCoordinate = boardManager.getKingCoordinate(getTurn());
            foreach (Tuple<int, int> coordinate in potentialCoordinates)
                if (kingCheckCoordinate != null)
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

        //Gets moves played in game thus far
        public int getMovesPlayed()
        {
            return movesPlayed;
        }
    }
}
