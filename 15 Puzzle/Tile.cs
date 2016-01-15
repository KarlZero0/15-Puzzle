using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _15_Puzzle
{
    class Tile : Sprite
    {
        //Declares variables
        private int num;
        private int row;
        private int col;

        private float moveSpeed = 0;

        //String prefix for all tile images
        private static string directory = "graphics/tile_";

        //Constructor taking in image name and adding it to the prefix before passing it to it's parent
        public Tile(string imageNumber, ContentManager Content) : base(directory + imageNumber, Content)
        {
            
        }

        //Set's the tile's position based on it's row and column relative to the board
        public void SetPos(Board board)
        {
            X = board.X + W * col;
            Y = board.Y + H * row; 
        }

        public void MovingPos(Tile toMove, Tile movingTo, Board board)
        {
            if (toMove.X < movingTo.X)
            {
                X += (int)toMove.moveSpeed;
            }
            else if (toMove.X > movingTo.X)
            {
                X -= (int)toMove.moveSpeed;
            }
            else if (toMove.Y < movingTo.Y)
            {
                Y += (int)toMove.moveSpeed;
            }
            else if (toMove.Y > movingTo.Y)
            {
                Y -= (int)toMove.moveSpeed;
            }

            if (Math.Abs(toMove.X - movingTo.X) <= 5 && Math.Abs(toMove.Y - movingTo.Y) <= 5)
            {
                X = movingTo.X;
                Y = movingTo.Y;
                board.TileMoving = false;
            }

            board.ToMove.X = X;
            board.ToMove.Y = Y;
        }

        //Public properties
        public int Num
        {
            get
            {
                return num;
            }
            set
            {
                num = value;
            }
        }

        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }
        
        public int Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }

        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            set
            {
                moveSpeed = value;
            }
        }
    }
}
