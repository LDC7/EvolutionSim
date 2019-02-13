namespace EvolutionSim
{
  using System;

  internal interface IEatable : ICell, ICloneable
  {
    int GetValue();
  }
}
