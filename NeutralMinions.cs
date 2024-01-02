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

            private int OpponentInitialPhysResist { get; set; }

            private bool isFirstTurn { get; set; } = true;

            private bool isWolfPack { get; set; } = false;

            //Constructor
            public Wolf() { }

            //Methods
            public string Act(Player p1, Player p2)
            {

                string ActSummary;

                if (!isWolfPack)
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

                            wolf.isWolfPack = true;

                        }

                    }

                }

                //Deal damage

                (ActSummary, bool[] dodged) = PlayHelper.DamagePlayer(this, p1, p2);

                //If opponent hasn't dodged attack, reduce their physical resistance by 1%

                if (!dodged[0])
                {

                    if (isFirstTurn) { this.OpponentInitialPhysResist = p2.Resistances["Physical"]; isFirstTurn = false; }

                    p2.Resistances["Physical"] -= 1;
                    ActSummary += $"{p2.PlayerName} loses 1% physical resistance.";

                }

                if ((this.Duration == 1))
                {

                    if (!isWolfPack)
                    {
                        //If this wolf is part of a pack, it's physical resistance shred effect is permanent

                        p2.Resistances["Physical"] = this.OpponentInitialPhysResist;

                        ActSummary += $"{p2.PlayerName}'s physical resistance returns to normal.";

                    }

                    ActSummary += "\n Wolf is unsummoned.";

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
