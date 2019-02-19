namespace EvolutionSim
{
    using System;
    using System.Collections.Generic;

    public class Subject
    {
        private static Random random = new Random();
        internal static int MaxTurns { get; } = 10;
        internal Point Point { get; set; }
        internal byte DirectionVector { get; set; }
        internal DNA DNA { get; private set; }
        internal IList<IEffect> Effects { get; } = new List<IEffect>();

        internal Subject() { }

        internal Subject(DNA dna)
        {
            this.DNA = dna;
        }

        internal void Process(Map map)
        {
            int actionIndex = 0;
            for (int i = 0; i < MaxTurns; i++)
            {
                actionIndex = Do(DNA.gens[actionIndex], map);
                if (actionIndex < 0)
                {
                    break;
                }
                if (actionIndex <= 1)
                {
                    actionIndex++;
                }                
            }
        }

        internal static int Do(Gen gen, Map map)
        {
            if (gen.Value > Gen.MaxActionsValue)
            {
                return gen.Value - Gen.MaxActionsValue;
            }








        }

        internal static Subject CreateRandom()
        {
            Subject subject = new Subject
            {
                DirectionVector = (byte)random.Next(0, 8),
                DNA = DNA.CreateRandom()
            };

            return subject;
        }
    }
}
