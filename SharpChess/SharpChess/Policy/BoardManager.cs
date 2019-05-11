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
        private Board board;
        private MoveManager moveManager;
        private const int BOARD_SIZE = 8;
       
        public BoardManager()
        {
            board = new Board(BOARD_SIZE);
            moveManager = new MoveManager();
        }

        public Board getBoard()
        {
            return board;
        }

        private void placePiece(Piece piece, int x, int y)
        {
            board.getTileMap()[x, y].setPiece(piece);          
        }

    }
}
