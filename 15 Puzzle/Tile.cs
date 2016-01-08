using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _15_Puzzle
{
    class Tile
    {
        protected Sprite sprite;
        private int num;
        private int row;
        private int col;

        public Tile(string imageFile, ContentManager Content)
        {
            sprite = new Sprite(imageFile, Content);
        }

        public void SetPos()
        {
            sprite.X = sprite.W * col;
            sprite.Y = sprite.H * row; 
        }

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

        public Sprite Sprite
        {
            get
            {
                return sprite;
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
    }
}
