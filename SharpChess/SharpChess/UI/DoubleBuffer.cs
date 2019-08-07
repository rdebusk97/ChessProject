using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChess.UI
{
    public class DoubleBuffer : System.Windows.Forms.Panel
    {
        public DoubleBuffer() : base()
        {
            DoubleBuffered = true;
        }
    }
}
