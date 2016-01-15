using Microsoft.Xna.Framework.Content;

namespace _15_Puzzle
{
    class CheckButton : Button
    {
        //Passes parameters up the chain
        public CheckButton(string name, ContentManager content) : base(name, content)
        {

        }

        //Click event checks if the game has been won
        public void Click(Board board)
        {
            board.CheckWin();
        }
    }
}
