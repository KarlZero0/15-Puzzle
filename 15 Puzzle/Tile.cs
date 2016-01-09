using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _15_Puzzle
{
    class Tile
    {
        private Sprite sprite;
        private int num;
        private int row;
        private int col;

        public Tile(string imageNumber, ContentManager Content)
        {
            string directory = "graphics/tile_";
            sprite = new Sprite(directory + imageNumber, Content);
        }

        public void SetPos(Board board)
        {
            sprite.X = board.X + sprite.W * col;
            sprite.Y = board.Y + sprite.H * row; 
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
