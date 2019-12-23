using SharpChess.Data;             
using SharpChess.Data.Pieces;
using SharpChess.Data.Moves;
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

        //private bool GAME_OVER = false;
        private int movesPlayed;
        private PieceAllegiance pieceTurn;

        // Makes instances of the boardManager and moveManager
        public GameManager()
        {
            boardManager = new BoardManager();
            moveManager = new MoveManager();
            movesPlayed = 0;
            pieceTurn = PieceAllegiance.WHITE;
        }

        // Resets a game
        public void reset()
        {
            //GAME_OVER = false;
            pieceTurn = PieceAllegiance.WHITE;
            moveManager.clearList();
            movesPlayed = 0;
        }

        private Move createMove(Piece piece, Tile startTile, Tile endTile)
        {
            Piece capturedPiece = endTile.getCurrentPiece();
            if (piece is Pawn && startTile.x != endTile.x && capturedPiece == null)
                return new Move(startTile, endTile, piece, capturedPiece, MoveType.EN_PASSANT);
            else
                return new Move(startTile, endTile, piece, capturedPiece, MoveType.REGULAR_MOVE);
        }

        public void executeMove(Piece piece, Tile startTile, Tile endTile)
        {
            Move m = createMove(piece, startTile, endTile);
            if (m.moveType == MoveType.EN_PASSANT)
                boardManager.findTile(m.endTile.x, m.startTile.y).removePiece();
            moveManager.executeMove(m);
            movesPlayed++;
            nextTurnSetup(piece);
        }

        public void unexecuteMove()
        {
            Move m = moveManager.getRecentMove();
            moveManager.unexecuteMove();
            if (m.moveType == MoveType.EN_PASSANT)
            {
                Piece p = new Pawn(getTurn());
                boardManager.findTile(m.endTile.x, m.startTile.y).setPiece(p);
                p.doneMove();
                p.populateGeneralMoves();
            }
            movesPlayed--;
            nextTurnSetup(m.movedPiece);
        }

        private void undoEnPassant(Move m)
        {
            Piece p = new Pawn(getTurn());
            boardManager.findTile(m.endTile.x, m.startTile.y).setPiece(p);
            p.doneMove();
            p.populateGeneralMoves();
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
