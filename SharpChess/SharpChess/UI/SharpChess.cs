using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpChess.Data;
using SharpChess.Data.Pieces;
using SharpChess.Policy;

namespace SharpChess
{
    public partial class SharpChess : Form
    {
        private const int BOARD_SIZE = 8;
        private const int DEBUG_DIRECTORY_OFFSET = 10;

        private Bitmap boardMap;
        private Board board;
        private Tile[,] tileMap;
        private GameManager gameManager = new GameManager();
        private string imagesDirectory = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName
            .Substring(0, Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName.Length - DEBUG_DIRECTORY_OFFSET);

        public SharpChess()
        {
            InitializeComponent();
            board = new Board();
            tileMap = board.getTileMap();
            boardMap = drawBoard();
            drawPiece(new King(PieceAllegiance.WHITE), 34);
        }

        private Bitmap drawBoard()
        {
            Bitmap tempBitMap = new Bitmap(boardPanel.Width, boardPanel.Height);
            Graphics graphics = Graphics.FromImage(tempBitMap);

            SolidBrush tanBrush = new SolidBrush(Color.Tan);
            SolidBrush beigeBrush = new SolidBrush(Color.Beige);

            int counter = 0;
            int tileSize = boardPanel.Height / BOARD_SIZE;

            for (int i = 0; i < BOARD_SIZE; i++)  
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    int xPixel = i * tileSize;
                    int yPixel = j * tileSize;
                    if (counter % 2 == 0)
                        graphics.FillRectangle(tanBrush, xPixel, yPixel, tileSize, tileSize);
                    else
                        graphics.FillRectangle(beigeBrush, xPixel, yPixel, tileSize, tileSize);

                    counter++;
                }
                counter++;
            }
            graphics.DrawImage(tempBitMap, 0, 0);
            return tempBitMap;
        }

        private Bitmap drawPiece(Piece piece, int coordinate)
        {
            Image pieceImage = Image.FromFile(imagesDirectory + piece.toImage());
            Graphics graphics = Graphics.FromImage(boardMap);
            int xPixel = (boardPanel.Width / board.specificWidth) * ((coordinate % BOARD_SIZE) - 1);
            int yPixel = (boardPanel.Width / board.specificWidth) * ((coordinate / BOARD_SIZE));
            int tileSize = boardPanel.Height / BOARD_SIZE;
            graphics.DrawImage(pieceImage, xPixel, yPixel, tileSize, tileSize);
            return boardMap;
        }

        private Bitmap drawBorder(int coordinate)
        {
            Pen p = new Pen(Color.Black, 5);
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileHeight = (boardPanel.Height / board.specificHeight);
            int tileWidth = (boardPanel.Width / board.specificWidth);
            int xCoordinate = (tileHeight * (coordinate % BOARD_SIZE) - 1);
            int yCoordinate = (tileWidth * ((coordinate / BOARD_SIZE)));
            graphics.DrawLine(p, xCoordinate, yCoordinate, xCoordinate + tileHeight, yCoordinate);
            return boardMap;
        }

        private Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(boardMap, 0, 0);
        }

        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Invalidate();
            Point point = boardPanel.PointToClient(Cursor.Position);
            int xValue = point.X;
            int yValue = point.Y;
            int tileSize = boardPanel.Height / BOARD_SIZE;
            int coordinate = gameManager.boardManager.calculateCoordinate(xValue, yValue, tileSize);
            //drawBorder(coordinate);
            label1.Text = "Tile Coordinate: " + coordinate.ToString();
            drawPiece(new King(PieceAllegiance.WHITE), coordinate);
        }
    }
}
