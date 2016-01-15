using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _15_Puzzle
{
    class Button : Sprite
    {
        //String prefix for all button images
        private static string directory = "graphics/button_";

        //Constructor taking in the image name and adding the prefix to it before passing it to it's parent
        public Button(string name, ContentManager Content) : base(directory + name, Content)
        {

        }

        //Sets the button's X and Y positions to align to a part of the board
        public void SetPos(Board board, string alignment)
        {
            switch (alignment)
            {
                case "left":
                    X = board.X;
                    break;
                case "right":
                    X = board.X + board.Width - W;
                    break;
                case "middle":
                    X = board.X + board.Width / 2 - W / 2;
                    break;
            }

            Y         = board.Y + board.Height;
        }
    }
}
