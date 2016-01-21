using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _15_Puzzle
{
    class mainmenu_QuitButton : Button
    {
        //Passes parameters up the chain
        public mainmenu_QuitButton(string name, ContentManager content) : base(name, content)
        {

        }

        public void Click(GameLoop game)
        {
            game.Exit();
        }
    }
}
