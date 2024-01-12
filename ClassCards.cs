using System.Text;

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

        internal class Repurpice : ICard
        {
            //Properties

            public string Name { get; } = "Repurpice";

            public string Description { get; } = "Convert each chill token on your opponent into block tokens for yourself.";

            public int StaminaCost { get; } = 2;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = new() {

                { "<+>", new List<IToken>() { new BlockToken() } }

            };

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public Repurpice() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                StringBuilder PlaySummary = new();

                if (p2.TokensActive.ContainsKey("<C>"))
                {

                    for (int i = p2.TokensActive["<C>"].Count; i >= 0; i--)
                    {

                        PlaySummary.AppendLine(PlayHelper.AddCasterTokens(this.TokensAppliedCaster!, p1));

                    }

                    p2.TokensActive.Remove("<C>");

                }

                return PlaySummary.ToString();

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

                //Add block tokens

                string TokenSummary = PlayHelper.AddCasterTokens(this.TokensAppliedCaster!, p2);

                return PlaySummary.AppendLine(TokenSummary).ToString();

            }

            public string DeactivateEffects(Player p1, Player p2)
            {

                StringBuilder DeactivateSummary = new("Snowstorm protection's effect wears off.\n");

                //Resistances return to normal
                foreach (string resistance in p1.Resistances.Keys)
                {

                    p1.Resistances[resistance] += 20;
                    DeactivateSummary.AppendLine($"{p1.PlayerName}'s {resistance} resistance decreases by 20%.");
                }

                return DeactivateSummary.ToString();

            }

            public override string ToString()
            {
                return CardPrinter.PrintCard(this);
            }

        }


    }

    internal class HeraldCards
    {

        //Class specific cards for the Herald class

        internal class SwiftInferno: ICard
        {

            //Properties

            public string Name { get; } = "Swift Inferno";

            public string Description { get; } = "Quickly swing twice at your opponent with your emblazoned katana, dealing 2 physical damage on the first hit and 3 fire damage on the second, and gain a dodge token.";

            public int StaminaCost { get; } = 5;

            public Damage[]? DamageDealt { get; } = { new Damage("Physical", 2), new Damage("Fire", 3) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = new() {

                { "</>", new List<IToken>() { new DodgeToken() } }

            };

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = null;

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public SwiftInferno() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                (string PlaySummary, _) = PlayHelper.DamagePlayer(this, p1, p2);

                PlaySummary += PlayHelper.AddOpponentTokens(this.TokensAppliedCaster!, p2);

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

        internal class FromTheirAshes : ICard
        {

            //Properties

            public string Name { get; } = "From their Ashes...";

            public string Description { get; } = "Regain 2 health for every burn token on your opponent. If they have none, apply 3 burn tokens onto them and gain a dodge token.";

            public int StaminaCost { get; } = 5;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 2;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = new() {

                { "</>", new List<IToken>() { new DodgeToken() } }

            };

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = new()
            {
                {"<B>", new List<IToken>{new BurnToken(), new BurnToken(), new BurnToken(), new BurnToken(), new BurnToken() } }
            };

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public FromTheirAshes() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                StringBuilder PlaySummary = new();

                //If opponent has burn tokens, heal caster per token
                if (p2.TokensActive.ContainsKey("<B>")) {

                    for (int i = 0; i < p2.TokensActive["<B>"].Count; i++)
                    {

                        PlaySummary.AppendLine(PlayHelper.HealPlayer(this.Heal, p1));

                    }

                    return PlaySummary.ToString();
                }

                //If not:

                //1) apply burn tokens to them

                PlayHelper.AddOpponentTokens(this.TokensAppliedOpponent!, p2);

                //2) add dodge tokens to caster

                PlayHelper.AddCasterTokens(this.TokensAppliedCaster!, p1);

                return PlaySummary.ToString();

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

    internal class RotcherCards 
    {
        
        //Class cards for the rotcher class

        internal class MudArrows : ICard
        {

            //Properties

            public string Name { get; } = "Mud Arrows";

            public string Description { get; } = "Shoot 3 'mud' covered arrows at your opponent each dealing 2 physical damage and applying a poison token if they hit.";

            public int StaminaCost { get; } = 4;

            public Damage[]? DamageDealt { get; } = { new Damage("Physical", 2), new Damage("Physical", 2), new Damage("Physical", 2) };

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = new() {

                { "<P>", new List<IToken>() { new PoisonToken() } }

            };

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public MudArrows() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                StringBuilder PlaySummary = new();

                //Deal damage

                (string DamageSummary, bool[] hit) = PlayHelper.DamagePlayer(this, p1, p2);

                PlaySummary.AppendLine(DamageSummary);

                //Per arrow hit, add a poison token

                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i])
                    {

                        PlaySummary.AppendLine(PlayHelper.AddOpponentTokens(this.TokensAppliedOpponent!, p2));

                    }
                    
                }

                return PlaySummary.ToString();

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

        internal class PlagueSeekers : ICard
        {

            //Properties

            public string Name { get; } = "Plague Seekers";

            public string Description { get; } = "Conjure a force of pure rot and send it towards your opponent, weakening their nerves and destroying all of their dodge and block tokens, and applying a posion token. Then, summon a horde of 3 zombies to fight by your side for 2 turns. A zombie deals 1 poison damage and has a 50% chance to apply a poison token on hit. If its target already has a poison token on them, it deals double damage.";

            public int StaminaCost { get; } = 8;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = new() {

                { "<P>", new List<IToken>() { new PoisonToken() } }

            };

            public IMinion[]? Summons { get; } = { new NeutralMinions.Zombie(), new NeutralMinions.Zombie(), new NeutralMinions.Zombie() };

            public bool HasDeactivate { get; } = false;

            //Constructor

            public PlagueSeekers() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                //Remove tokens

                if (p2.TokensActive.ContainsKey("</>")) { p2.TokensActive.Remove("</>"); }
                if (p2.TokensActive.ContainsKey("<+>")) { p2.TokensActive.Remove("<+>"); }

                IEnumerable<char> PlaySummary = $"All of {p2.PlayerName}'s dodge and block tokens are destroyed!";

                //Add posion token, summon zombies and return the total string

                return (string) PlaySummary.Concat(PlayHelper.AddOpponentTokens(this.TokensAppliedOpponent!, p2).Concat(PlayHelper.SummonMinions(this, p1))); ;

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

    internal class DruidCards { }

}
