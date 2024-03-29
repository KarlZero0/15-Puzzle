﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _15_Puzzle
{
    class Sprite
    {
        //Declare variables
        private int x;
        private int y;
        private int w;
        private int h;
        private Texture2D img;

        //Create get / set properties
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int W
        {
            get
            {
                return w;
            }
        }

        public int H
        {
            get
            {
                return h;
            }
        }
        public Texture2D Img
        {
            get
            {
                return img;
            }
        }

        //Constructor if X and Y pos are already known
        public Sprite(string imageFile, int xPos, int yPos, ContentManager Content)
        {
            //Assign variables
            img = Content.Load<Texture2D>(imageFile);
            x   = xPos;
            y   = yPos;
            w   = img.Width;
            h   = img.Height;
        }

        //Constructor if coordinates aren't known
        public Sprite(string imageFile, ContentManager Content)
        {
            //Assign variables
            img = Content.Load<Texture2D>(imageFile);
            w   = img.Width;
            h   = img.Height;
        }

        //Sets x and y positions from parameters
        public void SetPos(int x, int y)
        {
            X = x;
            Y = y;
        }


    }
}
