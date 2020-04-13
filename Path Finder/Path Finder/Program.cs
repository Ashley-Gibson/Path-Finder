// Inspired by A* Pathfinding Algorithm - https://youtu.be/aKYlikFAV4k and pseudocode algorithm - https://en.wikipedia.org/wiki/A*_search_algorithm

using System;

namespace Path_Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            grid.SetupGrid();
            grid.DrawGrid();

            Console.Write("\n\nPress any key to end...");
            Console.ReadKey();
        }
    }
}
