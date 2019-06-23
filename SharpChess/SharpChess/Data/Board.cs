using SharpChess.Data.Pieces;
using SharpChess.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data
{
    public class Board
    {
        private int boardSize;
        private Tile[,] tileMap;
        private Tile selectedTile;

        //Sizable board
        public Board(int size)
        {
            boardSize = size;
            instantiateBoard(size);
        }

        // Creates a board (Tile[,] array) of size 'statedSize'
        private void instantiateBoard(int statedSize)
        {
            tileMap = new Tile[statedSize, statedSize];
            for (int j = 0; j < statedSize; j++)
                for (int i = 0; i < statedSize; i++)
                    tileMap[j, i] = new Tile(i, j);
            setDefaultPieces();
        }

        // Resets the board to the default pieces
        public void resetBoard()
        {
            foreach (Tile t in tileMap)
                if (t.hasPlacedPiece())
                    t.removePiece();
            setDefaultPieces();
        }

        // Returns the size of the board (size will be same height and width)
        public int getBoardSize()
        {
            return boardSize;
        }

        // Default chess pieces on a board
        public void setDefaultPieces()
        {
            tileMap[0, 0].setPiece(new Rook(PieceAllegiance.BLACK));
            tileMap[0, 1].setPiece(new Knight(PieceAllegiance.BLACK));
            tileMap[0, 2].setPiece(new Bishop(PieceAllegiance.BLACK));
            tileMap[0, 3].setPiece(new Queen(PieceAllegiance.BLACK));
            tileMap[0, 4].setPiece(new King(PieceAllegiance.BLACK));
            tileMap[0, 5].setPiece(new Bishop(PieceAllegiance.BLACK));
            tileMap[0, 6].setPiece(new Knight(PieceAllegiance.BLACK));
            tileMap[0, 7].setPiece(new Rook(PieceAllegiance.BLACK));
            for (int i = 0; i < boardSize; i++)
                tileMap[1, i].setPiece(new Pawn(PieceAllegiance.BLACK));
            for (int i = 0; i < boardSize; i++)
                tileMap[6, i].setPiece(new Pawn(PieceAllegiance.WHITE));
            tileMap[7, 0].setPiece(new Rook(PieceAllegiance.WHITE));
            tileMap[7, 1].setPiece(new Knight(PieceAllegiance.WHITE));
            tileMap[7, 2].setPiece(new Bishop(PieceAllegiance.WHITE));
            tileMap[7, 4].setPiece(new King(PieceAllegiance.WHITE));
            tileMap[7, 3].setPiece(new Queen(PieceAllegiance.WHITE));
            tileMap[7, 5].setPiece(new Bishop(PieceAllegiance.WHITE));
            tileMap[7, 6].setPiece(new Knight(PieceAllegiance.WHITE));
            tileMap[7, 7].setPiece(new Rook(PieceAllegiance.WHITE));
        }

        // Retrieves  the 2D tile map
        public Tile[,] getTileMap()
        {
            return tileMap;
        }

        // Sets the selected current tile
        public void setTile(Tile t)
        {
            selectedTile = t;
        }

        // Retrieves the selected tile
        public Tile getTile()
        {
            return selectedTile;
        }
    }
}
