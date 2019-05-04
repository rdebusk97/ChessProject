using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Policy
{
    public class MoveManager
    {
        private List<Move> whiteMoveHistory = new List<Move>();
        private List<Move> blackMoveHistory = new List<Move>();
        private int totalMoveCounter = 0;
    }
}
