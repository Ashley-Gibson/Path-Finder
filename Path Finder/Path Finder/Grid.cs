using System;

namespace Path_Finder
{
    public class Grid
    {
        // Grid will always be a 2D square/rectangle
        public const int horizontalPoints = 40;
        public const int verticalPoints = 25;
        public char[,] GridArray = new char[verticalPoints, horizontalPoints];
        
        // Grid Characters
        public const char SPACE = '~';
        public const char PLAYER = '@';
        public const char DESTINATION = '?';
        public const char OBSTACLE = '&';
        public const char NEWLINE = '\n';

        // Obstacles
        public const int numberOfObstacles = 30;
        public readonly Random random = new Random();
        public int[] randomX = new int[numberOfObstacles];
        public int[] randomY = new int[numberOfObstacles];

        public void DrawGrid()
        {
            for (int y = 0; y < verticalPoints; y++)
            {
                for (int x = 0; x < horizontalPoints; x++)
                    Console.Write(GridArray[y, x]);

                Console.Write(NEWLINE);
            }
        }

        private int GenerateRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public void SetupGrid()
        {
            for (int i = 0; i < numberOfObstacles; i++)
            {
                randomX[i] = GenerateRandomNumber(1, horizontalPoints);
                randomY[i] = GenerateRandomNumber(0, verticalPoints - 1);
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
                            GridArray[y, x] = OBSTACLE;
                            randomNumberSelected = true;
                        }
                    }

                    if (randomNumberSelected)
                        continue;

                    if (x == 0 && y == 0)
                        GridArray[y, x] = PLAYER;
                    else if (x == horizontalPoints - 1 && y == verticalPoints - 1)
                        GridArray[y, x] = DESTINATION;
                    else
                        GridArray[y, x] = SPACE;
                }               
            }
        }
    }
}
