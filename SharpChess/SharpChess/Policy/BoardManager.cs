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

        private void placePiece(Piece piece, int coordinate)
        {
            foreach(Tile t in board.board)
                if (t.coordinate == coordinate)
                    t.currentPiece = piece;           
        }
    }
}
