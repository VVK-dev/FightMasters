﻿namespace FightMasters
{
    internal abstract class PlayHelper
    {
        //THIS CLASS IS STORAGE FOR DEFAULT PLAY METHODS AVAILABLE TO ALL CARDS AND MINIONS

        //p1 refers to player 1, i.e. caster of the card i.e. the one who plays the card
        //p2 refers to player 2, i.e. target of the card i.e. the opponent of the one who plays the card


        //Method to deal damage only

        public static string DamagePlayer(dynamic Item, Player p1, Player p2)
        {
            //This method can be used by both cards and minions

            bool IsCard = ObjCheck(Item);

            Span<Damage> DamageSpan = new (Item.DamageDealt);

            string PlayDamageSummary = string.Empty;

            if (DamageSpan != null)
            {

                //Before damage is actually dealt, the caster may say something (a voiceline may be triggered)
                //Voicelines are only triggered on player based damage from cards, not minions

                if(IsCard) { PlayDamageSummary += p1.PlayDamageVoiceLines(p2, DamageSpan, true); }

                //Iterating through the damage array of the item

                for (int i = 0; i < DamageSpan.Length; i++)
                {

                    Damage CurrentDamage = DamageSpan[i];

                    //Check for Dodge Tokens

                    if (TokenHandler.ActivateDodgeTokens(p2))
                    {

                        PlayDamageSummary += $"{p2.PlayerName} dodges the incoming {CurrentDamage} " +
                            $"damage. ";

                        continue; //If this instance of damage is dodged, move to next instance

                    }

                    //Check for poison tokens on player 1

                    if (IsCard) //Poison tokens are only triggered on player based damage from cards, not minions
                    {

                        CurrentDamage = TokenHandler.ActivatePosionTokens(p1, CurrentDamage);

                        PlayDamageSummary += $"{p1.PlayerName} takes 1 damage from an active poison token." +
                            $" The damage {p1.PlayerName} tries to deal is reduced by 10%, reducing it to " +
                            $"{CurrentDamage} damage. ";

                    }

                    //Check for pre-damage event

                    CurrentDamage = p1.BeforeDealingDamage(CurrentDamage, p2);

                    //Check for block tokens

                    (CurrentDamage, bool blocked) = TokenHandler.ActivateBlockTokens(p2, CurrentDamage);

                    if(blocked)
                    {
                        PlayDamageSummary += $"{p2.PlayerName} expends a block token to reduce the amount of " +
                        $"damage they take by 50%, reducing the incoming instance of damage to " +
                        $"{CurrentDamage} damage. ";
                    }

                    //Deal damage

                    p2.TakeDamage(CurrentDamage);

                    PlayDamageSummary += $"{p2.PlayerName} takes {CurrentDamage} damage. ";

                }

                //After all damage is dealt, the opponent may say something (a voiceline may be triggered)

                PlayDamageSummary += p2.PlayDamageVoiceLines(p1, DamageSpan, false);

            }

            return PlayDamageSummary;

        }

        //Method to heal player

        public static string HealPlayer(ICard Card, Player p1)
        {

            string HealSummary = "";

            if (Card.Heal > 0)
            {

                p1.ActiveHp += Card.Heal;

                HealSummary = $"{p1.PlayerName} heals for {Card.Heal} ";

            }

            return HealSummary;

        }

        //Method to add tokens to caster

        public static string AddCasterTokens(ICard Card, Player p1)
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

        public static string AddOpponentTokens(ICard Card, Player p2)
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

        //Method to check if incoming object is card or summon

        public static bool ObjCheck(object Item)
        {

            bool IsCard = false;

            if (Item is IMinion) { return IsCard; }
            else if (Item is ICard)
            {

                IsCard = true;

                return IsCard;

            }
            //TO DO: Create custom exception for this case

            else { throw new Exception("The PlayDamage method cannot accept an object that doesn't implement either IMinion or ICard."); }

        }

        //Method to calculate damage dealt to player after resistances

        public static Damage CalculateDamage(Damage d, Player p2) //p2 is the caster's (i.e. p1's) opponent
        {

            //Getting corresponding resistance to incoming damage type

            int correspondingresist = 0;

            switch (d.DamageType)
            {

                case "Physical":
                    correspondingresist = p2.Resistances["Physical"];
                    break;

                case "Fire":
                    correspondingresist = p2.Resistances["Fire"];
                    break;

                case "Frost":
                    correspondingresist = p2.Resistances["Frost"];
                    break;

                case "Lightning":
                    correspondingresist = p2.Resistances["Lightning"];
                    break;

                case "Poison":
                    correspondingresist = p2.Resistances["Poison"];
                    break;

                //Currently, if none of the cases are satisfied then correspondingresist is left at 0.
                //May change this in the future to throw an exception in this default case.
            }

            //Damage Calculation

            d.DamageValue -= d.DamageValue / correspondingresist;

            d.DamageValue = Math.Floor(d.DamageValue);

            return d;

        }

    }

}
