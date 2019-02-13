namespace EvolutionSim
{
  internal class Food : IEatable
  {
    private readonly int value;

    internal Food(int value)
    {
      this.value = value;
    }

    public object Clone()
    {
      return new Food(this.value);
    }

    public int GetValue()
    {
      return this.value;
    }
  }
}
