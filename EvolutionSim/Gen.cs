namespace EvolutionSim
{
    using System;

    internal class Gen
    {
        private int value;
        internal int MinValue { get; } = 0;
        internal int MaxValue { get; } = 0;

        internal int Value
        {
            get { return this.value; }
            set
            {
                int tempVar = value < MinValue ? Math.Abs(value) : value;
                this.value = tempVar > MaxValue ? tempVar % MaxValue : tempVar;
            }
        }
    }
}
