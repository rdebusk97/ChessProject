using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data
{
    public class ChessBoard
    {
        private const int STANDARD_SIZE = 8;
        private Tile[,] tiles;

        //Standard sized board
        public ChessBoard()
        {
            tiles = new Tile[STANDARD_SIZE, STANDARD_SIZE];
        }

        //Non-standard sized board
        public ChessBoard(int specificHeight, int specificWidth) 
        {
            tiles = new Tile[specificHeight, specificWidth];
        }

        public void createFreshBoard()
        {
            foreach (Tile t in tiles)
                t.currentPiece = null;
            //createStandardBoard(); //set up board
        }
    }
}
