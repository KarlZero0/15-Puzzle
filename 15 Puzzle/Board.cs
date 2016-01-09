using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace _15_Puzzle
{
    class Board
    {
        private ContentManager content;
        private int numTiles = 16;
        private Sprite background;
        Board self;

        private int width;
        private int height;
        private int x;
        private int y;

        Tile[] array;
        Tile[] winPattern;

        StartButton start;
        CheckButton check;

        public Board(ContentManager Content, int screenWidth, int screenHeight)
        {
            content = Content;
            self = this;

            start = new StartButton("start", content);
            check = new CheckButton("check", content);

            background = new Sprite("graphics/background", content);
            int bgx = screenWidth / 2 - background.W / 2;
            int bgY = screenHeight / 2 - background.H / 2 - start.Sprite.H / 2;
            background.SetPos(bgx, bgY);

            width = background.W;
            height = background.H;
            x = background.X;
            y = background.Y;

            array = new Tile[numTiles];
            winPattern = new Tile[numTiles];
            
            CreateBoard();
        }

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


        public void CreateBoard()
        {
            int rowCounter = 0;
            int colCounter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                {
                    string tileNum = (i + 1).ToString();
                    array[i] = new Tile( tileNum, content);
                    array[i].Num = i;
                    array[i].Col = colCounter;
                    array[i].Row = rowCounter;

                    colCounter++;
                    if (colCounter >= 4)
                    {
                        colCounter = 0;
                    }

                    if ((i + 1) % 4 == 0)
                    {
                        rowCounter++;
                    }

                    array[i].SetPos(self);
                }
                else
                {
                    array[i] = null;
                }
            }
            Array.Copy(array, winPattern, numTiles);

            start.SetPos(this, "left");
            check.SetPos(this, "right");
        }

        public void DrawBoard(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background.Img, new Rectangle(background.X, background.Y, background.W, background.H), Color.White);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    Sprite spr = array[i].Sprite;
                    spriteBatch.Draw(spr.Img, new Rectangle(spr.X, spr.Y, spr.W, spr.H), Color.White);
                }
            }
            Sprite sprite = start.Sprite; 
            spriteBatch.Draw(sprite.Img, new Rectangle(sprite.X, sprite.Y, sprite.W, sprite.H), Color.White);
            sprite = check.Sprite;
            spriteBatch.Draw(sprite.Img, new Rectangle(sprite.X, sprite.Y, sprite.W, sprite.H), Color.White);
            spriteBatch.End();
        }

        public bool ButtonClick (int x, int y, GameLoop game)
        {
            int height = start.Sprite.H;
            int width = start.Sprite.W;
            int startX = X;
            int startY = Y + Height;
            int endX = X + Width;

            if (x >= startX && x <= startX + width && y >= startY && y <= startY + height)
            {
                start.Click(game);
                return true;
            }
            else if (x <= endX && x >= endX - width && y >= startY && y <= startY + height)
            {
                check.Click(self);
                return true;
            }
            return false;
        }

        public void TileClick(int x, int y)
        {
            Tile shorthand;
            if (array[0] != null)
            {
                shorthand = array[0];
            }
            else
            {
                shorthand = array[1];
            }

            int height = shorthand.Sprite.H;
            int width = shorthand.Sprite.W;
            int row;
            int col;
            int startX = X;
            int startY = Y;

            for (int i = 0; i < 4; i++)
            {
                if (startY + i * height <= y && startY + i * height + height >= y)
                {
                    row = i;
                    for (int j = 0; j < 4; j++)
                    {
                        if (startX + j * width <= x && startX + j * width + width >= x)
                        {
                            col = j;
                            int indexPos = (row * 4) + col;
                            CheckMove(indexPos, row, col);
                        }
                    }
                }
            }
        }

        public void CheckMove(int indexPos, int row, int col)
        {
            int movingToIndex = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    movingToIndex = i;
                }
            }

            int nullRow = 0;
            int nullCol = 0;

            if (movingToIndex != 0)
            {
                if (array[movingToIndex - 1].Col == 3)
                {
                    nullRow = array[movingToIndex - 1].Row + 1;
                    nullCol = 0;
                }
                else
                {
                    nullRow = array[movingToIndex - 1].Row;
                    nullCol = array[movingToIndex - 1].Col + 1;
                }
            }
            else
            {
                nullRow = array[movingToIndex + 1].Row;
                nullCol = array[movingToIndex + 1].Col - 1;
            }    

            if ((nullRow == row && (nullCol == col + 1 || nullCol == col - 1)) || (nullCol == col && (nullRow == row + 1 || nullRow == row - 1)))
            {
                MoveTile(indexPos, movingToIndex, nullRow, nullCol);
            }
        }

        public void MoveTile(int indexPos, int movingToIndex, int row, int col)
        {
            Tile[] tempArray = new Tile[numTiles];
            Array.Copy(array, tempArray, numTiles);

            tempArray[movingToIndex] = array[indexPos];
            tempArray[movingToIndex].Row = row;
            tempArray[movingToIndex].Col = col;
            tempArray[indexPos] = null;
            array = tempArray;
            array[movingToIndex].SetPos(self);
        }

        public bool CheckWin()
        {
            bool areEqual = array.SequenceEqual(winPattern);
            if (areEqual)
            {
                Console.WriteLine("Win");
                return true;
            }
            return false;
        }
    }
}
