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

        private int currentCoordinateClicked;

        private Bitmap boardMap;
        private Board board;
        private Tile[,] tileMap;
        private GameManager gameManager = new GameManager();
        private string imagesDirectory = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName
            .Substring(0, Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName.Length - DEBUG_DIRECTORY_OFFSET);

        public SharpChess()
        {
            InitializeComponent();
            board = gameManager.boardManager.getBoard();
            tileMap = board.getTileMap();
            boardMap = drawBoard();
            drawInitialPieces();
            //drawPiece(new King(PieceAllegiance.WHITE), 34);
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

        private void drawInitialPieces()
        {
            foreach (Tile t in tileMap)
            {
                if (t.hasPlacedPiece())
                    drawPiece(t.currentPiece, t.coordinate);
            }
        }

        private Bitmap drawPiece(Piece piece, int coordinate)
        {
            Image pieceImage = Image.FromFile(imagesDirectory + piece.toImage());
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = boardPanel.Height / BOARD_SIZE;
            int xPixel = tileSize * ((coordinate - 1) % (BOARD_SIZE));
            int yPixel = tileSize * (((coordinate - 1) / (BOARD_SIZE)));
            graphics.DrawImage(pieceImage, xPixel, yPixel, tileSize, tileSize);
            return boardMap;
        }

        private Bitmap drawBorder(int coordinate)
        {
            int penThickness = 2;
            Pen p = new Pen(Color.Black, penThickness);
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = (boardPanel.Height / BOARD_SIZE);
            int xCoordinate = tileSize * ((coordinate - 1) % (BOARD_SIZE));
            int yCoordinate = tileSize * (((coordinate - 1) / (BOARD_SIZE)));
            graphics.DrawLine(p, xCoordinate, yCoordinate + 1, xCoordinate + tileSize - 1, yCoordinate + 1);
            graphics.DrawLine(p, xCoordinate + 1, yCoordinate, xCoordinate + 1, yCoordinate + tileSize - 1);
            graphics.DrawLine(p, xCoordinate + tileSize - 1, yCoordinate, xCoordinate + tileSize - 1, yCoordinate + tileSize - 1);
            graphics.DrawLine(p, xCoordinate, yCoordinate + tileSize - 1, xCoordinate + tileSize, yCoordinate + tileSize - 1);
            return boardMap;
        }

        private Bitmap drawSquare(int coordinate)
        {
            SolidBrush tanBrush = new SolidBrush(Color.Tan);
            SolidBrush beigeBrush = new SolidBrush(Color.Beige);
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = (boardPanel.Height / BOARD_SIZE);
            int xCoordinate = tileSize * ((coordinate - 1) % (BOARD_SIZE));
            int yCoordinate = tileSize * (((coordinate - 1) / (BOARD_SIZE)));
            int row = ((coordinate - 1) / BOARD_SIZE);
            if (row % 2 == 1)
            {
                if ((coordinate - 1) % 2 == 0)
                    graphics.FillRectangle(beigeBrush, xCoordinate, yCoordinate, tileSize, tileSize);
                else
                    graphics.FillRectangle(tanBrush, xCoordinate, yCoordinate, tileSize, tileSize);
            }
            else
            {
                if ((coordinate - 1) % 2 == 0)
                    graphics.FillRectangle(tanBrush, xCoordinate, yCoordinate, tileSize, tileSize);
                else
                    graphics.FillRectangle(beigeBrush, xCoordinate, yCoordinate, tileSize, tileSize);
            }
            return boardMap;
        }

        private void drawBackPlacedPiece(int coordinate) //Wish there was an easier way to get around this...
        {
            foreach (Tile t in tileMap)
                if (t.coordinate == coordinate)
                    if (t.hasPlacedPiece())
                        drawPiece(t.currentPiece, coordinate);
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
            int coordinate = gameManager.boardManager.calculateCoordinate(xValue, yValue, tileSize);
            if (findTile(coordinate).hasPlacedPiece())
            {
                if (coordinate != currentCoordinateClicked)
                {
                    drawSquare(currentCoordinateClicked);
                    drawBackPlacedPiece(currentCoordinateClicked);
                    drawBorder(coordinate);
                    currentCoordinateClicked = coordinate;
                    gameManager.boardManager.getBoard().setTile(findTile(coordinate));
                }
                else
                {
                    drawSquare(coordinate);
                    drawBackPlacedPiece(coordinate);
                    currentCoordinateClicked = 0;
                    gameManager.boardManager.getBoard().setTile(null);
                }
                displayUpdatedTileLabels(coordinate);
            }
            //drawPiece(new King(PieceAllegiance.WHITE), coordinate);
            boardPanel.Refresh();
        }

        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(boardMap, 0, 0);
        }

        #endregion

        private void displayUpdatedTileLabels(int coordinate)
        {
            label1.Text = "Tile Coordinate: " + coordinate.ToString();
            if (findTile(coordinate).hasPlacedPiece())
                currentPiece_lbl.Text = "Piece: " + findTile(coordinate).currentPiece.getAllegiance() + "_" + findTile(coordinate).currentPiece.toString();
        }


        private Tile findTile(int coordinate)
        {
            foreach (Tile t in tileMap)
                if (t.coordinate == coordinate)
                    return t;
            return null;
        }

        private Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

    }
}
