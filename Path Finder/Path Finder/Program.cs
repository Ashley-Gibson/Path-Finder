// Inspired by A* Pathfinding Algorithm - https://youtu.be/aKYlikFAV4k and pseudocode algorithm - https://en.wikipedia.org/wiki/A*_search_algorithm

using System;
using System.Collections.Generic;
using static Path_Finder.Grid;

namespace Path_Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            grid.SetupGrid();

            Console.Write("\n\nPress any key to find the optimal path...");
            Console.ReadKey();            

            List<Spot> optimalPath = PathFinder.FindPath(grid);            

            grid.DisplayPath(optimalPath);

            string output = optimalPath.Count > 0 ? "\n\nPress any key to end..." : "\n\nNO SOLUTIONS FOUND!\n\nPress any key to end...";
            Console.Write(output);
            Console.ReadKey();
        }
    }
}
