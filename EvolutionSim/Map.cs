namespace EvolutionSim
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Map
    {
        private static Random random = new Random();
        internal int SizeX { get; private set; }
        internal int SizeY { get; private set; }
        internal int Size { get { return this.SizeX * this.SizeY; } }
        internal bool IsLoaded { get; set; }
        internal int EmptyCellsCount { get; private set; }
        internal int SubjectsCount { get; private set; }
        internal int FoodCount { get; private set; }
        internal int PoisonCount { get; private set; }
        internal Cell[,] Cells { get; set; }

        private const int DefaultSizeX = 10;
        private const int DefaultSizeY = 10;

        internal Map() : this(DefaultSizeX, DefaultSizeY) { }

        internal Map(int sizeX, int sizeY)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;
            this.Cells = new Cell[this.SizeX, this.SizeY];
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

        internal void MapRun(Action<Cell, int, int> cellAction = null, Action<int> afterLineAction = null, Action<int> beforeLineAction = null)
        {
            for (int i = 0; i < this.SizeX; i++)
            {
                beforeLineAction?.Invoke(i);
                for (int j = 0; j < this.SizeY; j++)
                {
                    cellAction?.Invoke(this.Cells[i, j], i, j);
                }
                afterLineAction?.Invoke(i);
            }
        }

        internal void CountEmptyFoodPoisonCells()
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
