namespace EvolutionSim
{
    internal class Point
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        internal Point() : this(0, 0) { }

        internal Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
