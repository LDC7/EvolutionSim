namespace EvolutionSim
{
    using System;

    internal class DNA
    {
        private static Random random = new Random();
        internal Gen[] gens;
        internal static int GensCount { get; } = 100;

        internal static DNA CreateRandom()
        {
            DNA res = new DNA();
            Gen[] dnaGens = new Gen[GensCount];
            for (int i = 0; i < GensCount; i++)
            {
                dnaGens[i] = new Gen((Gen.GenType)random.Next(Gen.MinValue, Gen.MaxValue + 1));
            }

            res.gens = dnaGens;
            return res;
        }

        internal void Mutate(double chance)
        {
            for (int i = 0; i < GensCount; i++)
            {
                if (random.NextDouble() >= chance)
                {
                    gens[i] = new Gen((Gen.GenType)random.Next(Gen.MinValue, Gen.MaxValue + 1));
                }
            }
        }

        internal static DNA Mix(DNA dna1, DNA dna2)
        {
            DNA newDNA = new DNA();
            for (int i = 0; i < GensCount; i++)
            {
                newDNA.gens[i] = random.Next(2) > 0 ? dna1.gens[i] : dna2.gens[i];
            }

            return newDNA;
        }
    }
}
