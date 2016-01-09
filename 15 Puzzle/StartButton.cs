using Microsoft.Xna.Framework.Content;

namespace _15_Puzzle
{
    class StartButton : Button
    {
        public StartButton(string name, ContentManager content) : base(name, content)
        {

        }

        public void Click(GameLoop game)
        {
            game.GameStarted = true;
        }
    }
}
