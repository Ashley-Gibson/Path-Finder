namespace Path_Finder
{
    public static class PathFinder
    {
        public enum Direction
        {
            Left = 0,
            Right = 1,
            Up = 2,
            Down = 3
        }

        public static void FindPath(Grid grid)
        {
            grid.UpdateGrid(Direction.Right);
        }
    }
}
