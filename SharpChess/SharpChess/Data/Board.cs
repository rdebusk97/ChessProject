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
        private const int STANDARD_SIZE = 8;
        private Tile[,] tileMap;
        private Tile selectedTile;

        //Standard sized board
        public Board()
        {
            createFreshBoard();
        }

        private void createFreshBoard()
        {
            tileMap = new Tile[STANDARD_SIZE, STANDARD_SIZE];
            int coordinate = 1;
            for (int j = 0; j < STANDARD_SIZE; j++)
                for (int i = 0; i < STANDARD_SIZE; i++)
                {
                    tileMap[j, i] = new Tile(coordinate);
                    ++coordinate;
                }
            setDefaultPieces();
        }

        public List<Move> listOfPotentialMoves()
        {
            Piece p = selectedTile.currentPiece;
            p.getListOfMoves();
        }

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
            for (int i = 0; i < STANDARD_SIZE; i++)
                tileMap[1, i].setPiece(new Pawn(PieceAllegiance.BLACK));
            for (int i = 0; i < STANDARD_SIZE; i++)
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

        public Tile[,] getTileMap()
        {
            return tileMap;
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
