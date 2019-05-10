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
        private const int BOARD_COLORS = 2;
        private const int BOARD_SIZE = 8;
        private const int DEBUG_DIRECTORY_OFFSET = 10;
        private string imagesDirectory = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName
            .Substring(0, Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName.Length - DEBUG_DIRECTORY_OFFSET);

        private Tuple<int, int> currentCoordinateClicked = new Tuple<int, int>(8, 8);

        private Bitmap boardMap;
        private GameManager gameManager = new GameManager();

        public SharpChess()
        {
            InitializeComponent();
            boardMap = drawBoard();
            drawInitialPieces();
        }

        #region -- Bitmap Drawing

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
                    if (counter % BOARD_COLORS == 0)
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

        private void drawInitialPieces()
        {
            foreach (Tile t in gameManager.boardManager.getBoard().getTileMap())
            {
                if (t.hasPlacedPiece())
                    drawPiece(t.getCurrentPiece(), t.x, t.y);
            }
        }

        private Bitmap drawPiece(Piece piece, int x, int y)
        {
            Image pieceImage = Image.FromFile(imagesDirectory + piece.toImage());
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = boardPanel.Height / BOARD_SIZE;
            int xPixel = tileSize * x;
            int yPixel = tileSize * y;
            graphics.DrawImage(pieceImage, xPixel, yPixel, tileSize, tileSize);
            return boardMap;
        }

        private Bitmap drawBorder(int x, int y)
        {
            int penThickness = 2;
            Pen p = new Pen(Color.Black, penThickness);
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = (boardPanel.Height / BOARD_SIZE);
            int xCoordinate = tileSize * x;
            int yCoordinate = tileSize * y;
            graphics.DrawLine(p, xCoordinate, yCoordinate + 1, xCoordinate + tileSize - 1, yCoordinate + 1);
            graphics.DrawLine(p, xCoordinate + 1, yCoordinate, xCoordinate + 1, yCoordinate + tileSize - 1);
            graphics.DrawLine(p, xCoordinate + tileSize - 1, yCoordinate, xCoordinate + tileSize - 1, yCoordinate + tileSize - 1);
            graphics.DrawLine(p, xCoordinate, yCoordinate + tileSize - 1, xCoordinate + tileSize, yCoordinate + tileSize - 1);
            return boardMap;
        }

        private Bitmap drawSquare(int x, int y)
        {
            SolidBrush tanBrush = new SolidBrush(Color.Tan);
            SolidBrush beigeBrush = new SolidBrush(Color.Beige);
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = (boardPanel.Height / BOARD_SIZE);
            int xCoordinate = tileSize * x;
            int yCoordinate = tileSize * y;
            int row = y;
            if (row % BOARD_COLORS == 1)
            {
                if (x % BOARD_COLORS == 0)
                    graphics.FillRectangle(beigeBrush, xCoordinate, yCoordinate, tileSize, tileSize);
                else
                    graphics.FillRectangle(tanBrush, xCoordinate, yCoordinate, tileSize, tileSize);
            }
            else
            {
                if (x % BOARD_COLORS == 0)
                    graphics.FillRectangle(tanBrush, xCoordinate, yCoordinate, tileSize, tileSize);
                else
                    graphics.FillRectangle(beigeBrush, xCoordinate, yCoordinate, tileSize, tileSize);
            }
            return boardMap;
        }

        private void drawBackPlacedPiece(int x, int y) //Wish there was an easier way to get around this...
        {
            foreach (Tile t in gameManager.boardManager.getBoard().getTileMap())
                if (t.x == x && t.y == y)
                    if (t.hasPlacedPiece())
                        drawPiece(t.getCurrentPiece(), x, y);
        }

        #endregion

        #region -- Board Event Handlers

        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Invalidate();
            Point point = boardPanel.PointToClient(Cursor.Position);
            int xValue = point.X;
            int yValue = point.Y;
            int tileSize = boardPanel.Height / BOARD_SIZE;
            // coordinate = gameManager.boardManager.calculateCoordinate(xValue, yValue, tileSize);
            int x = point.X / tileSize;
            int y = point.Y / tileSize;
            if (findTile(x, y).hasPlacedPiece())
            {
                if (x != currentCoordinateClicked.Item1 || y != currentCoordinateClicked.Item2) 
                {
                    drawSquare(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2); //draws square over place selected before
                    drawBackPlacedPiece(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2); //draws piece back in place before
                    drawBorder(x, y);
                    currentCoordinateClicked = Tuple.Create(x, y);
                    gameManager.boardManager.getBoard().setTile(findTile(x, y));
                }
                else
                {
                    drawSquare(x, y); //draws square 
                    drawBackPlacedPiece(x, y); //draw back piece
                    currentCoordinateClicked = Tuple.Create(-1, -1); 
                    gameManager.boardManager.getBoard().setTile(null);
                }
                displayUpdatedTileLabels(x, y);
            }
            //drawPiece(new King(PieceAllegiance.WHITE), coordinate);
            boardPanel.Refresh();
        }

        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(boardMap, 0, 0);
        }

        #endregion

        private void displayUpdatedTileLabels(int x, int y)
        {
            label1.Text = "Tile Coordinate: [" + x.ToString() + "," + y.ToString() + "]";
            if (findTile(x, y).hasPlacedPiece())
                currentPiece_lbl.Text = "Piece: " + findTile(x, y).getCurrentPiece().getAllegiance() + 
                    "_" + findTile(x, y).getCurrentPiece().toString();
        }


        private Tile findTile(int x, int y)
        {
            return gameManager.boardManager.getBoard().getTileMap()[y, x];
        }

        private Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

    }
}
