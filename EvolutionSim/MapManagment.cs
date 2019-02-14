namespace EvolutionSim
{
    using System.IO;

    internal abstract class MapManagment
    {
        internal static Map LoadMapFromTxt(string path)
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

                Map map = new Map(sizeX, sizeY);
                map.Cells = cells;
                map.CountEmptyFoodPoisonCells();
                map.IsLoaded = true;

                return map;
            }
        }

        internal static void SaveMapToTxt(Map map, string path)
        {
            const string Separator = " ";

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(map.SizeX);
                writer.WriteLine(map.SizeY);

                map.MapRun(
                cellAction: (c, i, j) =>
                {
                    writer.Write($"{(int)c.Type}{(j == map.SizeY - 1 ? string.Empty : Separator)}");
                },
                afterLineAction: (i) => { writer.WriteLine(); }
                );
            }
        }
    }
}
