using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _15_Puzzle
{
    class Shuffle
    {
        //Instantiates random class
        private Random random = new Random();

        //Randomises tile placement
        public void ShuffleArray(Tile[] array)
        {
            //Temporary variable for storing tiles
            Tile tmp;

            //Checks there are more than 1 tiles left to iterate through
            if (array.Length > 1)
            {
                //Loops backwards through the array of tiles
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    //Stores tile at current index in the temporary variable
                    tmp = array[i];

                    //Selects a random index out of the tiles that have yet to be moved
                    int randomIndex = random.Next(i + 1);

                    //Replaces tile at current index with the randomly selected tile
                    array[i] = array[randomIndex];

                    //Places previously stored tile in new position
                    array[randomIndex] = tmp;
                }
            }
        }
    }
}
