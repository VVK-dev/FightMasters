using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FightMasters
{
    internal abstract class PlayHelper
    {
        //THIS CLASS IS STORAGE FOR DEFAULT PLAY METHODS AVAILABLE TO ALL CARDS AND MINIONS

        //p1 refers to player 1, i.e. caster of the card i.e. the one who plays the card
        //p2 refers to player 2, i.e. target of the card i.e. the opponent of the one who plays the card

        //Damage only play method

        public static string PlayDamage<T>(T Item, Player p1, Player p2) where T : ICard, IMinion
        {
            //This method can be used by both cards and minions

            Damage[]? DamageArray;

            if (Item is IMinion minion)
            {
                DamageArray = minion.DamageDealt;
            }
            else if (Item is ICard card)
            {
                DamageArray = card.DamageDealt;
            }

            string PlayDamageSummary = "";

            if (DamageArray != null)
            {

                //Before damage is actually dealt, the caster may say something (a voiceline may be triggered)

                PlayDamageSummary += p1.PlayDamageVoiceLines(p2, Item.DamageDealt, true);

                //Iterating through the damage array of the item

                for (int i = 0; i < Item.DamageDealt.Length; i++)
                {

                    ref Damage CurrentDamage = ref Item.DamageDealt[i];

                    //Check for Dodge Tokens

                    if (p2.ActivateDodgeTokens())
                    {

                        PlayDamageSummary += $"{p2.PlayerName} dodges the incoming {CurrentDamage} " +
                            $"damage. ";

                        return PlayDamageSummary;

                    }

                    //Check for poison tokens on player 1

                    if (p1.TokensActive.ContainsKey("<P>"))
                    {

                        CurrentDamage = p1.ActivatePosionTokens(CurrentDamage);

                        PlayDamageSummary += $"{p1.PlayerName} takes 1 damage from an active poison token." +
                            $" The damage {p1.PlayerName} tries to deal is reduced by 10%, reducing it to " +
                            $"{CurrentDamage} damage. ";

                    }

                    //Deal damage

                    CurrentDamage = p1.BeforeDealingDamage(CurrentDamage, p2);
                    
                    if ()
                    {

                        IncomingDamage = this.ActivateBlockTokens(IncomingDamage);

                     

                    }

                    if (p2.TokensActive.ContainsKey("<+>"))
                    {

                        PlayDamageSummary += $"{p2.PlayerName} expends a block token to reduce the amount of " +
                            $"damage they take by 50%, reducing the incoming instance of damage to " +
                            $"{CurrentDamage} damage. ";

                    }

                    PlayDamageSummary += $"{p2.PlayerName} takes {CurrentDamage} damage. ";


                }

                //After all damage is dealt, the opponent may say something (a voiceline may be triggered)

                PlayDamageSummary += p2.PlayDamageVoiceLines(p1, Item.DamageDealt, false);

            }

            return PlayDamageSummary;

        }

        //Method to heal player

        public static string PlayHeal(ICard Card, Player p1)
        {

            string HealSummary = "";

            if (Card.Heal > 0)
            {

                p1.ActiveHp += Card.Heal;

                HealSummary = $"{p1.PlayerName} heals for {Card.Heal} ";

            }

            return HealSummary;

        }

        //Method to apply tokens to caster

        public static string PlayCasterTokens(ICard Card, Player p1)
        {

            string CasterTokenSummary = "";

            if (Card.TokensAppliedCaster != null)
            {
                List<IToken> AddedTokens = p1.AddTokens(Card.TokensAppliedCaster);

                foreach (IToken Token in AddedTokens)
                {
                    CasterTokenSummary += $"{Token} applied to {p1.PlayerName}  ";

                }

            }

            return CasterTokenSummary;

        }

        //Method to apply tokens to opponent

        public static string PlayOpponentTokens(ICard Card, Player p2)
        {

            string OpponentTokenSummary = "";

            if (Card.TokensAppliedOpponent != null)
            {
                List<IToken> AddedTokens = p2.AddTokens(Card.TokensAppliedOpponent);

                foreach (IToken Token in AddedTokens)
                {
                    OpponentTokenSummary += $"{Token} applied to {p2.PlayerName} ";

                }

            }

            return OpponentTokenSummary;
        }

        //Method to summon minions

        public static string SummonMinions(ICard Card, Player p1)
        {

            string MinionsSummonedSummary = "";

            if (Card.Summons != null)
            {

                for (int i = 0; i < p1.CurrentSummons.Length; i++)
                {

                    if (p1.CurrentSummons[i] == null) //Only fill in empty slots
                    {

                        p1.CurrentSummons[i] = Card.Summons[i];

                        MinionsSummonedSummary += $"{p1.PlayerName} summons {p1.CurrentSummons[i]} ";

                    }

                }

            }

            return MinionsSummonedSummary;

        }


        //Method to calculate damage dealt to player after resistances

        public static Damage CalculateDamage(Damage d, Player p2) //p2 is the caster's (i.e. p1's) opponent
        {

            //Getting corresponding resistance to incoming damage type

            int correspondingresist = 0;

            if (d.DamageType == "Physical")
            {
                correspondingresist = p2.Resistances["Physical"];

            }
            if (d.DamageType == "Fire")
            {
                correspondingresist = p2.Resistances["Fire"];

            }
            if (d.DamageType == "Frost")
            {
                correspondingresist = p2.Resistances["Frost"];

            }
            if (d.DamageType == "Lightning")
            {
                correspondingresist = p2.Resistances["Lightning"];

            }
            if (d.DamageType == "Poison")
            {
                correspondingresist = p2.Resistances["Poison"];

            }

            //Damage Calculation

            d.DamageValue -= d.DamageValue / correspondingresist;

            d.DamageValue = Math.Floor(d.DamageValue);

            return d;

        }

    }

}
