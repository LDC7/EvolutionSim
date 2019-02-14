namespace EvolutionSim
{
    public class Evolution
    {
        internal Map map;
        private int maxSubjectsCount;
        private int subjectsMinLimit;

        public Evolution()
        {
            map = new Map();
        }


        public void SaveMap(string path)
        {
            MapManagment.SaveMapToTxt(map, path);
        }

        public void LoadMap(string path)
        {
            map = MapManagment.LoadMapFromTxt(path);
        }
    }
}
