namespace EvolutionSim
{
  internal class Poison : IEatable
  {
    private readonly int value;

    internal Poison(int value)
    {
      this.value = value;
    }

    public object Clone()
    {
      return new Poison(this.value);
    }

    public int GetValue()
    {
      return this.value;
    }
  }
}
