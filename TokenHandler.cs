namespace FightMasters
{
    internal abstract class TokenHandler
    {

        /*This class handles the addition, activation and deactivation of tokens on players*/

        //Add tokens

        public static List<IToken> AddTokens(Dictionary<string, List<IToken>> tokens, Player player)
        {

            List<IToken> TokensAdded = new(); //List of tokens actually added to player from the incoming dictionary

            foreach (KeyValuePair<string, List<IToken>> TokenElement in tokens)
            {
                if (player.TokensActive.Count < 4) //A player can only have a max of 4 types of tokens active on them
                {

                    string key = TokenElement.Key;

                    if (player.TokensActive.ContainsKey(key))
                    {
                        //A token of a particular type has a limit on how many times it can be applied on to a player

                        int capacity = 0;

                        switch (key)
                        {
                            case "<B>":
                                capacity = BurnToken.Capacity; break;
                            case "<S>":
                                capacity = ShockToken.Capacity; break;
                            case "<C>":
                                capacity = ChillToken.Capacity; break;
                            case "<P>":
                                capacity = PoisonToken.Capacity; break;
                            case "</>":
                                capacity = DodgeToken.Capacity; break;
                            case "<+>":
                                capacity = BlockToken.Capacity; break;

                        }

                        int AmountToAdd = player.TokensActive[key].Count - capacity;
                        //Difference between {the amount of tokens of that same type already affecting character} and
                        //{max number of tokens of that type allowed on a character (i.e.capacity)} = amount of tokens
                        //from the incoming list of tokens that can actually be added to the character

                        if (AmountToAdd > 0)
                        {

                            for (int i = 0; i < AmountToAdd; i++)
                            {
                                player.TokensActive[key].Add(TokenElement.Value[i]);

                                TokensAdded.Add(TokenElement.Value[i]);

                            }

                        }

                    }
                    else
                    {

                        player.TokensActive.Add(key, TokenElement.Value);

                        TokensAdded.AddRange(TokenElement.Value);

                    }

                }
                else { break; } //If the player already has 4 types of tokens on them, do nothing

            }


            return TokensAdded;

        }

        public static void ActivateBurnTokens(Player player)
        {
            //Check for burn tokens

            if (player.TokensActive.ContainsKey("<B>"))
            {

                foreach(BurnToken BToken in player.TokensActive["<B>"].Cast<BurnToken>())
                {
                    //For each burn token, player takes 2 damage. This damage ignores resistances.

                    player.TakeDamage(BToken.Damage);

                }

                //Remove all burn tokens by removing the burn key itself

                player.TokensActive.Remove("<B>");

            }

        }

        public static void ActivateShockTokens(Player player)
        {
            //player method is called after a card is played

            //Check for shock tokens

            if (player.TokensActive.ContainsKey("<S>"))
            {

                foreach (ShockToken SToken in player.TokensActive["<S>"].Cast<ShockToken>())
                {
                    if (SToken.On)
                    {
                        //For each switched on shock token, player loses 20 lightning resistance.

                        player.Resistances["Lightning"] -= 20;

                        //Switch off the token after activating it

                        SToken.On = false;
                    }

                }

            }

        }

        public static void DeactivateShockTokens(Player player)
        {

            /*Shock token removal is treated differently from other tokens. Only one "off" shock token is removed
              every turn from a player.*/


            if (player.TokensActive.ContainsKey("<S>"))
            {

                //LINQ gymnastics

                player.TokensActive["<S>"].Remove(player.TokensActive["<S>"]
                    .OfType<ShockToken>()
                    .First(SToken => !SToken.On)); //Removing first "off" shock token encountered

                //Revert player resistance after token removal

                player.Resistances["Lightning"] += 20;


            }

        }

        public static void ActivateChillTokens(Player player)
        {
            //Check for chill tokens

            if (player.TokensActive.ContainsKey("<C>"))
            {

                for (int i = 0; i < player.TokensActive["<C>"].Count; i++)
                {
                    //For each chill token, player loses 1 stamina.

                    player.CurrentStamina--;

                }

                //Removing the chill key to remove all activated chill tokens

                player.TokensActive.Remove("<C>");

            }

        }

        public static Damage ActivatePosionTokens(Player player, Damage damage)
        {

            //Check for poison tokens

            if (player.TokensActive.ContainsKey("<P>"))
            {

                for (int i = 0; i < player.TokensActive["<P>"].Count; i++)
                {
                    //For each poison token, reduce each instance of outgoing damage by 10%

                    damage.DamageValue -= damage.DamageValue / 10;

                    //For each poison token, make caster take 1 poison damage; this damage ignores resistances.
                    //But because this damage is not being resisted at all, the actual type of damage being
                    //taken is irrelevant, so I've just used the default constructor for damage here.

                    player.TakeDamage(new Damage());

                }

                /*Poison tokens are triggered on every instance of damage dealt by a player. Every time they are
                 triggered, one is removed. player is because a player will generally deal multiple instances of damage
                 per turn*/

                //Remove most recent poison token

                player.TokensActive["<P>"].RemoveAt(player.TokensActive["<P>"].Count - 1);

                //If there are no more poison tokens, remove the poison key

                if (player.TokensActive["<P>"].Count == 0) { player.TokensActive.Remove("<P>"); }

            }

            return damage;

        }

        public static bool ActivateDodgeTokens(Player player)
        {

            //A dodge check happens once per instance of incoming damage and NOT once per token.

            if (player.TokensActive.ContainsKey("</>"))
            {
                //If TokensActive contains a key, that means that the length of the list that is the value of that key
                //is > 0. Therefore, the player has a dodge token.

                //Check if player dodges incoming damage

                if (new Random().Next(0, 2) == 1)
                {

                    player.DodgeCounter += 1;

                    //Remove most recent dodge token

                    player.TokensActive["</>"].RemoveAt(player.TokensActive["</>"].Count - 1);

                    //If no more dodge tokens, remove the key

                    if (player.TokensActive["</>"].Count == 0) { player.TokensActive.Remove("</>"); }

                    return true;

                }

            }

            return false;

        }

        public static (Damage, bool) ActivateBlockTokens(Player player, Damage damage)
        {
            //If TokensActive contains a key, that means that the length of the list that is the value of that key
            //is > 0. Therefore, the player has a Block token.

            bool blocked = false;

            if (player.TokensActive.ContainsKey("<+>"))
            {

                damage.DamageValue /= 2;

                blocked = true;

                //Remove most recent Block token

                player.TokensActive["<+>"].RemoveAt(player.TokensActive["<+>"].Count - 1);

                //If there are no more Block tokens, remove the Block key

                if (player.TokensActive["<+>"].Count == 0) { player.TokensActive.Remove("<+>"); }

            }

            return (damage, blocked);

        }

    }
}
