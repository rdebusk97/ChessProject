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
        public Tile[,] board; //{ get; private set; }

        //Standard sized board
        public Board()
        {
            int coordinate = 1;
            board = new Tile[STANDARD_SIZE, STANDARD_SIZE];
            for (int j = 0; j < STANDARD_SIZE; j++)
                for (int i = 0; i < STANDARD_SIZE; i++)
                {
                    board[j, i] = new Tile(coordinate);
                    ++coordinate;
                }
        }

        //Non-standard sized board
        public Board(int specificHeight, int specificWidth) 
        {
            board = new Tile[specificHeight, specificWidth];
        }

        public void createFreshBoard()
        {
            //createStandardBoard(); //set up board
        }

        public Tile[,] getTileMap()
        {
            return board;
        }
    }
}
