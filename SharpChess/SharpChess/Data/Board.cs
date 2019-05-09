using SharpChess.Data.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data
{
    public class Board
    {
        private const int STANDARD_SIZE = 8;
        private Tile[,] board;
        private Tile selectedTile;

        //Standard sized board
        public Board()
        {
            createFreshBoard();
        }

        private void createFreshBoard()
        {
            board = new Tile[STANDARD_SIZE, STANDARD_SIZE];
            int coordinate = 1;
            for (int j = 0; j < STANDARD_SIZE; j++)
                for (int i = 0; i < STANDARD_SIZE; i++)
                {
                    board[j, i] = new Tile(coordinate);
                    ++coordinate;
                }
            setDefaultPieces();
        }

        public void setDefaultPieces()
        {
            board[0, 0].setPiece(new Rook(PieceAllegiance.BLACK));
            board[0, 1].setPiece(new Knight(PieceAllegiance.BLACK));
            board[0, 2].setPiece(new Bishop(PieceAllegiance.BLACK));
            board[0, 3].setPiece(new Queen(PieceAllegiance.BLACK));
            board[0, 4].setPiece(new King(PieceAllegiance.BLACK));
            board[0, 5].setPiece(new Bishop(PieceAllegiance.BLACK));
            board[0, 6].setPiece(new Knight(PieceAllegiance.BLACK));
            board[0, 7].setPiece(new Rook(PieceAllegiance.BLACK));
            for (int i = 0; i < STANDARD_SIZE; i++)
                board[1, i].setPiece(new Pawn(PieceAllegiance.BLACK));
            for (int i = 0; i < STANDARD_SIZE; i++)
                board[6, i].setPiece(new Pawn(PieceAllegiance.WHITE));
            board[7, 0].setPiece(new Rook(PieceAllegiance.WHITE));
            board[7, 1].setPiece(new Knight(PieceAllegiance.WHITE));
            board[7, 2].setPiece(new Bishop(PieceAllegiance.WHITE));
            board[7, 3].setPiece(new King(PieceAllegiance.WHITE));
            board[7, 4].setPiece(new Queen(PieceAllegiance.WHITE));
            board[7, 5].setPiece(new Bishop(PieceAllegiance.WHITE));
            board[7, 6].setPiece(new Knight(PieceAllegiance.WHITE));
            board[7, 7].setPiece(new Rook(PieceAllegiance.WHITE));
        }

        public Tile[,] getTileMap()
        {
            return board;
        }

        public void setTile(Tile t)
        {
            selectedTile = t;
        }

        public Tile getTile()
        {
            return selectedTile;
        }
    }
}
