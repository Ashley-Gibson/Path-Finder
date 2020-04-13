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
