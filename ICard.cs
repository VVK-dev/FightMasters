namespace FightMasters
{
    public interface ICard
    {
        //PROPERTIES

        string Name { get; }

        string Description { get; } //Description will include card's description and summons + their effects

        int StaminaCost { get; }

        Damage[]? DamageDealt { get; } //List of damage done by this card (as a single card can do multiple types of
                                       //damage with varying values)

        int Heal { get; }

        Dictionary<string, List<IToken>>? TokensAppliedCaster { get; }
        //Key = type of token applied as string, Value = tokens applied 
        Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; }
        //Key = type of token applied as string, Value = tokens applied 

        IMinion[]? Summons { get; }

        bool HasDeactivate { get; } //bool check for whether the card actually has any effects to deactivate
                                    //or not

        //METHODS

        //Play Method - Central method for a card. Dictates what happens when the card is played.
        public abstract string Play(Player p1, Player p2);

        //Deactivate Method - If the card has any effects that persist for some turns (eg: changing resistances), then
        //the deactivate method is called to undo these effects when the card's interal timer is over.
        public abstract string DeactivateEffects(Player p1, Player p2);

    }

    public abstract class CardPrinter
    {

        //ToString Method
        public static string PrintCard(ICard Card)
        {
            string print = $"[{Card.Name} - {Card.Description}]\n\t[(Stamina: {Card.StaminaCost})(Damage: ";

            //Damage

            if (Card.DamageDealt == null) { print += "NONE "; }
            else
            {
                foreach (Damage DamageItem in Card.DamageDealt)
                {

                    print += $"{DamageItem.DamageType} - {DamageItem.DamageValue})";

                }
            }

            print += "(Heal: ";

            if (Card.Heal > 0) { print += Card.Heal; }
            else { print += "NONE"; }

            print += ")";

            //Tokens

            print += "(Tokens Applied on You - ";

            if (Card.TokensAppliedCaster != null)
            {

                foreach (KeyValuePair<string, List<IToken>> token in Card.TokensAppliedCaster)
                {
                    print += $"{token.Key} : {token.Value.Count}";

                }

            }
            else { print += "NONE"; }

            print += "/ Tokens Applied on Opponent - ";

            if (Card.TokensAppliedOpponent != null)
            {

                for (int i = 0; i < Card.TokensAppliedOpponent.Count; i++)
                {
                    KeyValuePair<string, List<IToken>> token = Card.TokensAppliedOpponent.ElementAt(i);

                    print += $"{token.Key} : {token.Value.Count}";

                    if (i != Card.TokensAppliedOpponent.Count-1)
                    {
                        print += ",";
                        
                        //if not on the last element in dictionary, add comma to end of string

                    }

                }

            }
            else { print += "NONE "; }

            //Summons

            print += ")(Minions Summoned:";

            if (Card.Summons != null)
            {

                foreach (IMinion summon in Card.Summons)
                {

                    print += summon;

                }

            }
            else { print += " NONE"; }

            print += ")]\n";

            return print;

        }

    }

}