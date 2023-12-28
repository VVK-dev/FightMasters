using System.ComponentModel;
using static FightMasters.NeutralCards;
using static FightMasters.VikingCards;

namespace FightMasters
{
    public abstract class PlayGame
    {

        static private Player Player1 { get; set; }
        static private Player Player2 { get; set; }

        //Neither player objects will ever actually be null

        public static void Game()
        {

            MatchSetup();

            Match();

        }

        /*PRE-MATCH SETUP METHODS*/

        //Method to set up a match
        private static void MatchSetup()
        {

            ICharClass Player1Class = ClassSetup(1);
            ICharClass Player2Class = ClassSetup(2);
            (string, string) PlayerNames = NameSetup();

            bool order = CoinFlip(PlayerNames);

            if (order)
            {
                //If the first player wins the toss, they are set to player 1 and the second player to player 2, i.e.
                //the first player goes first and the second player goes second

                Player1 = new Player(Player1Class, PlayerNames.Item1, PopulateDeck(Player1Class));
                Player2 = new Player(Player2Class, PlayerNames.Item2, PopulateDeck(Player2Class));
            }
            else
            {
                //If the first player loses the toss, they are set to player 2 and the second player to player 1, i.e.
                //the second player goes first and the first player goes second

                Player1 = new Player(Player2Class, PlayerNames.Item2, PopulateDeck(Player2Class));
                Player2 = new Player(Player1Class, PlayerNames.Item1, PopulateDeck(Player1Class));
            }

        }

        //Pre-match class choice
        private static ICharClass ClassSetup(int PlayerNum)
        {

            Console.WriteLine($"Player {PlayerNum}, please choose your class by entering the number associated with it:");

            Console.WriteLine("[1] Viking \n[2] Herald \n[3] Rotcher \n[4]Druid");

            int choice = Console.Read();

            switch (choice)
            {

                case 1: return new Viking();

                case 2: return new Herald();

                case 3: return new Rotcher();

                case 4: return new Druid();

                default:

                    Console.WriteLine("Invalid entry. Please try again.");
                    return ClassSetup(PlayerNum);

            }

        }

        //Pre-match set player names
        private static (string, string) NameSetup()
        {

            Console.WriteLine("Player 1, please enter your name: ");

            string p1Name = Console.ReadLine() ?? "Player 1"; 
            //If player 1 doesn't enter anything, set name to "Player 1" by default

            Console.WriteLine("Player 2, please enter your name: ");

            string p2Name = Console.ReadLine() ?? "Player 2";
            //If player 2 doesn't enter anything, set name to "Player 2" by default

            return (p1Name, p2Name);

        }

        //Coin Flip to decide who goes first
        private static bool CoinFlip((string,string) PlayerNames)
        {

            Console.WriteLine($"Flip a coin to see who goes first.");

            Console.WriteLine($"Do you pick heads or tails {PlayerNames.Item1}? Enter H for heads or T for tails." +
                $"If you do not enter either H or T, you will be auto assigned heads.");

            string input = Console.ReadLine() ?? "h"; //If p1 doesn't enter anything, they are auto-assigned heads

            //Numbered choices: 1 = heads, 2 = tails

            int p1choice = 1;

            if (input.ToLower().Contains('t'))
            {
                p1choice = 2;

            }

            int result = new Random().Next(1,3);

            if (result == p1choice) { 
                
                Console.WriteLine($"{PlayerNames.Item1} has won the toss. {PlayerNames.Item1} gets to go first"); 
                return true;
            }
            else
            {

                Console.WriteLine($"{PlayerNames.Item2} has won the toss. {PlayerNames.Item2} gets to go first");
                return false;

            }

        }

        //Create decks once player setups are complete
        private static Queue<ICard> PopulateDeck(ICharClass playerclass)
        {

            ICard[] Deck = {new Zap(), new DragonBreath(), new LFrostShield(), new BoulderToss() 
                    /*TO DO : Fill in with remaining cards*/
            };

            Deck = (ICard[]) Deck.Concat(playerclass.ClassCards);

            Deck = ShuffleArray(Deck);

            Queue<ICard> FinalDeck = new(Deck);

            return FinalDeck;

        }

        //Knuth shuffle algorithm to shuffle deck at start of match
        private static ICard[] ShuffleArray(ICard[] array)
        {
            Random random = new();

            for (int i = 0; i < array.Length; i++)
            {
                int j = random.Next(i, array.Length);

                // Swap elements at positions i and j
                (array[j], array[i]) = (array[i], array[j]);
            }

            return array;
        }

        /*IN MATCH METHODS*/

        //Match method
        private static void Match()
        {

            int rounds = 1;

            while (rounds <= 15)
            {

                //In a single round, there are 2 turns - one for each player

                //Player 1's turn
                Turn(Player1, Player2, Player1.GetHand());
                
                //Check win/draw conditions
                CheckWinCons();

                //Player 2's turn
                Turn(Player2, Player1, Player2.GetHand());
                
                //Check win/draw conditions
                CheckWinCons();

                rounds++;
            }

        }

        private static void CheckWinCons()
        {

            if ((Player1.ActiveHp == Player2.ActiveHp) && (Player1.ActiveHp < 1))
            {

                Console.WriteLine("\n === DRAW! === \n");

            }

            if (Player1.ActiveHp < 1)
            {

                Console.WriteLine("\n === PLAYER 2 WINS! === \n");

            }

            if (Player2.ActiveHp < 1)
            {

                Console.WriteLine("\n === PLAYER 1 WINS! === \n");

            }

            /*TO DO: SHOW MATCH STATS LIKE TOTAL DMG DEALT, ETC. AND ADD THEM TO PROFILE STATS */

            return;

        }

        //Turn method
        private static void Turn(Player CurrentPlayer, Player Opponent, ICard[] Hand)
        {

            //Call activate token methods and calculate stamina for this turn

            CurrentPlayer.CurrentStamina = CurrentPlayer.ActiveCharClass.MaxStamina;

            TokenHandler.ActivateBurnTokens(CurrentPlayer);
            CheckWinCons();

            TokenHandler.ActivateChillTokens(CurrentPlayer);

            //Deactivate persistent card effects

            for (int i = CurrentPlayer.PersistentCards.Count; i > 0; i--)
            {

                Console.WriteLine(CurrentPlayer.PersistentCards[i].DeactivateEffects(CurrentPlayer, Opponent)); 

            }

            //Player loses stamina for this turn if they successfully dodged last turn
            CurrentPlayer.CurrentStamina -= CurrentPlayer.DodgeCounter;
            CurrentPlayer.DodgeCounter = 0;

            Console.WriteLine(CurrentPlayer);

            PlayCards(CurrentPlayer, Opponent, Hand);

        }

        private static void PlayCards(Player CurrentPlayer, Player Opponent, ICard[] Hand)
        {

            //Loop through and print cards

            for (int index = 0; index < Hand.Length; index++)
            {
                if (Hand[index] is Dummy)
                {
                    Console.WriteLine("NO MORE CARDS LEFT IN DECK!");
                    break;
                }

                Console.WriteLine($"{index + 1}: {Hand[index]}");

            }

            Console.WriteLine($"{Hand.Length}: Pass Turn.");

            //Get player's choice and either pass turn or play card

            int choice = GetChoice(Hand, CurrentPlayer);

            if (choice == Hand.Length)
            {

                Console.WriteLine($"{CurrentPlayer.PlayerName} chooses to pass this turn.");
                EndTurn(Hand, CurrentPlayer, Opponent);
                return;

            }

            //If player chooses not to pass, play card chosen

            Hand[choice].Play(CurrentPlayer, Opponent);

            //If the card played has some persistent effect thatneeds to be deactivated after a while, add it
            //to the list of such cards

            if (Hand[choice].HasDeactivate) { CurrentPlayer.PersistentCards.Add(Hand[choice]); }

            //After card is played, set that index to dummy as it is not longer playable
            Hand[choice] = new Dummy();

            //If the player has enough stamina to still play at least 1 card from their hand, their turn
            //continues

            if (CurrentPlayer.CurrentStamina > Hand.Min(Card => Card.StaminaCost))
            {

                //Recurse, but remove the dummy card from their hand
                PlayCards(CurrentPlayer, Opponent, Hand.Where(Card => Card is not Dummy).ToArray());

                EndTurn(Hand, CurrentPlayer, Opponent);

                return;

            }

        }

        //Method to get a player's choice from their hand
        private static int GetChoice(ICard[] Hand, Player CurrentPlayer)
        {

            Console.WriteLine("Enter a number from those above to indicate how you choose to play: ");

            int choice = Console.Read();

            if((choice < 1) || (choice > Hand.Length) || (Hand[choice - 1].StaminaCost > CurrentPlayer.CurrentStamina))
            {

                Console.WriteLine("Invalid choice. Please try again.");

                return GetChoice(Hand, CurrentPlayer);
            }

            return choice;

        }

        /*END OF TURN METHODS*/

        //Method to call actions that occur at the end of a player's turn
        private static void EndTurn(ICard[] Hand, Player CurrentPlayer, Player Opponent)
        {

            PlaySummons(CurrentPlayer, Opponent);
            ReQueue(Hand, CurrentPlayer);

        }

        //At the end of a player's turn, all of their summons will act
        private static void PlaySummons(Player CurrentPlayer, Player Opponent)
        {

            for (int i = CurrentPlayer.CurrentSummons.Length - 1; i >= 0; i--)
            {

                ref IMinion? Summon = ref CurrentPlayer.CurrentSummons[i];

                //Using a pointer to the current summon for cleaner code, otherwise will have to call
                //CurrentPlayer.CurrentSummons[i] anytime I want to modify it

                if (Summon != null)
                {
                    if(Summon.Duration >= 1)
                    {

                        Summon.Act(CurrentPlayer, Opponent);
                        Summon.Duration--;

                    }
                    if (Summon.Duration < 1){ Summon = null; } 
                    
                    //Duration check happens both before and after summon has acted to prevent summon from acting if 
                    //duration is 0 or to remove summon from array if duration is 0 after acting

                }
            }
        }

        //Method to add unplayed cards from the player's hand back to their deck
        private static void ReQueue(ICard[] Hand, Player CurrentPlayer)
        {

            for (int i = 0; i < Hand.Length; i++)
            {
                CurrentPlayer.Deck.Enqueue(Hand[i]);

            }

        }

    }
}
