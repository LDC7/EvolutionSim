namespace EvolutionSim
{
    using System;

    public class Rules
    {
        public Func<Food> RuleFood { get; set; }
        public Func<Poison> RulePoison { get; set; }
    }
}
