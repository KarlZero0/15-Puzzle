using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _15_Puzzle
{
    class Shuffle
    {
        private Random random = new Random();

        public void ShuffleArray(Tile[] array)
        {
            Tile tmp;
            if (array.Length > 1)
            {
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    tmp = array[i];
                    int randomIndex = random.Next(i + 1);

                    //Swap elements
                    array[i] = array[randomIndex];
                    array[randomIndex] = tmp;
                }
            }
        }
    }
}
