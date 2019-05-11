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

        public Board getBoard()
        {
            return board;
        }

        private void placePiece(Piece piece, int x, int y)
        {
            board.getTileMap()[x, y].setPiece(piece);          
        }

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

    }
}
