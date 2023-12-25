﻿using System.Text;
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

        //Event Call Before Dealing Damage
        public Damage BeforeDealingDamage(Damage IncomingDamage, Player opponent)
        {

                //Calculate incoming damage after accounting for resistances

                IncomingDamage = CalculateDamage(IncomingDamage, opponent);

                //Invoke OnDealDamageCardEffects event to call any methods that might trigger before final
                //damage is dealt

                Damage? EventResult = this.OnDealDamageCardEffects?.Invoke((this, IncomingDamage)); 

                if (EventResult != null)
                {

                    IncomingDamage = EventResult;

                }


            return IncomingDamage;

        }

        //TO DO: Move this token method to token handler

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

        //VOICELINES

        public string PlayDamageVoiceLines(Player opponent, Span<Damage> DamageDone, bool DealingDamage)
        {

            string statement = string.Empty;

            //20% chance whenever a player deals or takes damage to trigger a voiceline

            if (new Random().Next(0, 5) == 1)
            {

                //Calculating the amount of damage done

                double TotalDamageDone = DamageDone.ToArray().Sum(x => x.DamageValue);

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

                statement = VoiceLines[RandomIndex] + "\n";
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
