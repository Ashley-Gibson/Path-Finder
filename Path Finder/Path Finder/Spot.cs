using System.Collections.Generic;

namespace Path_Finder
{
    public class Spot
    {
        public int F;
        public int G;
        public int H;
        public int X;
        public int Y;
        public char Character;
        public List<Spot> Neighbours;
        public List<Spot> PreviousSpot;

        public Spot()            
        {
            F = 0;
            G = 0;
            H = 0;
            X = 0;
            Y = 0;
            Character = Grid.SPACE;
            Neighbours = new List<Spot>();
            PreviousSpot = new List<Spot>();
        }
    }
}
