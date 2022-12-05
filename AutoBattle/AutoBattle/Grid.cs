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
                for (int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(i, j, false);
                    grids.Add(newBox);
                }
            }
        }
        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(count);
                        count++;
                    }
                    GridBox currentgrid = syncGrid(grids,i,j);
                    if (currentgrid.ocupied)
                    {
                        //For player and enemy differentiation
                        //FEATURE: symbols can be changed or customized by the player
                        if (currentgrid.isOwner)
                        {
                            Console.Write("[X]\t");
                        }
                        else
                        {
                            Console.Write("[O]\t");
                        }
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                        //change commented line for tests with grid with better visualization
                        //Console.Write($"[" + currentgrid.Index + "]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            for(int i = 0; i < Columns; i++)
            {
                Console.Write("  " + alphabet[i] + $"\t");
            }
            Console.Write(Environment.NewLine + Environment.NewLine);;
        }
        GridBox syncGrid(List<GridBox> grids, int i, int j)// i and j respective are x and y
        {
            string result = i.ToString() + j.ToString();
            int resultI = Int32.Parse(result);
            
            for (int k = 0; k < grids.Count; k++)
            {
                if (grids[k].Index == resultI)
                {
                    return grids[k];
                }
            }   
            return grids[0];
        }
    }
}
