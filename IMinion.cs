namespace FightMasters
{
    public interface IMinion
    {

        //PROPERTIES

        string Name { get; }

        int Duration { get; set; } //How many turns this minion lasts for

        Damage[]? DamageDealt { get; } //List of damage done by this card (as a single card can do multiple types of
                                      //damage with varying values)

        int Heal { get; } //Healing done per round

        Dictionary<string, List<IToken>>? TokensAppliedCaster { get; }
        //Key = type of token applied as string, Value = tokens applied 
        Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; }
        //Key = type of token applied as string, Value = tokens applied 

        //METHODS

        //Act Method - Defines what happens when a minion acts
        public abstract string Act(Player p1, Player p2);

    }

    public abstract class MinionPrinter
    {

        //ToString Method

        public static string PrintMinion(IMinion Minion)
        {
            string print = $"[{Minion.Name}| Duration: {Minion.Duration}]\n";

            return print;

        }

    }
}

