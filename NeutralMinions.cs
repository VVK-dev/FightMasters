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

            //Wolf specific fields

            private int TurnsAlive { get; set; }

            private bool IsWolfPack { get; set; } = false;

            //Constructor
            public Wolf() { }

            //Methods
            public string Act(Player p1, Player p2)
            {

                string ActSummary;

                if (!IsWolfPack)
                {

                    //If wolf is not part of a pack, check if it should be

                    IEnumerable<Wolf> wolves = p1.CurrentSummons.OfType<Wolf>();

                    if (wolves.Count() >= 3)
                    {

                        //If yes, make all wolves in pack deal extra damage

                        foreach (Wolf wolf in wolves)
                        {

                            wolf.DamageDealt![0] = new Damage("Physical", 3);

                            wolf.Duration++;

                            wolf.IsWolfPack = true;

                        }

                    }

                }
              
                //Deal damage

                (ActSummary, bool[] dodged) = PlayHelper.DamagePlayer(this, p1, p2);

                //If opponent hasn't dodged attack, reduce their physical resistance by 1%

                if (!dodged[0])
                {

                    p2.Resistances["Physical"] -= 1;
                    ActSummary += $"{p2.PlayerName} loses 1% physical resistance.";

                }

                this.TurnsAlive++;

                if ((this.Duration == 1) && (!IsWolfPack))
                {

                    //If this wolf isn't part of a pack, it's physical resistance shred effect is temporary

                    //As a wolf only reduces the opponent's physical resistance by 1 per turn it is alive,
                    //the opponent's physical resistance can be reset to normal by simply adding TurnsAlive
                    //to their current physical resistance

                    p2.Resistances["Physical"] += this.TurnsAlive;

                    ActSummary += $"{p2.PlayerName}'s physical resistance returns to normal.";

                }

                return ActSummary;
            }

            //ToString
            public override string? ToString()
            {
                return MinionPrinter.PrintMinion(this);
            }

        }

        internal class Zombie : IMinion
        {

            public string Name { get; set; } = "Zombie";

            public int Duration { get; set; } = 2;

            public Damage[]? DamageDealt { get; } = { new Damage("Poison", 1) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } =
            new Dictionary<string, List<IToken>> { 
                
                { "<P>", new List<IToken> { new PoisonToken() } } 
                
            };

            //Constructor
            public Zombie() { }

            //Methods
            public string Act(Player p1, Player p2)
            {

                string ActSummary;

                if (p2.TokensActive.ContainsKey("<P>") && ( (int) this.DamageDealt![0].DamageValue == 1))
                {

                    this.DamageDealt[0].DamageValue = 2;

                }

                //Deal damage

                (ActSummary, bool[] dodged) = PlayHelper.DamagePlayer(this, p1, p2);

                //If opponent hasn't dodged attack, 50% chance to add a posion token to them

                if (!dodged[0] && (new Random().Next(2) == 0))
                {

                    PlayHelper.AddOpponentTokens(this.TokensAppliedOpponent!, p2);

                }

                return ActSummary;
            }

            //ToString
            public override string? ToString()
            {
                return MinionPrinter.PrintMinion(this);
            }

        }

    }

}
