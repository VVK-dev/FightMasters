namespace FightMasters
{
    public class NeutralMinions
    {

        internal class Wolf : IMinion
        {

            public string Name { get; set; } = "Wolf";

            public int Duration { get; set; } = 3;

            public Damage[]? DamageDealt { get; } = { new Damage("Physical", 2) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            //Constructor
            public Wolf() { }

            //Methods
            public string Act(Player p1, Player p2)
            {

                string ActSummary;

                


            }

            //ToString
            public override string? ToString()
            {
                return MinionPrinter.PrintMinion(this);
            }

        }
        
    }

}
