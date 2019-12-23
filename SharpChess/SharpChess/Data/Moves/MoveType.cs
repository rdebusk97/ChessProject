using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.Data.Moves
{
    public enum MoveType
    {
        REGULAR_MOVE,
        EN_PASSANT,
        PAWN_PROMOTION,
        KINGSIDE_CASTLE,
        QUEENSIDE_CASTLE
    }
}
