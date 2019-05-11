using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Policy
{
    public class GameManager
    {
        public BoardManager boardManager { get; private set; }

        public GameManager()
        {
            boardManager = new BoardManager();
        }
    }
}
