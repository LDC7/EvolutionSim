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
      map.SaveMapToTxt(path);
    }

    public void LoadMap(string path)
    {
      map.LoadMapFromTxt(path);
    }
  }
}
