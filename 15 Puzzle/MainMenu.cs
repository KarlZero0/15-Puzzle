using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _15_Puzzle
{
    class MainMenu
    {
        private mainmenu_StartButton start;
        private mainmenu_OptionsButton options;
        private mainmenu_QuitButton quit;
        private Sprite logo;
        private Sprite crown1;
        private Sprite crown2;
        private Sprite crownBack1;
        private Sprite crownBack2;

        private float transparency = 0.0f;
        private string transFade = "in";

        public MainMenu(ContentManager content, int screenWidth, int screenHeight)
        {
            start = new mainmenu_StartButton("menuStart", content);
            options = new mainmenu_OptionsButton("menuOptions", content);
            quit = new mainmenu_QuitButton("menuQuit", content);
            logo = new Sprite("graphics/logo", content);
            crown1 = new Sprite("graphics/crown", content);
            crown2 = new Sprite("graphics/crown", content);
            crownBack1 = new Sprite("graphics/crownBack", content);
            crownBack2 = new Sprite("graphics/crownBack", content);

            start.SetPos(screenWidth, screenHeight, 0);
            options.SetPos(screenWidth, screenHeight, 1);
            quit.SetPos(screenWidth, screenHeight, 2);
            logo.SetPos(screenWidth / 2 - logo.W / 2, screenHeight / 4 - logo.H);
            crown1.SetPos(start.X - crown1.W * 2, (start.Y + start.H / 2) - crown1.H / 2);
            crown2.SetPos(start.X + start.W + crown1.W, (start.Y + start.H / 2) - crown1.H / 2);
            crownBack1.SetPos(crown1.X, crown1.Y);
            crownBack2.SetPos(crown2.X, crown2.Y);
        }

        public void CheckClick(int x, int y, GameLoop game)
        {
            if (x >= start.X && x <= start.X + start.W && y >= start.Y && y <= start.Y + start.H)
            {
                start.Click(game);
            }
            else if (x >= options.X && x <= options.X + options.W && y >= options.Y && y <= options.Y + options.H)
            {
                options.Click(game);
            }
            else if (x >= quit.X && x <= quit.X + quit.W && y >= quit.Y && y <= quit.Y + quit.H)
            {
                quit.Click(game);
            }
        }

        public void Update()
        {
            MouseState mState = Mouse.GetState();
            int x = mState.X;
            int y = mState.Y;

            if (x >= start.X && x <= start.X + start.W && y >= start.Y && y <= start.Y + start.H)
            {
                start.Selected = true;
                crown1.SetPos(start.X - crown1.W * 2, (start.Y + start.H / 2) - crown1.H / 2);
                crown2.SetPos(start.X + start.W + crown1.W, (start.Y + start.H / 2) - crown1.H / 2);
                crownBack1.SetPos(crown1.X, crown1.Y);
                crownBack2.SetPos(crown2.X, crown2.Y);
            }
            else if (x >= options.X && x <= options.X + options.W && y >= options.Y && y <= options.Y + options.H)
            {
                options.Selected = true;
                crown1.SetPos(options.X - crown1.W * 2, (options.Y + options.H / 2) - crown1.H / 2);
                crown2.SetPos(options.X + options.W + crown1.W, (options.Y + options.H / 2) - crown1.H / 2);
                crownBack1.SetPos(crown1.X, crown1.Y);
                crownBack2.SetPos(crown2.X, crown2.Y);
            }
            else if (x >= quit.X && x <= quit.X + quit.W && y >= quit.Y && y <= quit.Y + quit.H)
            {
                quit.Selected = true;
                crown1.SetPos(quit.X - crown1.W * 2, (quit.Y + quit.H / 2) - crown1.H / 2);
                crown2.SetPos(quit.X + quit.W + crown1.W, (quit.Y + quit.H / 2) - crown1.H / 2);
                crownBack1.SetPos(crown1.X, crown1.Y);
                crownBack2.SetPos(crown2.X, crown2.Y);
            }
            else
            {
                start.Selected = false;
                options.Selected = false;
                quit.Selected = false;
                //crown1.SetPos(-500, -500);
                //crown2.SetPos(-500, -500);
            }

            if (transFade == "in")
            {
                transparency += 0.03f;

                if (transparency >= 1)
                {
                    transFade = "out";
                }
            }
            else if (transFade == "out")
            {
                transparency -= 0.03f;

                if (transparency <= 0)
                {
                    transFade = "in";
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(blendState: BlendState.NonPremultiplied);
            spriteBatch.Draw(start.Img, new Rectangle(start.X, start.Y, start.W, start.H), Color.White);
            spriteBatch.Draw(options.Img, new Rectangle(options.X, options.Y, options.W, options.H), Color.White);
            spriteBatch.Draw(quit.Img, new Rectangle(quit.X, quit.Y, quit.W, quit.H), Color.White);
            spriteBatch.Draw(logo.Img, new Rectangle(logo.X, logo.Y, logo.W, logo.H), Color.White);
            spriteBatch.Draw(crownBack1.Img, new Rectangle(crownBack1.X, crownBack1.Y, crownBack1.W, crownBack1.H), Color.White);
            spriteBatch.Draw(crownBack2.Img, new Rectangle(crownBack2.X, crownBack2.Y, crownBack2.W, crownBack2.H), Color.White);
            spriteBatch.Draw(crown1.Img, new Rectangle(crown1.X, crown1.Y, crown1.W, crown1.H), new Color(255, 255, 255, transparency));
            spriteBatch.Draw(crown2.Img, new Rectangle(crown2.X, crown2.Y, crown2.W, crown2.H), new Color(255, 255, 255, transparency));
            spriteBatch.End();
        }
    }
}
