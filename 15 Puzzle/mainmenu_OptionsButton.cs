using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _15_Puzzle
{
    class mainmenu_OptionsButton : Button
    {
        //Passes parameters up the chain
        public mainmenu_OptionsButton(string name, ContentManager content) : base(name, content)
        {
            
        }

        public void Click(GameLoop game)
        {
            game.EGameState = GameLoop.EGameStates.options;
        }
    }
}
