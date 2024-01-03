namespace FightMasters
{

    //STORAGE FOR ALL CLASS SPECIFIC CARDS

    internal class VikingCards
    {

        //Class cards for the Viking Class

        internal class HeavySwing : ICard
        {
            //Properties

            public string Name { get; } = "Heavy Swing";

            public string Description { get; } = "Perform an overhead strike with your axe, dealing 8 physical damage." +
                "This damage ignores 20% physical resistance.";

            public int StaminaCost { get; } = 4;

            public Damage[]? DamageDealt { get; } = { new Damage("Physical", 8) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public HeavySwing() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                p2.Resistances["Physical"] -= 20;

                (string PlaySummary, _) = PlayHelper.DamagePlayer(this, p1, p2);

                p2.Resistances["Physical"] += 20;

                return PlaySummary;

            }

            public string DeactivateEffects(Player p1, Player p2)
            {

                //Has no effects to deactivate

                return string.Empty;

            }

            public override string ToString()
            {
                return CardPrinter.PrintCard(this);
            }

        }

        internal class IceArmour : ICard
        {
            //Properties

            public string Name { get; } = "Ice Armour";

            public string Description { get; } = "Gain 2 block tokens. Apply 2 chill tokens to your opponent the next time they attack you.";

            public int StaminaCost { get; } = 5;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = new() {

                { "<+>", new List<IToken>() { new BlockToken(), new BlockToken() } }

            };

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public IceArmour() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                string TokenSummary = PlayHelper.AddCasterTokens(this.TokensAppliedCaster!, p1);

                p2.OnDealDamageCardEffects += OnOpponentDamage;

                return TokenSummary;

            }

            //Method that triggers when the opponent does damage next
            private static Damage OnOpponentDamage((Player p2, Damage d) input)
            {

                //Must create a seperate dictionary here for this as this method is static

                Dictionary<string, List<IToken>> ChillTokens = new() {

                    { "<+>", new List<IToken>() { new ChillToken(), new ChillToken() } }

                };

                PlayHelper.AddOpponentTokens(ChillTokens, input.p2);

                return input.d;

            }

            public string DeactivateEffects(Player p1, Player p2)
            {

                //Has no effects to deactivate

                return string.Empty;

            }

            public override string ToString()
            {
                return CardPrinter.PrintCard(this);
            }

        }

        internal class SnowstormProtection : ICard
        {
            //Properties

            public string Name { get; } = "Snowstorm Protection";

            public string Description { get; } = "Increase all your resistances by 20% until your next turn and gain 2 block tokens.";

            public int StaminaCost { get; } = 7;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = new() {

                { "<+>", new List<IToken>() { new BlockToken(), new BlockToken() } }

            };

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public SnowstormProtection() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                StringBuilder PlaySummary = new();

                p1.CurrentStamina -= this.StaminaCost;

                //Increase resistances
                foreach (string resistance in p1.Resistances.Keys)
                {

                    p1.Resistances[resistance] += 20;
                    PlaySummary.Append($"{p1.PlayerName}'s {resistance} resistance increases by 20%.");
        }

    }

    internal class HeraldCards { }

    internal class RotcherCards { }

    internal class DruidCards { }

}
