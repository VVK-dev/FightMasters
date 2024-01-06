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

            public string Description { get; } = "Shoot a bolt of lightning from your fingertips, dealing 4 lightning damage to your target. Has a 30% chance to Shock the target.";

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

                (string PlaySummary, bool[] DamageDodged) = PlayHelper.DamagePlayer(this, p1, p2);

                //Zap has a 30% chance to apply a shock token to the target

                if (!DamageDodged[0] && (new Random().Next(1,10) <= 3))
                {

                    //If damage isn't dodged, chance to apply shock token

                    PlaySummary += "The attack has lingering effects...";
                    PlaySummary += PlayHelper.AddOpponentTokens(this.TokensAppliedOpponent!, p2);

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

            public string Description { get; } = "Transform your head into that of a dragon and take a deep breath, releasing hot lava as you exhale dealing 5 fire damage and applying a Burn token to your target.";

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

                (string PlaySummary, bool[] DamageDodged) = PlayHelper.DamagePlayer(this, p1, p2);

                //Dragon Breath applies a Burn token to the target if damage is not dodged

                if (!DamageDodged[0])
                {

                    PlaySummary += "The emblazoned atmosphere leaves its mark...";
                    PlaySummary += PlayHelper.AddOpponentTokens(this.TokensAppliedOpponent!, p2);

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

                //This card's effects last until the start of the player's next turn.

                string DeactivateSummary = "The effects of the Lesser Frost Shield wear off, reducing your physical and frost resistances by 20.";

                p1.Resistances["Frost"] -= 20;
                p1.Resistances["Physical"] -= 20;

                p1.PersistentCards.Remove(this);

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

            public string Description { get; } = "Push your hands into the ground and grab as hard as you can, pulling a massive boulder from the earth beneath you and fling it at your opponent dealing 12 physical damage if it hits.";

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

                (string PlaySummary, _) = PlayHelper.DamagePlayer(this, p1, p2);

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

        internal class SummonWolf : ICard
        {

            //Properties

            public string Name { get; } = "Summon Wolf";

            public string Description { get; } = "Summon a wolf to fight by your side for 3 turns. It deals 2 physical damage and reduces your opponent's physical resistance by 1% everytime it attacks (their resistance goes back to normal after the wolf is unsummoned). If you have atleast 3 wolves, they form a wolf pack, causing each to deal 1 extra damage per turn. This effect also makes the physical resistance they shred from your opponent permanent for the rest if the match (including the resistance shredded until that point). Anytime a wolf is added to the pack, each wolf's duration is increased by 1.";

            public int StaminaCost { get; } = 3;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = { new NeutralMinions.Wolf() };

            public bool HasDeactivate { get; } = false;

            //Constructor

            public SummonWolf() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                return PlayHelper.SummonMinions(this, p1);

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

    internal class SummonZombie : ICard
    {

        //Properties

        public string Name { get; } = "Summon Zombie";

        public string Description { get; } = "Summon a zombie to fight by your side for 2 turns. It deals 1 poison damage and has a 50% chance to apply a poison token on hit. If its target already has a poison token on them, it deals double damage.";

        public int StaminaCost { get; } = 2;

        public Damage[]? DamageDealt { get; } = null;

        public int Heal { get; } = 0;

        public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

        public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

        public IMinion[]? Summons { get; } = { new NeutralMinions.Zombie() };

        public bool HasDeactivate { get; } = false;

        //Constructor

        public SummonZombie() { }

        //Methods

        public string Play(Player p1, Player p2)
        {

            return PlayHelper.SummonMinions(this, p1);

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
