using SharpChess.Data;
using SharpChess.Data.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Policy
{
    public class BoardManager
    {
        private const int BOARD_SIZE = 8;

        private Board board;
        private MoveManager moveManager;

        private List<Tuple<int, int>> borderCoordinates = new List<Tuple<int, int>>();
       
        public BoardManager()
        {
            board = new Board(BOARD_SIZE);
            moveManager = new MoveManager();
            generateBorderCoordinates(BOARD_SIZE);
        }

        // Returns the instance board
        public Board getBoard()
        {
            return board;
        }

        // Sets the piece for a tile
        private void placePiece(Piece piece, int x, int y)
        {
            board.getTileMap()[x, y].setPiece(piece);          
        }

        // Generates the coordinates that resemble the border (may be unused)
        private void generateBorderCoordinates(int boardSize)
        {
            for (int i = 0; i < boardSize; i++)
            {
                borderCoordinates.Add(Tuple.Create(0, i));
                borderCoordinates.Add(Tuple.Create(boardSize - 1, i));
            }
            for (int i = 1; i < boardSize - 1; i++)
            {
                borderCoordinates.Add(Tuple.Create(i, 0));
                borderCoordinates.Add(Tuple.Create(i, boardSize - 1));
            }
        }

        // Returns a tile at a coordinate, if nonexistent, return null.
        public Tile findTile(int x, int y)
        {
            if (0 <= x && x < BOARD_SIZE && 0 <= y && y < BOARD_SIZE)
                return getBoard().getTileMap()[y, x];
            else
                return null;
        }

        #region -- Destination Candidacy

        public void testPotentialPawnDestination(Tile currentTile)
        {
            throw new NotImplementedException();
        }

        // Used for testing simplistic piece destinations (i.e. Knight, King)
        public bool testPotentialSimplexDestination(Piece debatedPiece, int newX, int newY)
        {
            if (findTile(newX, newY) != null)
            {
                if (!findTile(newX, newY).hasPlacedPiece())
                    return true;
                else if (findTile(newX, newY).getCurrentPiece().getAllegiance() != debatedPiece.getAllegiance())
                    return true;
            }
            return false;
        }

        // Used for testing complex piece destinations (i.e. Queen, Rook, Bishop)
        public int testPotentialComplexDestination(Piece debatedPiece, int newX, int newY)
        {
            if (findTile(newX, newY) != null)
            {
                if (!findTile(newX, newY).hasPlacedPiece())
                    return 1;
                else
                {
                    if (findTile(newX, newY).getCurrentPiece().getAllegiance() != debatedPiece.getAllegiance())
                        return 0;
                }
            }
            return -1;
        }

        #endregion
    }
}
