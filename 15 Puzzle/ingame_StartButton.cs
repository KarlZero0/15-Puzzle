using Microsoft.Xna.Framework.Content;

namespace _15_Puzzle
{
    class ingame_StartButton : Button
    {
        //Passes parameters up the chain
        public ingame_StartButton(string name, ContentManager content) : base(name, content)
        {

        }

        //Click event for starting the game
        public void Click(GameLoop game, Tile[] array, Board board)
        {
            //Instatiates class and randomises the tile placement
            Shuffle shuffle = new Shuffle();
            shuffle.ShuffleArray(array);

            //Creates variables
            int rowCounter  = 0;
            int colCounter  = 0;
            Tile index;

            //Loops through array of tiles
            for (int i = 0; i < array.Length; i++)
            {
                //Checks if the current tile is empty and it isn't placed in the bottom right
                if (array[i] == null && i != array.Length - 1)
                {
                    //Stores tile currently in bottom right
                    index = array[array.Length - 1];

                    //Swaps it with empty tile
                    array[array.Length - 1] = null;
                    array[i]                = index;

                }
            }

            //Loops through array of tiles
            for (int i = 0; i < array.Length; i++)
            {
                //Checks counter hasn't reached the empty tile
                if (i < array.Length - 1)
                {
                    //Assigns tile's column and row
                    array[i].Col = colCounter;
                    array[i].Row = rowCounter;

                    //Increment column counter
                    colCounter++;

                    //If the end of the row has been reached, reset column to zero and move to next row
                    if (colCounter >= 4)
                    {
                        colCounter = 0;
                        rowCounter++;
                    }

                    //Sets current tile's x and y position based on row and column
                    array[i].SetPos(board);
                }
            }

            //Tracks that the game is now in progress
            game.GameStarted = true;
        }
    }
}
