namespace EvolutionSim
{
    using System;

    internal class Gen
    {
        internal static int MinValue { get; } = 0;
        internal static int MaxActionsValue { get; } = 17;
        internal static int MaxValue { get; } = 100 + MaxActionsValue - 1;
        private int value;

        internal int Value
        {
            get { return this.value; }
            set
            {
                int tempVar = value < MinValue ? Math.Abs(value) : value;
                this.value = (tempVar > MaxValue ? tempVar % MaxValue : tempVar);
            }
        }

        internal Gen(int gen)
        {
            Value = gen;
        }

        internal enum GenType
        {
            // Move
            MoveNW = 0,
            MoveNN = 1,
            MoveNE = 2,
            MoveEE = 3,
            MoveSE = 4,
            MoveSS = 5,
            MoveSW = 6,
            MoveWW = 7,

            // Rotation
            Rotation1 = 8,
            Rotation2 = 9,
            Rotation3 = 10,
            Rotation4 = 11,
            Rotation5 = 12,
            Rotation6 = 13,
            Rotation7 = 14,

            // Look
            Look = 15,

            // Pick
            Pick = 16,

            // Attack
            Attack = 17,
        }
    }
}
