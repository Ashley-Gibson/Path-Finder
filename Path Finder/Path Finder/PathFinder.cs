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
                    Console.WriteLine("DONE!");
                    return optimalPath;
                }

                grid.openSet.Remove(current);
                grid.closedSet.Add(current);

                for (int i = 0; i < current.Neighbours.Count; i++)
                {
                    Spot neighbour = current.Neighbours[i];
                    
                    if (!grid.closedSet.Exists(c => c.X == neighbour.X && c.Y == neighbour.Y))
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
            return new List<Spot>();
        }       

        #region Algorithm

        /* function reconstruct_path(cameFrom, current)
             total_path := {current
            }
            while current in cameFrom.Keys:
                current := cameFrom[current]
                total_path.prepend(current)
            return total_path
            
        
        // A* finds a path from start to goal.
        // h is the heuristic function. h(n) estimates the cost to reach goal from node n.
        function A_Star(start, goal, h)
            // The set of discovered nodes that may need to be (re-)expanded.
            // Initially, only the start node is known.
            // This is usually implemented as a min-heap or priority queue rather than a hash-set.
            openSet := {start
        }

        // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from start
        // to n currently known.
        cameFrom := an empty map

        // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
        gScore := map with default value of Infinity
        gScore[start] := 0

            // For node n, fScore[n] := gScore[n] + h(n). fScore[n] represents our current best guess as to
            // how short a path from start to finish can be if it goes through n.
            fScore := map with default value of Infinity
            fScore[start] := h(start)

            while openSet is not empty
                // This operation can occur in O(1) time if openSet is a min-heap or a priority queue
                current := the node in openSet having the lowest fScore[] value
                if current = goal
                    return reconstruct_path(cameFrom, current)

                openSet.Remove(current)
                for each neighbor of current
                    // d(current,neighbor) is the weight of the edge from current to neighbor
                    // tentative_gScore is the distance from start to the neighbor through current
        tentative_gScore := gScore[current] + d(current, neighbor)
                    if tentative_gScore<gScore[neighbor]
                        // This path to neighbor is better than any previous one. Record it!
                        cameFrom[neighbor] := current
                        gScore[neighbor] := tentative_gScore
                        fScore[neighbor] := gScore[neighbor] + h(neighbor)
                        if neighbor not in openSet
                            openSet.add(neighbor)

            // Open set is empty but goal was never reached
            return failure
        */

        #endregion
    }
}
