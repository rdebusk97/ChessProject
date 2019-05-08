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
        public int specificHeight { get; private set; }
        public int specificWidth { get; private set; }
        private Tile[,] board;

        //Standard sized board
        public Board()
        {
            this.specificHeight = STANDARD_SIZE;
            this.specificWidth = STANDARD_SIZE;
            createFreshBoard(STANDARD_SIZE, STANDARD_SIZE);
        }

        private void createFreshBoard(int specificHeight, int specificWidth)
        {
            board = new Tile[specificHeight, specificWidth];
            int coordinate = 1;
            for (int j = 0; j < specificWidth; j++)
                for (int i = 0; i < specificWidth; i++)
                {
                    board[j, i] = new Tile(coordinate);
                    ++coordinate;
                }
        }

        public Tile[,] getTileMap()
        {
            return board;
        }
    }
}
