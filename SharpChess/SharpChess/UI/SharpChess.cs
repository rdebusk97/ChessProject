﻿using System;
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

        // Should probably find a better way to get to the directory than this, but it's a work-around...
        private string imagesDirectory = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName
            .Substring(0, Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName.Length - DEBUG_DIRECTORY_OFFSET); 

        private Tuple<int, int> currentCoordinateClicked = new Tuple<int, int>(8, 8);
        private List<Tuple<int, int>> potentialCoordinates = new List<Tuple<int, int>>();

        private int boardSize;
        private MoveState moveState = MoveState.NO_SELECTION;

        private SolidBrush tanBrush = new SolidBrush(Color.Tan);
        private SolidBrush beigeBrush = new SolidBrush(Color.Beige);
        private SolidBrush khakiBrush = new SolidBrush(Color.Khaki);
        private SolidBrush redBrush = new SolidBrush(Color.Crimson);

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
            Graphics tempGraphics = Graphics.FromImage(tempBitMap);
            int counter = 0, tileSize = boardPanel.Height / boardSize;
            for (int i = 0; i < boardSize; i++)  
            {
                for (int j = 0; j < boardSize; j++)
                {
                    int xPixel = i * tileSize, yPixel = j * tileSize;
                    if (counter % BOARD_COLORS == 0)
                        tempGraphics.FillRectangle(tanBrush, xPixel, yPixel, tileSize, tileSize);
                    else
                        tempGraphics.FillRectangle(beigeBrush, xPixel, yPixel, tileSize, tileSize);
                    counter++;
                }
                counter++;
            }
            tempGraphics.DrawImage(tempBitMap, 0, 0);
            return tempBitMap;
        }

        // Draws a piece onto the board given an x and y coordinate
        private Bitmap drawPiece(Piece piece, int x, int y)
        {
            Image pieceImage = Image.FromFile(imagesDirectory + piece.toImage());
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = boardPanel.Height / boardSize;
            int xPixel = tileSize * x, yPixel = tileSize * (boardSize - 1 - y);
            graphics.DrawImage(pieceImage, xPixel, yPixel, tileSize, tileSize);
            return boardMap;
        }

        // Draws a border around a tile at an x and y coordinate
        private Bitmap drawBorder(int x, int y, SolidBrush s)
        {
            int penThickness = 2, tileSize = (boardPanel.Height / boardSize);
            int xCoordinate = tileSize * x, yCoordinate = tileSize * (boardSize - 1 - y);
            float ratio = .9f;
            Pen p = new Pen(Color.Black, penThickness);
            Graphics graphics = Graphics.FromImage(boardMap);
            graphics.FillRectangle(s, xCoordinate, yCoordinate, tileSize, tileSize);
            drawBackPlacedPiece(x, y);
            graphics.DrawLine(p, xCoordinate, yCoordinate + 1, xCoordinate + (tileSize * ratio), yCoordinate + 1);
            graphics.DrawLine(p, xCoordinate + 1, yCoordinate, xCoordinate + 1, yCoordinate + (tileSize * ratio));
            return boardMap;
        }

        // Draws new tile square over existing (as to reset the tile)
        private Bitmap drawSquare(int x, int y)
        {
            Graphics graphics = Graphics.FromImage(boardMap);
            int tileSize = (boardPanel.Height / boardSize);
            int xCoordinate = tileSize * x, yCoordinate = tileSize * (boardSize - 1 - y);
            if (y % BOARD_COLORS == x % BOARD_COLORS)
                graphics.FillRectangle(beigeBrush, xCoordinate, yCoordinate, tileSize, tileSize);
            else
                graphics.FillRectangle(tanBrush, xCoordinate, yCoordinate, tileSize, tileSize);
            return boardMap;
        }

        // Draws piece onto the board at a specified x and y coordinate, assuming there is a piece there
        private void drawBackPlacedPiece(int x, int y)
        {
            if (gameManager.boardManager.findTile(x, y) != null)
            {
                Tile t = gameManager.boardManager.findTile(x, y);
                if (t.x == x && t.y == y)
                    if (t.hasPlacedPiece())
                        drawPiece(t.getCurrentPiece(), x, y);
            }
        }

        #endregion

        #region -- Board Event Handlers

        // Clicking tiles, showing potential moves and information
        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = boardPanel.PointToClient(Cursor.Position);
            int tileSize = boardPanel.Height / boardSize;
            int x = point.X / tileSize, y = boardSize - 1 - (point.Y / tileSize);
            Tile endTile = gameManager.boardManager.findTile(x, y);
            if (moveState == MoveState.SELECTED_START && checkPotentialDestinationList(x, y))
            {
                Tile startTile = gameManager.boardManager.findTile(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2);
                checkSpecialMoves(startTile, endTile);
                executeMove(startTile, endTile);
                printMove();
            }
            else if (endTile.hasPlacedPiece() && endTile.getCurrentPiece().getAllegiance() == gameManager.getTurn()) // Remove second condition to allow free piece movement
            {
                if (x != currentCoordinateClicked.Item1 || y != currentCoordinateClicked.Item2)
                    newlyClickedCoordinate(endTile);
                else
                    closingCurrentCoordinate(endTile);
                displayUpdatedTileLabels(x, y); //Can eventually get rid of
            }
            boardPanel.Refresh();
        }

        // Paints new board
        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(boardMap, 0, 0);
        }

        // Sets a new game
        private void newGame_btn_Click(object sender, EventArgs e)
        {
            gameManager.boardManager.getBoard().resetBoard();
            boardMap = drawBoard();
            drawInitialPieces();
            gameManager.reset();
            boardPanel.Refresh();
            moveHistory_txtBox.Clear();
        }

        #endregion

        #region -- Form Updates

        // Displays current tile and piece clicked
        private void displayUpdatedTileLabels(int x, int y)
        {
            tileCoordinate_lbl.Text = "Tile Coordinate: [" + x.ToString() + "," + y.ToString() + "]";
            currentPiece_lbl.Text = "Piece: " + gameManager.boardManager.findTile(x, y).getCurrentPiece().getAllegiance() + 
                "_" + gameManager.boardManager.findTile(x, y).getCurrentPiece().ToString();
        }

        // Prints the move to the history log
        private void printMove()
        {
            Move lastMove = gameManager.moveManager.getRecentMove();
            if (gameManager.getMovesPlayed() > 1)
                moveHistory_txtBox.AppendText('\n' + lastMove.ToString());
            else
                moveHistory_txtBox.AppendText(lastMove.ToString());
            undo_btn.Enabled = true;
        }

        #endregion

        #region -- Helper Methods

        private void checkSpecialMoves(Tile startTile, Tile endTile)
        {
            Piece currentPiece = startTile.getCurrentPiece();
            if (currentPiece.toText() == 'P' && startTile.x != endTile.x)
                drawSquare(endTile.x, startTile.y);

        }
        // Draws any initial pieces that were placed on the board at startup
        private void drawInitialPieces()
        {
            foreach (Tile t in gameManager.boardManager.getBoard().getTileMap())
                if (t.hasPlacedPiece())
                    drawPiece(t.getCurrentPiece(), t.x, t.y);
        }

        // Displays new bordered coordinate and its potential moves
        private void newlyClickedCoordinate(Tile currentTile)
        {
            drawSquare(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2);
            drawBackPlacedPiece(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2);
            resetPotentialCoordinates();
            drawBorder(currentTile.x, currentTile.y, redBrush);
            drawBackPlacedPiece(currentTile.x, currentTile.y);
            currentCoordinateClicked = Tuple.Create(currentTile.x, currentTile.y);
            gameManager.boardManager.getBoard().setTile(currentTile);
            moveState = MoveState.SELECTED_START;
            testPotentialDestinations(currentTile);
        }

        // Undisplays current coordinate and opens new one
        private void closingCurrentCoordinate(Tile currentTile)
        {
            resetPotentialCoordinates();
            drawSquare(currentTile.x, currentTile.y);
            drawBackPlacedPiece(currentTile.x, currentTile.y);
            currentCoordinateClicked = Tuple.Create(-1, -1);
            gameManager.boardManager.getBoard().setTile(null);
            moveState = MoveState.NO_SELECTION;
        }

        //Preparations for the next click after a move and/or undo
        private void updateBoardInformation()
        {
            resetPotentialCoordinates();
            moveState = MoveState.NO_SELECTION;
            currentCoordinateClicked = new Tuple<int, int>(-1, -1);
            boardPanel.Refresh();
        }

        #endregion

        #region -- Move Execution & Candidacy

        //Executes a played move (by clicking and/or fast-forwarding a played game)
        private void executeMove(Tile oldTile, Tile currentTile)
        {
            gameManager.executeMove(oldTile.getCurrentPiece(), oldTile, currentTile);
            drawSquare(oldTile.x, oldTile.y);
            drawBackPlacedPiece(currentTile.x, currentTile.y);
            updateBoardInformation();
        }

        //Unexecutes a played move (by clicking and/or rewinding a played game)
        private void unexecuteMove(Tile oldTile, Tile currentTile)
        {
            gameManager.unexecuteMove();
            drawSquare(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2);
            drawBackPlacedPiece(currentCoordinateClicked.Item1, currentCoordinateClicked.Item2);
            drawSquare(currentTile.x, currentTile.y);
            drawBackPlacedPiece(oldTile.x, oldTile.y);
            if (currentTile.hasPlacedPiece())
                drawBackPlacedPiece(currentTile.x, currentTile.y);
            if (oldTile.getCurrentPiece().toText() == 'P' && currentTile.x != oldTile.x && currentTile.getCurrentPiece() == null) //EN PASSANT
                drawBackPlacedPiece(currentTile.x, oldTile.y);
            updateBoardInformation();
        }

        // Tests potential destinations given the current tile and piece
        private void testPotentialDestinations(Tile currentTile)
        {
            Piece debatedPiece = currentTile.getCurrentPiece();
            switch (debatedPiece.toText())
            {
                case 'P':
                case 'K':
                case 'N':
                    testSimplexCandidacy(currentTile, debatedPiece);
                    break;
                default:
                    testComplexCandidacy(currentTile, debatedPiece);
                    break;
            }
        }

        // Tests potential destinations for simple pieces (i.e. Pawn, King, Knight)
        private void testSimplexCandidacy(Tile currentTile, Piece debatedPiece)
        {
            foreach (Tuple<int, int> coordinate in debatedPiece.getListOfGeneralMoves())
            {
                int newX = currentTile.x + coordinate.Item1, newY = currentTile.y + coordinate.Item2;
                if (debatedPiece.toText() == 'P')
                {
                    if (gameManager.boardManager.testPotentialPawnDestination(currentTile, newX, newY))
                        drawPotentialDestinations(newX, newY, currentTile);
                }
                else
                    if (gameManager.boardManager.testPotentialSimplexDestination(debatedPiece, newX, newY))
                        drawPotentialDestinations(newX, newY, currentTile);
            }

        }

        // Tests potential destinations for complex pieces (Rook, Bishop, Queen)
        private void testComplexCandidacy(Tile currentTile, Piece debatedPiece)
        {
            int x = currentTile.x, y = currentTile.y;
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

        // Checks for legal coordinate and will draw the bordered coordinate
        private void drawPotentialDestinations(int newX, int newY, Tile currentTile)
        {
            if (0 <= newX && newX < boardSize && 0 <= newY && newY < boardSize) //Legal spot on board
            {
                potentialCoordinates.Add(Tuple.Create(newX, newY));
                if (moveState == MoveState.SELECTED_START) //
                    drawBorder(newX, newY, khakiBrush);
                else
                    testCheck();
                boardPanel.Refresh();
            }
        }

        // Resets potential coordinates
        private void resetPotentialCoordinates()
        {
            foreach (Tuple<int, int> coordinate in potentialCoordinates)
            {
                drawSquare(coordinate.Item1, coordinate.Item2);
                drawBackPlacedPiece(coordinate.Item1, coordinate.Item2);
            }
            potentialCoordinates.Clear();
        }

        private bool checkPotentialDestinationList(int x, int y)
        {
            foreach (Tuple<int, int> coordinate in potentialCoordinates)
                if (coordinate.Item1 == x && coordinate.Item2 == y)
                    return true;
            return false;
        }

        #endregion

        #region -- Endgame 

        private void testCheck()
        {
            Tuple<int, int> checkCoordinate = gameManager.testCheck(potentialCoordinates);
            if (checkCoordinate != null)
                drawBorder(checkCoordinate.Item1, checkCoordinate.Item2, redBrush);
        }
        #endregion

        private void turnbased_radioBtn_CheckedChanged(object sender, EventArgs e)
        {
            freePlay_radioBtn.Checked = false;
        }

        private void freePlay_radioBtn_CheckedChanged(object sender, EventArgs e)
        {
            turnbased_radioBtn.Checked = false;
        }

        private void undo_btn_Click(object sender, EventArgs e)
        {
            Move m = gameManager.moveManager.getRecentMove();
            unexecuteMove(m.startTile, m.endTile);
            if (gameManager.getMovesPlayed() == 0)
                moveHistory_txtBox.Clear();
            else
                moveHistory_txtBox.Text = moveHistory_txtBox.Text.Remove(moveHistory_txtBox.Text.LastIndexOf('\n'));
            undo_btn.Enabled = false;
        }
    }
}
