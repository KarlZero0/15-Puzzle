using Microsoft.Xna.Framework.Content;

namespace _15_Puzzle
{
    class StartButton : Button
    {
        public StartButton(string name, ContentManager content) : base(name, content)
        {

        }

        public void Click(GameLoop game, Tile[] array, Board board)
        {
            Shuffle shuffle = new Shuffle();
            shuffle.ShuffleArray(array);

            int rowCounter = 0;
            int colCounter = 0;
            Tile index;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null && i != array.Length - 1)
                {
                    index = array[array.Length - 1];
                    array[array.Length - 1] = null;
                    array[i] = index;

                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                {
                    array[i].Col = colCounter;
                    array[i].Row = rowCounter;

                    colCounter++;
                    if (colCounter >= 4)
                    {
                        colCounter = 0;
                    }

                    if ((i + 1) % 4 == 0)
                    {
                        rowCounter++;
                    }

                    array[i].SetPos(board);
                }
            }
            game.GameStarted = true;
        }
    }
}
