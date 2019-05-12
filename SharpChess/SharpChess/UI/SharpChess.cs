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
        private const int DEBUG_DIRECTORY_OFFSET = 10;
        private MoveState moveState = MoveState.NO_SELECTION;

        // Should probably find a better way to get to the directory than this, but it's a work-around...
        private string imagesDirectory = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName
            .Substring(0, Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName.Length - DEBUG_DIRECTORY_OFFSET); 

        private Tuple<int, int> currentCoordinateClicked = new Tuple<int, int>(8, 8);
        private List<Tuple<int, int>> potentialCoordinates = new List<Tuple<int, int>>();

        private int boardSize;

        private SolidBrush tanBrush = new SolidBrush(Color.Tan);
        private SolidBrush beigeBrush = new SolidBrush(Color.Beige);

        private Bitmap boardMap;
        private GameManager gameManager = new GameManager();

        // Constructor at startup, builds/draws the board and pieces
        public SharpChess()
        {
            boardSize = gameManager.boardManager.getBoard().getBoardSize();
            InitializeComponent();
            boardMap = drawBoard();
            drawInitialPieces();
        }

        #region -- Bitmap Drawing

        // Draws out whole chess board (just the board itself, no pieces)
        private Bitmap drawBoard()
        {
            Bitmap tempBitMap = new Bitmap(boardPanel.Width, boardPanel.Height);
            Graphics graphics = Graphics.FromImage(tempBitMap);
            int counter = 0, tileSize = boardPanel.Height / boardSize;
            for (int i = 0; i < boardSize; i++)  
            {
                for (int j = 0; j < boardSize; j++)
                {
                    int xPixel = i * tileSize, yPixel = j * tileSize;
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

        // Draws any initial pieces that were placed on the board at startup
        private void drawInitialPieces()
        {
            foreach (Tile t in gameManager.boardManager.getBoard().getTileMap())
                if (t.hasPlacedPiece())
                    drawPiece(t.getCurrentPiece(), t.x, t.y);
        }

        // Draws a piece onto the board given an x and y coordinate
        private Bitmap drawPiece(Piece piece, int x, int y)
        {
            Image pieceImage = Image.FromFile(imagesDirectory + piece.toImage());
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = boardPanel.Height / boardSize;
            int xPixel = tileSize * x, yPixel = tileSize * y;
            graphics.DrawImage(pieceImage, xPixel, yPixel, tileSize, tileSize);
            return boardMap;
        }

        // Draws a border around a tile at an x and y coordinate
        private Bitmap drawBorder(int x, int y)
        {
            int penThickness = 2;
            float ratio = .9f;
            Pen p = new Pen(Color.Black, penThickness);
            SolidBrush brush = new SolidBrush(Color.Khaki);
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = (boardPanel.Height / boardSize);
            int xCoordinate = tileSize * x, yCoordinate = tileSize * y;
            graphics.FillRectangle(brush, xCoordinate, yCoordinate, tileSize, tileSize);
            drawBackPlacedPiece(x, y);
            graphics.DrawLine(p, xCoordinate, yCoordinate + 1, xCoordinate + (tileSize * ratio) /*- 1*/, yCoordinate + 1);
            graphics.DrawLine(p, xCoordinate + 1, yCoordinate, xCoordinate + 1, yCoordinate + (tileSize * ratio) /*- 1*/);
            //graphics.DrawLine(p, xCoordinate + tileSize - 1, yCoordinate, xCoordinate + tileSize - 1, yCoordinate + tileSize - 1);
            graphics.DrawLine(p, xCoordinate + tileSize, yCoordinate + tileSize - 1, xCoordinate + tileSize, yCoordinate + tileSize - 1);
            return boardMap;
        }

        // Draws new tile square over existing (as to reset the tile)
        private Bitmap drawSquare(int x, int y)
        {
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = (boardPanel.Height / boardSize);
            int xCoordinate = tileSize * x, yCoordinate = tileSize * y, row = y;
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

        // Draws piece onto the board at a specified x and y coordinate, assuming there is a piece there
        private void drawBackPlacedPiece(int x, int y)
        {
            foreach (Tile t in gameManager.boardManager.getBoard().getTileMap())
                if (t.x == x && t.y == y)
                    if (t.hasPlacedPiece())
                        drawPiece(t.getCurrentPiece(), x, y);
        }

        #endregion

        #region -- Board Event Handlers

        // Clicking tiles, showing potential moves and information
        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = boardPanel.PointToClient(Cursor.Position);
            int tileSize = boardPanel.Height / boardSize;
            int x = point.X / tileSize, y = point.Y / tileSize;
            Tile currentTile = findTile(x, y);
            if (currentTile.hasPlacedPiece())
            {
                if (x != currentCoordinateClicked.Item1 || y != currentCoordinateClicked.Item2)
                    newlyClickedCoordinate(currentTile);
                else
                    closingCurrentCoordinate(currentTile);
                displayUpdatedTileLabels(x, y);
            }
            boardPanel.Refresh();
        }

        // Paints new board
        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(boardMap, 0, 0);
        }

        #endregion

        #region -- Form Updates

        // Displays current tile and piece clicked
        private void displayUpdatedTileLabels(int x, int y)
        {
            label1.Text = "Tile Coordinate: [" + x.ToString() + "," + y.ToString() + "]";
            if (findTile(x, y).hasPlacedPiece())
                currentPiece_lbl.Text = "Piece: " + findTile(x, y).getCurrentPiece().getAllegiance() + 
                    "_" + findTile(x, y).getCurrentPiece().toString();
        }

        #endregion

        #region -- Helper Methods

        // Finds tile given x and y coordinates
        private Tile findTile(int x, int y)
        {
            if (0 <= x && x < boardSize && 0 <= y && y < boardSize)
                return gameManager.boardManager.getBoard().getTileMap()[y, x];
            else
                return null;
        }

        // Checks for legal coordinate and will draw the bordered coordinate
        private void drawPotentialDestinations(int newX, int newY, Tile currentTile)
        {
            if (0 <= newX && newX < boardSize && 0 <= newY && newY < boardSize) //legal spot on board
            {
                drawBorder(newX, newY);
                potentialCoordinates.Add(Tuple.Create(newX, newY));
                boardPanel.Refresh();
            }
        }

        // Displays new bordered coordinate and its potential moves
        private void newlyClickedCoordinate(Tile currentTile)
        {
            drawSquare(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2);
            drawBackPlacedPiece(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2);
            foreach (Tuple<int, int> coordinate in potentialCoordinates)
            {
                drawSquare(coordinate.Item1, coordinate.Item2);
                drawBackPlacedPiece(coordinate.Item1, coordinate.Item2);
            }
            potentialCoordinates.Clear();
            drawBorder(currentTile.x, currentTile.y);
            currentCoordinateClicked = Tuple.Create(currentTile.x, currentTile.y);
            gameManager.boardManager.getBoard().setTile(currentTile);
            testPotentialDestinations(currentTile);
            moveState = MoveState.SELECTED_START; //move states will become important later on
        }

        // Undisplays current coordinate and opens new one
        private void closingCurrentCoordinate(Tile currentTile)
        {
            foreach (Tuple<int, int> coordinate in potentialCoordinates)
            {
                drawSquare(coordinate.Item1, coordinate.Item2);
                drawBackPlacedPiece(coordinate.Item1, coordinate.Item2);
            }
            potentialCoordinates.Clear();
            drawSquare(currentTile.x, currentTile.y);
            drawBackPlacedPiece(currentTile.x, currentTile.y);
            currentCoordinateClicked = Tuple.Create(-1, -1);
            gameManager.boardManager.getBoard().setTile(null);
        }

        // Tests potential destinations and adds them to the list as deemed legal (this needs to be refactored later on)
        private void testPotentialDestinations(Tile currentTile)
        {
            int x = currentTile.x, y = currentTile.y;
            Piece debatedPiece = currentTile.getCurrentPiece();
            if (debatedPiece is Pawn || debatedPiece is King || debatedPiece is Knight) //Simple moving pieces (PAWN will be worked with later)
                foreach (Tuple<int, int> coordinate in debatedPiece.getListOfGeneralMoves())
                {
                    int newX = x + coordinate.Item1, newY = y + coordinate.Item2;
                    if (gameManager.boardManager.testPotentialSimplexDestination(debatedPiece, newX, newY))
                        drawPotentialDestinations(newX, newY, currentTile);
                }
            else
                foreach (Tuple<int, int> coordinate in debatedPiece.getListOfGeneralMoves()) //More complex moving pieces
                {
                    bool foundFirstCollision = false;
                    int newX = x + coordinate.Item1, newY = y + coordinate.Item2;
                    while (!foundFirstCollision)
                    {
                        if (gameManager.boardManager.testPotentialComplexDestination(debatedPiece, newX, newY) == 1)
                        {
                            drawPotentialDestinations(newX, newY, currentTile);
                            newX += coordinate.Item1;
                            newY += coordinate.Item2;
                        }
                        else if (gameManager.boardManager.testPotentialComplexDestination(debatedPiece, newX, newY) == 0)
                        {
                            drawPotentialDestinations(newX, newY, currentTile);
                            foundFirstCollision = true;
                        }
                        else
                            foundFirstCollision = true;
                    }
                }
        }

        // Resizes images to a specified size (currently not used)
        private Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        #endregion 
    }
}
