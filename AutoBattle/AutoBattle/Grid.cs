using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLenght;
        public int yLength;

        //used for board side markings
        char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
        int count = 0;
        
        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                GridBox newBox = new GridBox(i,0,false);
                grids.Add(newBox);

                for (int j = 0; j < Columns; j++)
                {
                    new GridBox(i, j, false);
                    grids.Add(newBox);
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield(int Lines, int Columns)
        {
            Console.WriteLine();

            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(count);
                        count++;
                    }
                    GridBox currentgrid = new GridBox();
                    if (currentgrid.ocupied)
                    {
                        //if()
                        Console.Write("[X]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            for(int i = 0; i < count; i++)
            {
                Console.Write("  " + alphabet[i] + $"\t");
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }
}
