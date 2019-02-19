namespace EvolutionSim
{
    using System.Collections.Generic;

    public class Evolution
    {
        private int maxSubjectsCount;
        private int subjectsMinLimit;
        private IList<Subject> subjects;
        private Map map;

        public Evolution(int maxSubCount, int subMinLimit)
        {
            this.maxSubjectsCount = maxSubCount;
            this.subjectsMinLimit = subMinLimit;
            this.subjects = new List<Subject>();
            this.map = new Map();
        }

        public void Evolve()
        {

        }

        public void SaveMap(string path)
        {
            FileManagment.SaveMapToTxt(map, path);
        }

        public void LoadMap(string path)
        {
            this.map = FileManagment.LoadMapFromTxt(path);
        }
    }
}
