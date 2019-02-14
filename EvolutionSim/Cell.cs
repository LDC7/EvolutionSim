namespace EvolutionSim
{
    internal class Cell
    {
        internal CellType Type { get; set; }

        internal ICell Value { get; set; }

        internal Cell()
        {
            this.Type = CellType.Empty;
        }

        internal enum CellType
        {
            Empty = 0,
            Wall = 1,
            Subject = 2,
            Food = 3,
            Poison = 4,
        }
    }
}
