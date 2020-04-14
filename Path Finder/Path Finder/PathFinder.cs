using System;
using System.Collections.Generic;
using static Path_Finder.Grid;

namespace Path_Finder
{
    public static class PathFinder
    {
        public enum Direction
        {
            Left = 0,
            Right = 1,
            Up = 2,
            Down = 3,
            Undefined = 4
        }

        private static int CalculateHeuristic(Spot a, Spot b)
        {
            int d = Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
            return d;
        }

        public static List<Spot> FindPath(Grid grid)
        {
            List<Spot> optimalPath = new List<Spot>();
            
            grid.openSet.Add(grid.GridArray[grid.PlayerPosition.Y,grid.PlayerPosition.X]);

            while(grid.openSet.Count > 0)
            {
                int winner = 0;
                for (int i = 0; i < grid.openSet.Count; i++)
                {
                    if (grid.openSet[i].F < grid.openSet[winner].F)
                        winner = i;                      
                }

                Spot current = grid.openSet[winner];
                current.Character = PLAYER;
                grid.UpdateGridWithSpot(current);

                if (grid.openSet[winner].X == grid.end.X && grid.openSet[winner].Y == grid.end.Y)
                {
                    grid.solutionFound = true;
                    return optimalPath;
                }

                grid.openSet.Remove(current);
                grid.closedSet.Add(current);

                for (int i = 0; i < current.Neighbours.Count; i++)
                {
                    Spot neighbour = current.Neighbours[i];
                    
                    if (!grid.closedSet.Exists(c => c.X == neighbour.X && c.Y == neighbour.Y) && grid.GridArray[neighbour.Y, neighbour.X].Character != OBSTACLE)
                    {
                        int tempG = current.G + 1;

                        if (grid.openSet.Exists(o => o.X == neighbour.X && o.Y == neighbour.Y))
                        {
                            if (tempG < neighbour.G)
                                neighbour.G = tempG;
                        }
                        else
                        {
                            neighbour.G = tempG;
                            grid.openSet.Add(neighbour);
                        }

                        neighbour.H = CalculateHeuristic(neighbour, grid.end);
                        neighbour.F = neighbour.G + neighbour.H;
                        neighbour.PreviousSpot.Add(current);
                    }
                }
                optimalPath = new List<Spot>();
                Spot temp = current;
                optimalPath.Add(temp);
                while (temp.PreviousSpot.Count > 0)
                {
                    optimalPath.Add(temp.PreviousSpot[0]);
                    temp = temp.PreviousSpot[0];
                }
            }

            // Return no solution possible
            return optimalPath;
        }       
    }
}
