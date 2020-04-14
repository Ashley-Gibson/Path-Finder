// Inspired by A* Pathfinding Algorithm - https://youtu.be/aKYlikFAV4k
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Path_Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            grid.SetupGrid(true);

            Console.Write("\n\nPress any key to find the optimal path...");
            Console.ReadKey();

            var timer = new Stopwatch();
            timer.Start();
            List<Spot> optimalPath = PathFinder.FindPath(grid);
            timer.Stop();

            grid.DisplayPath(optimalPath);

            TimeSpan timeTaken = timer.Elapsed;
            string timeTakenString = "Time taken: " + timeTaken.ToString(@"ss\.ffff");

            string output = grid.solutionFound ? $"\n\n{timeTakenString} secs\n\nPress any key to end..." : $"\n\n{timeTakenString} secs \n\nNO SOLUTIONS FOUND!\n\nPress any key to end...";
            Console.Write(output);
            Console.ReadKey();
        }
    }
}
