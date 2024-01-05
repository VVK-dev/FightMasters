namespace FightMasters
{
    public interface IMinion : IPlayable
    {

        //PROPERTIES

        int Duration { get; set; } //How many turns this minion lasts for

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

