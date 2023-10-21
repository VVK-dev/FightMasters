using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightMasters
{

    //STORAGE FOR ALL CLASS CARDS

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

                string PlaySummary = PlayHelper.PlayDamage(this, p1, p2);

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

            public string Description { get; } = "Gain 2 block tokens. Apply a chill token to your opponent.";

            public int StaminaCost { get; } = 4;

            public Damage[]? DamageDealt { get; } = null;

            public int Heal { get; } = 0;

            public Dictionary<string, List<IToken>>? TokensAppliedCaster { get; } = null;

            public Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; } = new() {

                { "<C>", new List<IToken>() { new ChillToken() } }

            };

            public IMinion[]? Summons { get; } = null;

            public bool HasDeactivate { get; } = false;

            //Constructor

            public IceArmour() { }

            //Methods

            public string Play(Player p1, Player p2)
            {

                p1.CurrentStamina -= this.StaminaCost;

                string TokenSummary = PlayHelper.PlayOpponentTokens(this, p2);

                return TokenSummary;

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
