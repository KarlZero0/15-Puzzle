using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _15_Puzzle
{
    class Button
    {
        private Sprite sprite;

        public Button(string name, ContentManager Content)
        {
            string directory = "graphics/button_";
            sprite = new Sprite(directory + name, Content);
        }

        public void SetPos(Board board, string alignment)
        {
            switch (alignment)
            {
                case "left":
                    sprite.X = board.X;
                    break;
                case "right":
                    sprite.X = board.X + board.Width - sprite.W;
                    break;
                case "middle":
                    sprite.X = board.X + board.Width / 2 - sprite.W / 2;
                    break;
            }
            sprite.Y = board.Y + board.Height;
        }

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }
        }
    }
}
