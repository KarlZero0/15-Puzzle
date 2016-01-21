using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace _15_Puzzle
{
    class Board
    {
        //Declares variables
        private ContentManager content;
        private SpriteBatch spriteBatch;
        private int numTiles = 16;
        private Sprite background;
        private Board self;

        private bool tileMoving = false;

        private int width;
        private int height;
        private int x;
        private int y;

        private Tile[] array;
        private Tile[] winPattern;
        private Tile toMove;
        private Tile movingTo;

        private ingame_StartButton start;
        //private CheckButton check;

        //Constructor taking in current screen size
        public Board(ContentManager Content, SpriteBatch sprBatch, int screenWidth, int screenHeight)
        {
            //Get's GameLoop's ContentManager
            content = Content;

            spriteBatch = sprBatch;

            //Creates a variable to pass into parameters
            self = this;

            //Creates buttons
            start = new ingame_StartButton("gameStart", content);
            //check = new CheckButton("check", content);

            //Sets background image and centers the board based on the screen
            background = new Sprite("graphics/background", content);
            int bgx = screenWidth / 2 - background.W / 2;
            int bgY = screenHeight / 2 - background.H / 2 - start.H / 2;
            background.SetPos(bgx, bgY);

            //Locally stores properties of the background image
            width = background.W;
            height = background.H;
            x = background.X;
            y = background.Y;

            //Creates an empty array for the tiles
            array = new Tile[numTiles];
            //Creates another empty array to compare against
            winPattern = new Tile[numTiles];

            toMove = new Tile("1", content);
            movingTo = new Tile("1", content);

            //Creates the board
            CreateBoard();
        }

        //Create public properties
        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        public bool TileMoving
        {
            get
            {
                return tileMoving;
            }
            set
            {
                tileMoving = value;
            }
        }

        public Tile[] TileArray
        {
            get
            {
                return array;
            }
        }

        public Tile ToMove
        {
            get
            {
                return toMove;
            }
        }

        public Tile MovingTo
        {
            get
            {
                return movingTo;
            }
        }

        //Creates the game board
        public void CreateBoard()
        {
            //Counters for current position
            int rowCounter = 0;
            int colCounter = 0;

            //Loops through array of tiles
            for (int i = 0; i < array.Length; i++)
            {
                //If the counter hasn't reached the last index
                if (i < array.Length - 1)
                {
                    //Stores number of tile (1-based) for image name
                    string tileNum = (i + 1).ToString();
                    //Creates a new tile using the string
                    array[i] = new Tile(tileNum, content);

                    //Sets array's properties for position
                    array[i].Num = i;
                    array[i].Col = colCounter;
                    array[i].Row = rowCounter;

                    //Increments the column counter
                    colCounter++;

                    //Checks if the end of the row has been reached
                    if (colCounter >= 4)
                    {
                        //Resets column back to 0 and moves onto the next row
                        colCounter = 0;
                        rowCounter++;
                    }

                    //Sets the tile's X and Y position based relative to the board
                    array[i].SetPos(self);
                }
                //Once the last indes has been reached
                else
                {
                    //Sets bottom right tile as empty
                    array[i] = null;
                }
            }

            //Copies the array to be used to compare against for the win condition
            Array.Copy(array, winPattern, numTiles);

            //Set's the positions of the buttons relative to the board
            start.SetPos(this, "middle");
            //check.SetPos(this, "right");
        }

        //Draws the board
        public void DrawBoard()
        {
            spriteBatch.Begin();
            //Draws the background image
            spriteBatch.Draw(background.Img, new Rectangle(background.X, background.Y, background.W, background.H), Color.White);

            //Loops through tiles
            for (int i = 0; i < array.Length; i++)
            {
                //If the square isn't empty
                if (array[i] != null)
                {
                    //Shorthand
                    Tile tile = array[i];
                    //Draws the tile
                    spriteBatch.Draw(tile.Img, new Rectangle(tile.X, tile.Y, tile.W, tile.H), Color.White);
                }
            }

            //Draws the buttons
            spriteBatch.Draw(start.Img, new Rectangle(start.X, start.Y, start.W, start.H), Color.White);
            //spriteBatch.Draw(check.Img, new Rectangle(check.X, check.Y, check.W, check.H), Color.White);
            spriteBatch.End();
        }

        //Checks if a button has been pressed
        public bool ButtonClick(int x, int y, GameLoop game)
        {
            //Takes sizes and positions of buttons
            int height = start.H;
            int width = start.W;
            int startX = start.X;
            int startY = start.Y;

            //If the click coordinates are within the start button
            if (x >= startX && x <= startX + width && y >= startY && y <= startY + height)
            {
                //Call function and return bool
                start.Click(game, array, self);
                return true;
            }

            //No button was clicked
            return false;
        }

        //Checks if one of the tiles has been pressed
        public void TileClick(int x, int y)
        {
            //Temporary variable
            Tile tmp;

            //Checks if the empty tile isn't the first index
            if (array[0] != null)
            {
                //Sets the temp to the first tile
                tmp = array[0];
            }
            else
            {
                //Otherwise set the temp to the first non-empty tile
                tmp = array[1];
            }

            //Get properties from the tile
            int height = tmp.H;
            int width = tmp.W;
            int row;
            int col;
            int startX = X;
            int startY = Y;

            //Loops through the rows first to speed up check
            for (int i = 0; i < 4; i++)
            {
                //Checks if a click was on that row
                if (startY + i * height <= y && startY + i * height + height >= y)
                {
                    //Stores row for later use
                    row = i;

                    //Now loop through the columns
                    for (int j = 0; j < 4; j++)
                    {
                        //If the click was within a tile
                        if (startX + j * width <= x && startX + j * width + width >= x)
                        {
                            //Stores column
                            col = j;

                            //Gets an array index from the row and column
                            int indexPos = (row * 4) + col;
                            //Checks if the tile pressed can move
                            CheckMove(indexPos, row, col);
                        }
                    }
                }
            }
        }

        public void TileKeyboard(Keys direction)
        {
            int indexPos = 0;
            int movingToIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    movingToIndex = i;
                }
            }

            //The row and column of the empty tile
            int nullRow = 0;
            int nullCol = 0;

            //If the empty tile isn't in the first position
            if (movingToIndex != 0)
            {
                //If the tile in the previous index was the last of it's row
                if (array[movingToIndex - 1].Col == 3)
                {
                    //Increments stored row and resets column
                    nullRow = array[movingToIndex - 1].Row + 1;
                    nullCol = 0;
                }
                //If it wasn't the last of it's row
                else
                {
                    //Keeps the same row and increments the column
                    nullRow = array[movingToIndex - 1].Row;
                    nullCol = array[movingToIndex - 1].Col + 1;
                }
            }
            //If the empty tile wasn't in the first position
            else
            {
                //Stores same row and decrements column
                nullRow = array[movingToIndex + 1].Row;
                nullCol = array[movingToIndex + 1].Col - 1;
            }

            if (direction == Keys.Left && nullCol != 3)
            {
                indexPos = movingToIndex + 1;
            }
            else if (direction == Keys.Right && nullCol != 0)
            {
                indexPos = movingToIndex - 1;
            }
            else if (direction == Keys.Up && nullRow != 3)
            {
                indexPos = movingToIndex + 4;
            }
            else if (direction == Keys.Down && nullRow != 0)
            {
                indexPos = movingToIndex - 4;
            }
            else
            {
                return;
            }

            MoveTile(indexPos, movingToIndex, nullRow, nullCol);
        }

        //Checks if the tile pressed can move
        public void CheckMove(int indexPos, int row, int col)
        {
            //The index to move to
            int movingToIndex = 0;

            //Loops through array of tiles
            for (int i = 0; i < array.Length; i++)
            {
                //Sets the index to move to as the current index of the empty tile
                if (array[i] == null)
                {
                    movingToIndex = i;
                }
            }

            //The row and column of the empty tile
            int nullRow = 0;
            int nullCol = 0;

            //If the empty tile isn't in the first position
            if (movingToIndex != 0)
            {
                //If the tile in the previous index was the last of it's row
                if (array[movingToIndex - 1].Col == 3)
                {
                    //Increments stored row and resets column
                    nullRow = array[movingToIndex - 1].Row + 1;
                    nullCol = 0;
                }
                //If it wasn't the last of it's row
                else
                {
                    //Keeps the same row and increments the column
                    nullRow = array[movingToIndex - 1].Row;
                    nullCol = array[movingToIndex - 1].Col + 1;
                }
            }
            //If the empty tile wasn't in the first position
            else
            {
                //Stores same row and decrements column
                nullRow = array[movingToIndex + 1].Row;
                nullCol = array[movingToIndex + 1].Col - 1;
            }

            //If the tile clicked is in a position that is legal to move to the empty position
            if ((nullRow == row && (nullCol == col + 1 || nullCol == col - 1)) || (nullCol == col && (nullRow == row + 1 || nullRow == row - 1)))
            {
                //Swap the positions of the clicked tile and the empty tile
                MoveTile(indexPos, movingToIndex, nullRow, nullCol);
            }
        }

        //Swaps the position of two tiles
        public void MoveTile(int indexPos, int movingToIndex, int row, int col)
        {
            //Creates a temporary array for tiles
            Tile[] tempArray = new Tile[numTiles];
            //Copies current array of tiles
            Array.Copy(array, tempArray, numTiles);

            toMove.Col = array[indexPos].Col;
            toMove.Row = array[indexPos].Row;
            toMove.X = array[indexPos].X;
            toMove.Y = array[indexPos].Y;
            toMove.Num = movingToIndex;

            //Swaps positions of passed in tiles
            tempArray[movingToIndex] = array[indexPos];
            //Sets the tile's new row and column
            tempArray[movingToIndex].Row = row;
            tempArray[movingToIndex].Col = col;
            tempArray[indexPos] = null;

            movingTo.Col = col;
            movingTo.Row = row;

            tempArray[movingToIndex].SetPos(self);
            
            movingTo.X = tempArray[movingToIndex].X;
            movingTo.Y = tempArray[movingToIndex].Y;

            int distance = Math.Abs(toMove.X - movingTo.X);
            if (distance == 0)
            {
                distance = Math.Abs(toMove.Y - movingTo.Y);
            }

            toMove.MoveSpeed = distance / 10;

            tempArray[movingToIndex].X = toMove.X;
            tempArray[movingToIndex].Y = toMove.Y;
             

            //Copies new positions back to original array
            array = tempArray;
            tileMoving = true;
            //Resets the positions of the tiles relative to the board
            //array[movingToIndex].SetPos(self);
        }

        //Checks if the game has been won
        public bool CheckWin()
        {
            //Compares the elements of the current array of tiles and the array of their starting positions
            bool areEqual = array.SequenceEqual(winPattern);
            if (areEqual)
            {
                //If they are equal then the player has won
                Console.WriteLine("Win");
                return true;
            }
            return false;
        }

        /*public void placeholder()
        {
            //Creates a temporary array for tiles
            Tile[] tempArray = new Tile[1];
            int startX = array[indexPos].X;
            int startY = array[indexPos].Y;
            int startCol = array[indexPos].Col;
            int startRow = array[indexPos].Row;
            int endX;
            int endY;

            //Copies current array of tiles
            Array.Copy(array, indexPos, tempArray, 0, 1);

            array[movingToIndex] = tmp;
            array[movingToIndex].Col = col;
            array[movingToIndex].Row = row;
            array[movingToIndex].SetPos(self);

            endX = array[movingToIndex].X;
            endY = array[movingToIndex].Y;

            array[movingToIndex] = null;
            array[indexPos].Col = startCol;
            array[indexPos].Row = startRow;
            array[indexPos].SetPos(self);


            //Sets the tile's new row and column
            //Resets the positions of the tiles relative to the board

            int counter = 0;
            while (array[indexPos].X != endX || array[indexPos].Y != endY)
            {
                if (array[indexPos].X < endX && counter % 500 == 0)
                {
                    array[indexPos].X++;
                }
                else if (array[indexPos].X > endX && counter % 500 == 0)
                {
                    array[indexPos].X--;
                }

                if (array[indexPos].Y < endY && counter % 500 == 0)
                {
                    array[indexPos].Y++;
                }
                else if (array[indexPos].Y > endY && counter % 500 == 0)
                {
                    array[indexPos].Y--;
                }
                counter++;
                DrawBoard();
            }

            array[movingToIndex] = array[indexPos];
            array[movingToIndex].Col = col;
            array[movingToIndex].Row = row;
            array[indexPos] = null;
        }*/
    }
}
