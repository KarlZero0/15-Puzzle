using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System;

namespace _15_Puzzle
{
    class Input
    {
        //Stores last state of the mouse
        private MouseState oldMState;
        private KeyboardState oldKState;
        private Keys[] kArray;

        //Checks for input every tick
        public void Update(GameLoop game, Board board)
        {
            //Gets the current state of the mouse
            MouseState newMState = Mouse.GetState();
            KeyboardState newKState = Keyboard.GetState();
            kArray = oldKState.GetPressedKeys();
            Keys pressedKey = Keys.A;
            if (kArray.Length > 0)
            {
                pressedKey = kArray[kArray.Length - 1];
            }

            //Checks if the mouse has just been clicked by comparing the current state with the previous state
            if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
            {
                //Logs click coordinates
                int x = newMState.X;
                int y = newMState.Y;

                //Checks if a button has been pressed before checking if a tile was clicked
                if (board.ButtonClick(x, y, game) == false)
                {
                    board.TileClick(x, y);
                    board.CheckWin();
                }
            }

            //Resets old mouse state
            oldMState = newMState;

            //Checks if keys have been pressed to end the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
               game.Exit();
            }

            if ((newKState.IsKeyUp(Keys.Left) && oldKState.IsKeyDown(Keys.Left)) || (newKState.IsKeyUp(Keys.Right) && oldKState.IsKeyDown(Keys.Right)) || (newKState.IsKeyUp(Keys.Down) && oldKState.IsKeyDown(Keys.Down)) || (newKState.IsKeyUp(Keys.Up) && oldKState.IsKeyDown(Keys.Up)))
            {
                board.TileKeyboard(pressedKey);
            }

            oldKState = newKState;
        }

        public void Update(GameLoop game, MainMenu mainMenu)
        {
            //Gets the current state of the mouse
            MouseState newMState = Mouse.GetState();
            KeyboardState newKState = Keyboard.GetState();
            kArray = oldKState.GetPressedKeys();
            Keys pressedKey = Keys.A;
            if (kArray.Length > 0)
            {
                pressedKey = kArray[kArray.Length - 1];
            }

            //Checks if the mouse has just been clicked by comparing the current state with the previous state
            if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
            {
                //Logs click coordinates
                int x = newMState.X;
                int y = newMState.Y;
                mainMenu.CheckClick(x, y, game);
            }

            //Resets old mouse state
            oldMState = newMState;

            //Checks if keys have been pressed to end the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }

            if ((newKState.IsKeyUp(Keys.Left) && oldKState.IsKeyDown(Keys.Left)) || (newKState.IsKeyUp(Keys.Right) && oldKState.IsKeyDown(Keys.Right)) || (newKState.IsKeyUp(Keys.Down) && oldKState.IsKeyDown(Keys.Down)) || (newKState.IsKeyUp(Keys.Up) && oldKState.IsKeyDown(Keys.Up)))
            {
                
            }

            oldKState = newKState;
        }
    }
}
