using Microsoft.Xna.Framework.Content;

namespace _15_Puzzle
{
    class CheckButton : Button
    {
        public CheckButton(string name, ContentManager content) : base(name, content)
        {

        }

        public void Click(Board board)
        {
            board.CheckWin();
        }
    }
}
