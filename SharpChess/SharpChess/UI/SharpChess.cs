using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpChess.Data;

namespace SharpChess
{
    public partial class SharpChess : Form
    {
        private const int BOARD_SIZE = 8;

        private Bitmap boardMap;
        private Board board;
        private Tile[,] tileMap;

        public SharpChess()
        {
            InitializeComponent();
            board = new Board();
            tileMap = board.getTileMap();
            boardMap = drawBoard();
        }

        private Bitmap drawBoard()
        {
            Bitmap tempBitMap = new Bitmap(boardPanel.Width, boardPanel.Height);
            Graphics graphics = Graphics.FromImage(tempBitMap);

            SolidBrush tanBrush = new SolidBrush(Color.Tan);
            SolidBrush beigeBrush = new SolidBrush(Color.Beige);

            int counter = 0;
            int tileHeight = boardPanel.Height / BOARD_SIZE;
            int tileWidth = boardPanel.Width / BOARD_SIZE;

            for (int i = 0; i < BOARD_SIZE; i++)  
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {

                    int xPixel = i * tileWidth;
                    int yPixel = j * tileHeight;

                    if (counter % 2 == 0)
                        graphics.FillRectangle(tanBrush, xPixel, yPixel, tileWidth, tileHeight);
                    else
                        graphics.FillRectangle(beigeBrush, xPixel, yPixel, tileWidth, tileHeight);

                    counter++;
                }
                counter++;
            }

            graphics.DrawImage(tempBitMap, 0, 0);
            return tempBitMap;
        }

        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(boardMap, 0, 0);
        }
    }
}
