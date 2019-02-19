namespace EvolutionSim
{
    using System.Collections.Generic;
    using System.IO;

    internal abstract class FileManagment
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

        internal static IList<Subject> LoadSubjectsFromTxt(string path)
        {
            const char Separator = ' ';
            string[] line;
            List<Subject> subjects = new List<Subject>();
            List<Gen> gens;

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                if (!int.TryParse(reader.ReadLine(), out int size))
                {
                    throw new FileLoadException();
                }

                for (int i = 0; i < size; i++)
                {
                    line = reader.ReadLine().Split(Separator);
                    gens = new List<Gen>();

                    foreach (var gen in line)
                    {
                        if (!int.TryParse(gen, out int genValue))
                        {
                            throw new FileLoadException();
                        }

                        gens.Add(new Gen(genValue));
                        subjects.Add(new Subject(new DNA(gens.ToArray())));
                    }
                }
            }

            return subjects;
        }

        internal static void SaveSubjectsToTxt(IList<Subject> subjects, string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(subjects.Count);

                foreach (var sub in subjects)
                {
                    SaveGensToTxt(sub.DNA.gens, writer);
                }
                writer.WriteLine();
            }
        }

        private static void SaveGensToTxt(IEnumerable<Gen> gens, StreamWriter writer)
        {
            const char Separator = ' ';

            foreach (var gen in gens)
            {
                writer.Write($"{gen.Value}{Separator}");
            }
        }
    }
}
