using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System;

namespace _15_Puzzle
{
    class Input
    {
        private MouseState oldState;

        public void Update(GameLoop game, Board board)
        {
            MouseState newState = Mouse.GetState();

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                int x = newState.X;
                int y = newState.Y;
                board.Click(x, y);
            }

            oldState = newState;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
               game.Exit();
            }            
        }
    }
}
