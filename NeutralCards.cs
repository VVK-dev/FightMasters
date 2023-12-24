namespace FightMasters
{
    internal class NeutralCards
    {
        internal class Dummy : ICard
        {

            //Properties

            public string Name { get; } = "Dummy";

            public string Description { get; } = "You shouldn't be reading this, if you are then please report this as a bug to me.";

            public int StaminaCost { get; } = 0;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public Dummy() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                return "Nothing Happens.";

            }

            public string DeactivateEffects(Player p1, Player p2)
            {

                //Has no effects to deactivate

                return string.Empty;

            }

            public override string? ToString()
            {
                return CardPrinter.PrintCard(this);
            }

        }


        //STORAGE FOR ALL NEUTRAL (NON CLASS SPECIFIC) CARDS

        internal class Zap : ICard
        {

            //Properties

            public string Name { get; } = "Zap";

            public string Description { get; } = "Shoot a bolt of lightning from your fingertips, dealing 5 lightning " +
                "damage to your target. Has a 30% chance to Shock the target.";

            public int StaminaCost { get; } = 3;

            public Damage[]? DamageDealt { get; } = { new Damage("Lightning", 4) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = new() {
                
                { "<S>", new List<IToken>() { new ShockToken() } }
            
            };

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public Zap() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                string PlaySummary = PlayHelper.DamagePlayer(this, p1, p2);

                //Zap has a 30% chance to apply a shock token to the target

                Random random = new();

                if (random.Next(1, 10) <= 3)
                {

                    PlaySummary += PlayHelper.AddOpponentTokens(this, p2);

                }

                return PlaySummary;

            }

            public string DeactivateEffects(Player p1, Player p2)
            {

                //Has no effects to deactivate

                return string.Empty;

            }

            public override string? ToString()
            {
                return CardPrinter.PrintCard(this);
            }

        }

        internal class DragonBreath : ICard
        {

            //Properties

            public string Name { get; } = "Dragon Breath";

            public string Description { get; } = "Transform your head into that of a dragon and take a deep " +
                "breath, releasing hot lava as you exhale dealing 5 fire damage and applying a Burn token to your target.";

            public int StaminaCost { get; } = 4;

            public Damage[]? DamageDealt { get; } = { new Damage("Fire", 5) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = new() {

                { "<B>", new List<IToken>() { new BurnToken() } }
            };

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public DragonBreath() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                string PlaySummary = PlayHelper.DamagePlayer(this, p1, p2);

                //Dragon Breath applies a Burn token to the target

                PlaySummary += PlayHelper.AddOpponentTokens(this, p2);

                return PlaySummary;

            }

            public string DeactivateEffects(Player p1, Player p2)
            {

                //Has no effects to deactivate

                return string.Empty;

            }

            public override string? ToString()
            {
                return CardPrinter.PrintCard(this);
            }

        }

        internal class LFrostShield : ICard
        {

            //Properties

            public string Name { get; } = "Lesser Frost Shield";

            public string Description { get; } = "Gain 20% physical and Frost resistance.";

            public int StaminaCost { get; } = 3;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = true;

            //Constructor

            public LFrostShield() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                string PlaySummary = "You encase yourself in an icy wind.";

                p1.Resistances["Frost"] += 20;
                p1.Resistances["Physical"] += 20;

                return PlaySummary;

            }

            public string DeactivateEffects(Player p1, Player p2)
            {

                string DeactivateSummary = "The effects of the Lesser Frost Shield wear off, reducing your physical and " +
                    "frost resistances by 20.";

                p1.Resistances["Frost"] -= 20;
                p1.Resistances["Physical"] -= 20;

                return DeactivateSummary;

            }

            public override string? ToString()
            {
                return CardPrinter.PrintCard(this);
            }

        }

        internal class BoulderToss : ICard
        {

            //Properties

            public string Name { get; } = "Boulder Toss";

            public string Description { get; } = "You push your hands into the ground and grab as hard as you can, pulling a " +
                "massive boulder from the earth beneath you and hurtling it at your opponent dealing 12 physical damage.";

            public int StaminaCost { get; } = 6;

            public Damage[]? DamageDealt { get; } = { new Damage("Physical", 12) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public BoulderToss() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                string PlaySummary = PlayHelper.DamagePlayer(this, p1, p2);

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

    }

}
