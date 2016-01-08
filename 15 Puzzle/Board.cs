using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _15_Puzzle
{
    class Board
    {
        private ContentManager content;
        private int numTiles = 16;
        private Sprite background;
        //private string directory = "graphics/tile_";
        Tile[] array;

        public Board(ContentManager Content)
        {
            content = Content;
            background = new Sprite("graphics/background", 0, 0, content);
            array = new Tile[numTiles];
            CreateBoard();
        }

        public void CreateBoard()
        {
            string directory = "graphics/tile_";
            int rowCounter = 0;
            int colCounter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                {
                    string tileNum = (i + 1).ToString();
                    array[i] = new Tile(directory + tileNum, content);
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

                    array[i].SetPos();
                }
                else
                {
                    array[i] = null;
                }
            }
        }

        public void DrawBoard(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background.Img, new Rectangle(background.X, background.Y, background.W, background.H), Color.White);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    Sprite sprite = array[i].Sprite;
                    spriteBatch.Draw(sprite.Img, new Rectangle(sprite.X, sprite.Y, sprite.W, sprite.H), Color.White);
                }
            }
            spriteBatch.End();
        }

        public void GetPositions()
        {
            Tile[] tempArray = array;
            for (int i = 0; i < array.Length; i++)
            {
                int indexPos = (array[i].Row * 4) + array[i].Col - 1;
                tempArray[indexPos] = array[i];
            }
            array = tempArray;
        }

        public void Click(int x, int y)
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

            for (int i = 0; i < 4; i++)
            {
                if (i * height <= y && i * height + height >= y)
                {
                    row = i;
                    for (int j = 0; j < 4; j++)
                    {
                        if (j * width <= x && j * width + width >= x)
                        {
                            col = j;
                            int indexPos = (row * 4) + col;
                            CheckMove(indexPos, row, col);
                            //MoveTile(indexPos, row, col);
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
            array[movingToIndex].SetPos();
        }
    }
}
