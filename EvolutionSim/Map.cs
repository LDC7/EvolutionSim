namespace EvolutionSim
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  internal class Map
  {
    internal int SizeX { get; private set; }
    internal int SizeY { get; private set; }
    internal int Size { get { return this.SizeX * this.SizeY; } }
    internal bool IsLoaded { get; private set; }
    internal int EmptyCellsCount { get; private set; }
    internal int SubjectsCount { get; private set; }
    internal int FoodCount { get; private set; }
    internal int PoisonCount { get; private set; }
    private Cell[,] cells;

    private const int DefaultSizeX = 10;
    private const int DefaultSizeY = 10;

    internal Map() : this(DefaultSizeX, DefaultSizeY) { }

    internal Map(int sizeX, int sizeY)
    {
      this.SizeX = sizeX;
      this.SizeY = sizeY;
      this.cells = new Cell[this.SizeX, this.SizeY];
    }



    internal void CreateFood(int count, Food food)
    {
      int additionalFood = count > this.EmptyCellsCount ? this.EmptyCellsCount : count;

      if (this.EmptyCellsCount == additionalFood)
      {
        this.MapRun(cellAction: (c, i, j) =>
        {
          if (c.Type == Cell.CellType.Empty) { CellMapFactory.ToFood(c, food); }
        });
        this.EmptyCellsCount = 0;

        return;
      }

      Random random = new Random();
      List<Cell> emptyCells = this.GetEmptyCells().ToList();
      Cell cell;
      for (int i = 0; i < additionalFood; i++)
      {
        cell = emptyCells.ElementAt(random.Next(0, emptyCells.Count()));
        CellMapFactory.ToFood(cell, food);
        this.EmptyCellsCount--;
        emptyCells.Remove(cell);
      }
    }

    internal void CreatePoison(int count, Poison poison)
    {
      int additionalPoison = count > this.EmptyCellsCount ? this.EmptyCellsCount : count;

      if (this.EmptyCellsCount == additionalPoison)
      {
        this.MapRun(cellAction: (c, i, j) =>
        {
          if (c.Type == Cell.CellType.Empty) { c.Type = Cell.CellType.Poison; c.Value = (ICell)poison.Clone(); }
        });
        this.EmptyCellsCount = 0;

        return;
      }

      Random random = new Random();
      List<Cell> emptyCells = this.GetEmptyCells().ToList();
      Cell cell;
      for (int i = 0; i < additionalPoison; i++)
      {
        cell = emptyCells.ElementAt(random.Next(0, emptyCells.Count()));
        CellMapFactory.ToPoison(cell, poison);
        this.EmptyCellsCount--;
        emptyCells.Remove(cell);
      }
    }

    internal Subject[] GetSubjects()
    {
      Subject[] result = new Subject[this.SubjectsCount];

      int index = 0;
      this.MapRun(
        cellAction: (c, i, j) =>
        {
          if (c.Type == Cell.CellType.Subject)
          {
            result[index] = (Subject)c.Value;
            index++;
          }
        });

      return result;
    }

    internal Cell[] GetEmptyCells()
    {
      Cell[] result = new Cell[this.EmptyCellsCount];

      int index = 0;
      this.MapRun(cellAction: (c, i, j) =>
      {
        if (c.Type == Cell.CellType.Empty)
        {
          result[index] = c;
          index++;
        }
      });

      return result;
    }

    internal void LoadMapFromTxt(string path)
    {
      Cell[,] cells;
      string[] line;
      const char Separator = ' ';

      using (FileStream fileStream = new FileStream(path, FileMode.Open))
      using (StreamReader reader = new StreamReader(fileStream))
      {
        if (!int.TryParse(reader.ReadLine(), out int sizeX))
        {
          throw new FileLoadException();
        }
        if (!int.TryParse(reader.ReadLine(), out int sizeY))
        {
          throw new FileLoadException();
        }

        cells = new Cell[sizeX, sizeY];
        for (int i = 0; i < sizeX; i++)
        {
          line = reader.ReadLine().Split(Separator);
          for (int j = 0; j < sizeY; j++)
          {
            if (!int.TryParse(line[j], out int cell))
            {
              throw new FileLoadException();
            }

            cells[i, j] = CellMapFactory.GetCell((Cell.CellType)cell);
          }
        }

        this.SizeX = sizeX;
        this.SizeY = sizeY;
        this.cells = cells;
        this.CountEmptyFoodPoisonCells();
        this.IsLoaded = true;
      }
    }

    internal void SaveMapToTxt(string path)
    {
      const string Separator = " ";

      using (FileStream fileStream = new FileStream(path, FileMode.Create))
      using (StreamWriter writer = new StreamWriter(fileStream))
      {
        writer.WriteLine(this.SizeX);
        writer.WriteLine(this.SizeY);

        this.MapRun(
        cellAction: (c, i, j) =>
        {
          writer.Write($"{(int)c.Type}{(j == this.SizeY - 1 ? string.Empty : Separator)}");
        },
        afterLineAction: (i) => { writer.WriteLine(); }
        );
      }
    }

    private void MapRun(Action<Cell, int, int> cellAction = null, Action<int> afterLineAction = null, Action<int> beforeLineAction = null)
    {
      for (int i = 0; i < this.SizeX; i++)
      {
        beforeLineAction?.Invoke(i);
        for (int j = 0; j < this.SizeY; j++)
        {
          cellAction?.Invoke(this.cells[i, j], i, j);
        }
        afterLineAction?.Invoke(i);
      }
    }

    private void CountEmptyFoodPoisonCells()
    {
      this.MapRun(
        cellAction: (c, i, j) =>
        {
          switch (c.Type)
          {
            case Cell.CellType.Empty:
              this.EmptyCellsCount++;
              break;
            case Cell.CellType.Food:
              this.FoodCount++;
              break;
            case Cell.CellType.Poison:
              this.PoisonCount++;
              break;
          }
        });
    }
  }
}
