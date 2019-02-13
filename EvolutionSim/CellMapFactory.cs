namespace EvolutionSim
{
  internal abstract class CellMapFactory
  {
    internal static Cell GetCell(Cell.CellType type)
    {
      switch (type)
      {
        case Cell.CellType.Empty:
          return new Cell();
        case Cell.CellType.Wall:
          return new Cell
          {
            Value = new Wall(),
            Type = Cell.CellType.Wall
          };
        case Cell.CellType.Food:
          return new Cell
          {
            Value = new Food(),
            Type = Cell.CellType.Food
          };
        case Cell.CellType.Poison:
          return new Cell
          {
            Value = new Poison(),
            Type = Cell.CellType.Poison
          };

        default:
          return new Cell();
      }
    }

    internal static void ToFood(Cell cell, Food food)
    {
      cell.Type = Cell.CellType.Food;
      cell.Value = (ICell)food.Clone();
    }

    internal static void ToPoison(Cell cell, Poison poison)
    {
      cell.Type = Cell.CellType.Poison;
      cell.Value = (ICell)poison.Clone();
    }
  }
}
