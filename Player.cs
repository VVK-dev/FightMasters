using System.Text;
using static FightMasters.PlayHelper;

namespace FightMasters
{
    public class Player
    {
        //PROPERTIES

        public ICharClass ActiveCharClass { get; private set; }
        public string PlayerName { get; set; }

        public Queue<ICard> Deck { get; set; }

        public int ActiveHp { get; set; }
        public int CurrentStamina { get; set; }

        public IMinion?[] CurrentSummons { get; set; }

        public Dictionary<string, int> Resistances { get; set; } = new(4);

        public Dictionary<string, List<IToken>> TokensActive { get; set; } = new(4);
        //A player can have a max of 4 types of tokens active on them at any time

        internal int DodgeCounter { get; set; } = 0; 
        //Stamina is reduced next turn equal to the number of times a player has dodged this turn

        public event Func<(Player, Damage), Damage>? OnDealDamageCardEffects; 
        //params: opponent, damage value / returns: final damage value

        //CONSTRUCTORS

        public Player(ICharClass charClass, string playerName, Queue<ICard> deck)
        {

            this.PlayerName = playerName;

            this.ActiveHp = charClass.HealthPoints;
            this.CurrentStamina = charClass.MaxStamina;
            this.ActiveCharClass = charClass;

            this.Resistances.Add("Physical", charClass.PhysicalResistance);
            this.Resistances.Add("Fire", charClass.FireResistance);
            this.Resistances.Add("Frost", charClass.FrostResistance);
            this.Resistances.Add("Lightning", charClass.LightningResistance);
            this.Resistances.Add("Poison", charClass.PoisonResistance);

            this.CurrentSummons = new IMinion[charClass.SummonSlots];
            this.Deck = deck;
        }

        public Player()
        {

            this.PlayerName = "null";

            this.ActiveHp = 1;
            this.CurrentStamina = 1;
            this.ActiveCharClass = new Viking();

            this.Resistances.Add("Physical", 0);
            this.Resistances.Add("Fire", 0);
            this.Resistances.Add("Frost", 0);
            this.Resistances.Add("Lightning", 0);
            this.Resistances.Add("Poison", 0);

            this.CurrentSummons = Array.Empty<IMinion>();
            this.Deck = new Queue<ICard>();
        }

        //DAMAGE METHODS

        //Take Damage
        public void TakeDamage(Damage d)
        {

            //Calculate new hp

            int FinalDamageDealt = (int)d.DamageValue;

            this.ActiveHp -= FinalDamageDealt;

            return;

        }

        //Deal Damage
        public Damage BeforeDealingDamage(Damage IncomingDamage, Player opponent)
        {

                //Given that player hasn't dodged the instance of incoming damage, first calculate incoming damage

                IncomingDamage = CalculateDamage(IncomingDamage, opponent);

                //Next invoke event to call any methods that might trigger before damage is dealt

                Damage? EventResult = this.OnDealDamageCardEffects?.Invoke((this, IncomingDamage)); //returns this, opponent, final incoming damage

                //If the event has returned some values, set current player's OnDealDamageCardEffects event,
                //opponent and damage to values returned

                if (EventResult != null)
                {

                    IncomingDamage = EventResult;

                }


            return IncomingDamage;

        }

        //TOKEN METHODS

        //Add tokens

        public List<IToken> AddTokens(Dictionary<string, List<IToken>> tokens)
        {

            List<IToken> TokensAdded = new(); //List of tokens actually added to player from the incoming dictionary

            foreach (KeyValuePair<string, List<IToken>> TokenElement in tokens)
            {
                if (this.TokensActive.Count < 4) //A player can only have a max of 4 types of tokens active on them
                {

                    string key = TokenElement.Key;

                    if (this.TokensActive.ContainsKey(key))
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

                        int AmountToAdd = this.TokensActive[key].Count - capacity;
                        //Difference between {the amount of tokens of that same type already affecting character} and
                        //{max number of tokens of that type allowed on a character (i.e.capacity)} = amount of tokens
                        //from the incoming list of tokens that can actually be added to the character

                        if (AmountToAdd > 0)
                        {

                            for (int i = 0; i < AmountToAdd; i++)
                            {
                                this.TokensActive[key].Add(TokenElement.Value[i]);

                                TokensAdded.Add(TokenElement.Value[i]);

                            }

                        }

                    }
                    else
                    {

                        this.TokensActive.Add(key, TokenElement.Value);

                        TokensAdded.AddRange(TokenElement.Value);

                    }

                }
                else { break; } //If the player already has 4 types of tokens on them, do nothing

            }


            return TokensAdded;

        }

        //Token Effects

        public void ActivateBurnTokens()
        {
            //Check for burn tokens

            if (this.TokensActive.ContainsKey("<B>"))
            {

                for (int i = 0; i < this.TokensActive["<B>"].Count; i++)
                {
                    //For each burn token, player takes 2 damage. This damage ignores resistances.

                    this.ActiveHp -= 2;

                }

                //Remove all burn tokens by removing the burn key itself

                this.TokensActive.Remove("<B>");

            }

        }

        public void ActivateShockTokens()
        {
            //This method is called after a card is played

            //Check for shock tokens

            if (this.TokensActive.ContainsKey("<S>"))
            {

                foreach (ShockToken SToken in TokensActive["<S>"].Cast<ShockToken>())
                {
                    if (SToken.On)
                    {
                        //For each switched on shock token, player loses 20 lightning resistance.

                        this.Resistances["Lightning"] -= 20;

                        //Switch off the token after activating it

                        SToken.On = false;
                    }

                }

            }

        }

        public void DeactivateShockTokens()
        {

            /*Shock token removal is treated differently from other tokens. Only one "off" shock token is removed
              every turn from a player.*/


            if (this.TokensActive.ContainsKey("<S>"))
            {

                //LINQ gymnastics

                this.TokensActive["<S>"].Remove(this.TokensActive["<S>"]
                    .OfType<ShockToken>()
                    .First(SToken => !SToken.On)); //Removing first "off" shock token encountered

                //Revert player resistance after token removal

                this.Resistances["Lightning"] += 20;


            }

        }

        public void ActivateChillTokens()
        {
            //Check for chill tokens

            if (this.TokensActive.ContainsKey("<C>"))
            {

                for (int i = 0; i < this.TokensActive["<C>"].Count; i++)
                {
                    //For each chill token, player loses 1 stamina.

                    this.CurrentStamina--;

                }

                //Remove all chill tokens by removing the chill key itself

                this.TokensActive.Remove("<C>");

            }

        }

        public Damage ActivatePosionTokens(Damage damage)
        {

            //Check for poison tokens

            if (this.TokensActive.ContainsKey("<P>"))
            {

                for (int i = 0; i < this.TokensActive["<P>"].Count; i++)
                {
                    //For each poison token, reduce each instance of outgoing damage by 10%

                    damage.DamageValue -= damage.DamageValue / 10;

                    //For each poison token, make caster take 1 poison damage. This damage ignores resistances.

                    this.ActiveHp--;
                }

                /*Poison tokens are triggered on every instance of damage dealt by a player. Every time they are
                 triggered, one is removed. This is because a player will generally deal multiple instances of damage
                 per turn*/

                //Remove most recent poison token

                this.TokensActive["<P>"].RemoveAt(TokensActive["<P>"].Count - 1);

                //If there are no more poison tokens, remove the poison key

                if (this.TokensActive["<P>"].Count == 0) { this.TokensActive.Remove("<P>"); }

            }

            return damage;

        }

        public bool ActivateDodgeTokens()
        {

            //A dodge check happens once per instance of incoming damage and NOT once per token.

            if (this.TokensActive.ContainsKey("</>"))
            {
                //If TokensActive contains a key, that means that the length of the list that is the value of that key
                //is > 0. Therefore, the player has a dodge token.

                //Check if player dodges incoming damage

                if (new Random().Next(0, 2) == 1)
                {

                    this.DodgeCounter += 1;

                    //Remove most recent dodge token

                    this.TokensActive["</>"].RemoveAt(this.TokensActive["</>"].Count - 1);

                    //If no more dodge tokens, remove the key

                    if (this.TokensActive["</>"].Count == 0) { this.TokensActive.Remove("</>"); }

                    return true;

                }

            }

            return false;

        }

        public (Damage,bool) ActivateBlockTokens(Damage damage)
        {
            //If TokensActive contains a key, that means that the length of the list that is the value of that key
            //is > 0. Therefore, the player has a Block token.

            bool blocked = false;

            if (this.TokensActive.ContainsKey("<+>"))
            {

                damage.DamageValue /= 2;

                blocked = true;

                //Remove most recent Block token

                this.TokensActive["<+>"].RemoveAt(TokensActive["<+>"].Count - 1);

                //If there are no more Block tokens, remove the Block key

                if (this.TokensActive["<+>"].Count == 0) { this.TokensActive.Remove("<+>"); }

            }

            return (damage,blocked);

        }

        //VOICELINES

        public string PlayDamageVoiceLines(Player opponent, Damage[] DamageDone, bool DealingDamage)
        {

            string statement = "";

            //20% chance whenever a player deals or takes damage to trigger a voiceline

            if (new Random().Next(0, 5) == 1)
            {

                //Calculating the amount of damage done

                double TotalDamageDone = DamageDone.Sum(x => x.DamageValue);

                string DamageLevel = string.Empty;

                if (TotalDamageDone <= 5)
                {
                    DamageLevel = "LOW DAMAGE";

                }
                else if (TotalDamageDone <= 10)
                {
                    DamageLevel = "MEDIUM DAMAGE";

                }
                else if (TotalDamageDone > 10)
                {
                    DamageLevel = "HIGH DAMAGE";

                }

                //Getting voiceline from charclass

                string[] VoiceLines = Array.Empty<string>();

                if (DealingDamage)
                {

                    switch (opponent.ActiveCharClass)
                    {
                        case Viking:

                            VoiceLines = this.ActiveCharClass.OnDealDamageVoiceLines["Viking"][DamageLevel];
                            break;

                        case Herald:

                            VoiceLines = this.ActiveCharClass.OnDealDamageVoiceLines["Herald"][DamageLevel];
                            break;

                        case Rotcher:

                            VoiceLines = this.ActiveCharClass.OnDealDamageVoiceLines["Rotcher"][DamageLevel];
                            break;

                        case Druid:

                            VoiceLines = this.ActiveCharClass.OnDealDamageVoiceLines["Druid"][DamageLevel];
                            break;

                    }

                }
                else
                {

                    switch (opponent.ActiveCharClass)
                    {
                        case Viking:

                            VoiceLines = this.ActiveCharClass.OnTakeDamageVoiceLines["Viking"][DamageLevel];
                            break;

                        case Herald:

                            VoiceLines = this.ActiveCharClass.OnTakeDamageVoiceLines["Herald"][DamageLevel];
                            break;

                        case Rotcher:

                            VoiceLines = this.ActiveCharClass.OnTakeDamageVoiceLines["Viking"][DamageLevel];
                            break;

                        case Druid:

                            VoiceLines = this.ActiveCharClass.OnTakeDamageVoiceLines["Viking"][DamageLevel];
                            break;

                    }

                }

                //Choosing a random voiceline from array of voicelines

                int RandomIndex = new Random().Next(0, VoiceLines.Length);

                statement = VoiceLines[RandomIndex];
            }

            return statement;
        }

        //ToString

        public override string? ToString()
        {

            StringBuilder tokens = new();

            foreach (string token in TokensActive.Keys) {

                for (int i = 0; i < TokensActive[token].Count; i++)
                {
                    tokens.Append(token);

                }
            }

            StringBuilder summons = new();

            for (int i = 0; i < this.CurrentSummons.Length; i++)
            {
                summons.Append(CurrentSummons[i]!.ToString());

            }

            return $"{this.PlayerName} HP[{this.ActiveHp}] STAMINA[{this.CurrentStamina}] TOKENS[{tokens}] SUMMONS[{summons}]";
        
        }

    }

}
