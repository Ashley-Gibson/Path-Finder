using System;
using System.Collections.Generic;
using System.Drawing;

namespace Path_Finder
{
    public class Grid
    {
        public const int horizontalPoints = 40;
        public const int verticalPoints = 30;
        public Spot[,] GridArray = new Spot[verticalPoints, horizontalPoints];
        
        // Grid Characters
        public const char SPACE = '~';
        public const char PLAYER = '$';
        public const char DESTINATION = '?';
        public const char OBSTACLE = '&';
        public const char NEWLINE = '\n';
        public const char ROUTE = '$';

        public Point PlayerPosition = new Point(0,0);

        public List<Spot> closedSet = new List<Spot>();
        public List<Spot> openSet = new List<Spot>();

        public readonly Spot start = new Spot()
        {
            F = verticalPoints + horizontalPoints,
            G = 0,
            H = verticalPoints + horizontalPoints,
            X = 0,
            Y = 0,
            Character = PLAYER,
            Neighbours = new List<Spot>(),
            PreviousSpot = new List<Spot>()
        };

        public readonly Spot end = new Spot()
        {
            F = 0,
            G = 0,
            H = 0,
            X = horizontalPoints - 1,
            Y = verticalPoints - 1,
            Character = DESTINATION,
            Neighbours = new List<Spot>(),
            PreviousSpot = new List<Spot>()
        };

        // Obstacles
        public const int numberOfObstacles = 100;
        public readonly Random random = new Random();
        public int[] randomX = new int[numberOfObstacles];
        public int[] randomY = new int[numberOfObstacles];

        public bool solutionFound = false;

        public void DrawGrid()
        {
            Console.Clear();

            for (int y = 0; y < verticalPoints; y++)
            {
                for (int x = 0; x < horizontalPoints; x++)
                {
                    if (GridArray[y, x].Character == ROUTE || GridArray[y, x].Character == PLAYER)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if(GridArray[y, x].Character == DESTINATION)
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(GridArray[y, x].Character);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(NEWLINE);
            }
        }

        private int GenerateRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public void SetupGrid(bool randomNumbers = true)
        {
            if(randomNumbers)
                for (int i = 0; i < numberOfObstacles; i++)
                {
                    randomX[i] = GenerateRandomNumber(0, horizontalPoints);
                    randomY[i] = GenerateRandomNumber(0, verticalPoints);
                }

            for (int y = 0; y < verticalPoints; y++)
            {
                for (int x = 0; x < horizontalPoints; x++)
                {
                    bool randomNumberSelected = false;

                    for (int i = 0; i < numberOfObstacles; i++)
                    {
                        if (x == randomX[i] && y == randomY[i])
                        {
                            GridArray[y, x] = new Spot { F = 0, G = 0, H = 0, X = x, Y = y, Character = OBSTACLE };
                            randomNumberSelected = true;
                        }
                    }

                    if (randomNumberSelected)
                        continue;

                    if (x == start.X && y == start.X)
                        GridArray[y, x] = start;
                    else if (x == end.X && y == end.Y)
                        GridArray[y, x] = end;
                    else
                    {
                        GridArray[y, x] = new Spot()
                        {
                            X = x,
                            Y = y,
                            Character = SPACE
                        };
                    }                     
                }               
            }

            DrawGrid();
            UpdateNeighbours();
        }

        public void UpdateGridWithSpot(Spot spot)
        {
            GridArray[PlayerPosition.Y, PlayerPosition.X].Character = ROUTE;
            
            PlayerPosition.X = spot.X;
            PlayerPosition.Y = spot.Y;

            GridArray[spot.Y, spot.X] = spot;

            UpdateNeighbours();
        }

        // Update all Neighbours
        // TODO: Develop a more efficient way of doing this
        public void UpdateNeighbours()
        {
            for (int y = 0; y < verticalPoints; y++)
            {
                for (int x = 0; x < horizontalPoints; x++)
                {
                    // Clear List
                    GridArray[y, x].Neighbours = new List<Spot>() { };

                    // Recalculate all neighbours
                    if (y < verticalPoints - 1)
                        GridArray[y, x].Neighbours.Add(GridArray[y + 1, x]);
                    if(y > 0)
                        GridArray[y, x].Neighbours.Add(GridArray[y - 1, x]); 
                    if(x < horizontalPoints - 1)
                        GridArray[y, x].Neighbours.Add(GridArray[y, x + 1]);
                    if(x > 0)
                        GridArray[y, x].Neighbours.Add(GridArray[y, x - 1]);
                    if(x > 0 && y > 0)
                        GridArray[y, x].Neighbours.Add(GridArray[y - 1, x - 1]);
                    if(x < horizontalPoints - 1 && y > 0)
                        GridArray[y, x].Neighbours.Add(GridArray[y - 1, x + 1]);
                    if(x > 0 && y < verticalPoints - 1)
                        GridArray[y, x].Neighbours.Add(GridArray[y + 1, x - 1]);
                    if(x < horizontalPoints - 1 && y < verticalPoints - 1)
                        GridArray[y, x].Neighbours.Add(GridArray[y + 1, x + 1]);
                }
            }
        }

        public void DisplayPath(List<Spot> optimalPath)
        {
            SetupGrid(false);

            for (int i = 0; i < optimalPath.Count; i++)
                UpdateGridWithSpot(optimalPath[i]);

            DrawGrid();
        }
    }
}
