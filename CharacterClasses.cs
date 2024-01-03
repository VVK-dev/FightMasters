namespace FightMasters
{

    //Character Classes

    public class Viking : ICharClass
    {
        //Frost and physical damage based Tank

        public string ClassName { get; } = "Viking";
        public int HealthPoints { get; } = 100;
        public int MaxStamina { get; } = 10;
        public int PhysicalResistance { get; } = 20;
        public int FireResistance { get; } = 8;
        public int FrostResistance { get; } = 20;
        public int LightningResistance { get; } = 12;
        public int PoisonResistance { get; } = 10;
        public int SummonSlots { get; } = 3;
        public ICard[] ClassCards { get; } = { new VikingCards.IceArmour(), new VikingCards.HeavySwing() };

        public Dictionary<string, Dictionary<string, string[]>> OnDealDamageVoiceLines { get; } =
        new() {

            { "Viking" , new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'I thought I was the only one of my kind left. I'd like to keep it that way.'",
                        "'You call yourself one of my kind, eh? Well, let's see if you're telling the truth.'" }},
                    {"MEDIUM DAMAGE", new string[] {"'We are not the same. You slay mere men - My axe has tasted the blood of gods.'",
                        "'SHATTER!'" }},
                    {"LOW DAMAGE", new string[] {"'I wonder if you'll break with just this much, copycat?'",
                        "'Whether or not you're my kin, I cannot let you live.'"}}
                }
            },

            { "Rotcher", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'May your bones shatter.'", "'Die, vile creature!'" }},
                    {"MEDIUM DAMAGE", new string[] {"'I can smell you from here!'", "'I wil end you foul beast!'" }},
                    {"LOW DAMAGE", new string[] {"'Can your fetid body handle this?'", "'Return to the grave from which you came.'"}}
                }
            },

            { "Herald", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'Forgive me. May your soul find peace in the afterlife.'", "'You made me do this. Face the consequences.'" }},
                    {"MEDIUM DAMAGE", new string[] { "'Give up. I'd rather you surrender than me strike you down.'", "'We we friends once. Don't make me hurt you.'" }},
                    {"LOW DAMAGE", new string[] {"'You've forced my hand.'", "'May your soul keep you warm at the end of the world.'"}}
                }
            },

            { "Druid", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'Don't take this personally.'", "I have no enemity with you. You're simply in my way." }},
                    {"MEDIUM DAMAGE", new string[] {"'This is a warning. Surrender.'", "'Your creatures cannot save you. You will shatter.'"}},
                    {"LOW DAMAGE", new string[] {"'You can summon as many beasts as you want, I shall cull them all.'", "'Your beasts cannot stop my axe.'"}}
                }
            }
        };

        public Dictionary<string, Dictionary<string, string[]>> OnTakeDamageVoiceLines { get; } =
        new() {

            { "Viking" , new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'AAARHHHhh...that was...a good...hit... *spits blood*.'"}},
                    {"MEDIUM DAMAGE", new string[] {"'OOF...that's quite the axe you have.'"} },
                    {"LOW DAMAGE", new string[] {"'I can walk this off.'"} }
                }
            },

            { "Rotcher", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'...What is this?! I can barely feel my arms!'"}},
                    {"MEDIUM DAMAGE", new string[] {"'Get your filth off of me!'", "'What did you do to me you repulsive creature?!'"}},
                    {"LOW DAMAGE", new string[] {"'Your poisons will not affect me...probably'"}}
                }
            },

            { "Herald", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'You've... made me bleed... I didn't think I still could...'", "'My skin... IT'S CHARRED!'"}},
                    {"MEDIUM DAMAGE", new string[] {"'Your flame...*cough*... still burns strong I see. Good.'"}},
                    {"LOW DAMAGE", new string[] {"'You cannot set a blizzard aflame.'"}}
                }
            },

            { "Druid", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'*cough*..Your own claws are even sharper than your creatures it would *cough* seem...'"} },
                    {"MEDIUM DAMAGE", new string[] {"'You fight well, beastmaster.'"}},
                    {"LOW DAMAGE", new string[] {"'Congratulations. You grazed me.'"}}
                }
            }
        };


        //Constructor
        public Viking() { }

    }

    public class Herald : ICharClass
    {

        //Fire and Burn damage based samurai

        public string ClassName { get; } = "Herald";
        public int HealthPoints { get; } = 85;
        public int MaxStamina { get; } = 9;
        public int PhysicalResistance { get; } = 14;
        public int FireResistance { get; } = 20;
        public int FrostResistance { get; } = 5;
        public int LightningResistance { get; } = 10;
        public int PoisonResistance { get; } = 7;
        public int SummonSlots { get; } = 2;
        public ICard[] ClassCards { get; } = { /*TODO: Fill this in once cards are made*/};

        public Dictionary<string, Dictionary<string, string[]>> OnDealDamageVoiceLines { get; } =
        new() {

            { "Viking" , new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'MELT.'", "'TURN TO ASH.'" }},
                    {"MEDIUM DAMAGE", new string[] {"'May my flames consume you.'", "'I despise you.'" }},
                    {"LOW DAMAGE", new string[] {"'Can you feel it? The heat is rising.'", "'I will turn you to cinders.'"}}
                }
            },

            { "Rotcher", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'BURN ASUNDER.'", "'I WILL BURN YOU AND THE GRAVE FROM WHICH YOU AROSE.'" }},
                    {"MEDIUM DAMAGE", new string[] {"'You're much more pleasing to the eye when you're on fire.'", "'Explode, flith.'" }},
                    {"LOW DAMAGE", new string[] {"'May my flames cleanse this world of you.'", "'Ignite.'"}}
                }
            },

            { "Herald", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'MAY YOU BURN IN YOUR NEXT LIFE AS WELL, IMPOSTER.'", "'THERE CAN BE ONLY ONE. ONLY ME.'" }},
                    {"MEDIUM DAMAGE", new string[] {"'IGNITE, FAKER.'", "'IMMOLATE.'" }},
                    {"LOW DAMAGE", new string[] {"'May my flames burn your impurity.'", "'Catch fire.'"}}
                }
            },

            { "Druid", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'I'LL BURN YOUR BEASTS WITH YOU.'", "'LET MY FLAMES TURN YOU AND YOUR CREATURES TO THE DARKEST OF ASH.'" }},
                    {"MEDIUM DAMAGE", new string[] {"'YOU MAKE FOR GREAT TINDER.'", "'I WILL RAZE THE GROUND THE BENEATH YOU.'" }},
                    {"LOW DAMAGE", new string[] {"'I will set you ablaze.'", "'You are not prepared for what's to come.'"}}
                }
            }
        };

        public Dictionary<string, Dictionary<string, string[]>> OnTakeDamageVoiceLines { get; } =
        new() {

            { "Viking" , new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'AAAAAHHHHH! DAMN YOU! DAMN YOU TO HELL AND BACK!'", "'YOU WILL FEEL MY VENGENACE FOR THIS I SWEAR UPON THE VERY FLAMES THAT COURSE THROUGH ME.'" }},
                    {"MEDIUM DAMAGE", new string[] {"'Tch.'"}},
                    {"LOW DAMAGE", new string[] {"'Your ice cannot break through my wall of flames.'"}}
                }
            },

            { "Rotcher", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'MY EYES! THEY BURRRRNNN!'"}},
                    {"MEDIUM DAMAGE", new string[] {"'JUST YOU WAIT YOU FETID RAT. I WILL HAVE MY REVENGE.'"}},
                    {"LOW DAMAGE", new string[] {"'Cheap poisons.'", "'Filthy creature.'"}}
                }
            },

            { "Herald", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{ "'WHAT IS THIS?! IM BURNING!'", "''" }},
                    {"MEDIUM DAMAGE", new string[] {"'MAY YOUR BLADE CHIP AND SHATTER, FAKER.'", "'YOU DARE?!'" }},
                    {"LOW DAMAGE", new string[] {"'Your false flames cannot harm me.'"}}
                }
            },

            { "Druid", new Dictionary<string, string[]>()
                {
                    {"HIGH DAMAGE", new string[]{"'...TR-TRY *cough*...HARDER...'", "'Mad...*cough*...beasts...'" }},
                    {"MEDIUM DAMAGE", new string[] {"'I WILL TURN YOU TO ASH FOR THAT. MARK MY WORDS.'", "'YOU AND YOUR BEASTS CLAWS WILL BURN IF THEY DIG ANY DEEPER.'" }},
                    {"LOW DAMAGE", new string[] {"'Your creatures are weak, just like their master.'"}}
                }
            }
        };

        //Constructor
        public Herald() { }

    }

    public class Rotcher : ICharClass
    {

        //Poison and plague damage based archer

        public string ClassName { get; } = "Rotcher";
        public int HealthPoints { get; } = 70;
        public int MaxStamina { get; } = 11;
        public int PhysicalResistance { get; } = 9;
        public int FireResistance { get; } = 9;
        public int FrostResistance { get; } = 10;
        public int LightningResistance { get; } = 20;
        public int PoisonResistance { get; } = 10;
        public int SummonSlots { get; } = 3;
        public ICard[] ClassCards { get; } = { /*TODO: Fill this in once cards are made*/};

        public Dictionary<string, Dictionary<string, string[]>> OnDealDamageVoiceLines { get; } =
            new() {

                { "Viking" , new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "I thought I was the only one of my kind left. I'd like to keep it that way.",
                            "You call yourself one of my kind, eh? Well, let's see if you're telling the truth." }},
                        {"MEDIUM DAMAGE", new string[] {"We are not the same. You slay mere men - My axe has tasted the blood of gods.",
                            " " }},
                        {"LOW DAMAGE", new string[] {" I wonder if you'll break with just this much, copycat?",
                            " "}}
                    }
                },

                { "Rotcher", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Herald", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Druid", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                }
            };

        public Dictionary<string, Dictionary<string, string[]>> OnTakeDamageVoiceLines { get; } =
            new() {

                { "Viking" , new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "I thought I was the only one of my kind left. I'd like to keep it that way.",
                            "You call yourself one of my kind, eh? Well, let's see if you're telling the truth." }},
                        {"MEDIUM DAMAGE", new string[] {"We are not the same. You slay mere men - My axe has tasted the blood of gods.",
                            " " }},
                        {"LOW DAMAGE", new string[] {" I wonder if you'll break with just this much, copycat?",
                            " "}}
                    }
                },

                { "Rotcher", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Herald", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Druid", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                }
            };

        //Constructor
        public Rotcher() { }
    }


    public class Druid : ICharClass
    {

        //Summoner-Mage

        public string ClassName { get; } = "Druid";
        public int HealthPoints { get; } = 80;
        public int MaxStamina { get; } = 10;
        public int PhysicalResistance { get; } = 12;
        public int FireResistance { get; } = 10;
        public int FrostResistance { get; } = 10;
        public int LightningResistance { get; } = 10;
        public int PoisonResistance { get; } = 8;
        public int SummonSlots { get; } = 4;
        public ICard[] ClassCards { get; } = { new NeutralCards.SummonWolf() /*TODO: Fill this in once cards are made*/};

        //Although Summon wolf is a neutral card, the druid gets an extra copy of it as a class card

        public Dictionary<string, Dictionary<string, string[]>> OnDealDamageVoiceLines { get; } =
            new() {

                { "Viking" , new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "I thought I was the only one of my kind left. I'd like to keep it that way.",
                            "You call yourself one of my kind, eh? Well, let's see if you're telling the truth." }},
                        {"MEDIUM DAMAGE", new string[] {"We are not the same. You slay mere men - My axe has tasted the blood of gods.",
                            " " }},
                        {"LOW DAMAGE", new string[] {" I wonder if you'll break with just this much, copycat?",
                            " "}}
                    }
                },

                { "Rotcher", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Herald", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Druid", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                }
            };

        public Dictionary<string, Dictionary<string, string[]>> OnTakeDamageVoiceLines { get; } =
            new() {

                { "Viking" , new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "I thought I was the only one of my kind left. I'd like to keep it that way.",
                            "You call yourself one of my kind, eh? Well, let's see if you're telling the truth." }},
                        {"MEDIUM DAMAGE", new string[] {"We are not the same. You slay mere men - My axe has tasted the blood of gods.",
                            " " }},
                        {"LOW DAMAGE", new string[] {" I wonder if you'll break with just this much, copycat?",
                            " "}}
                    }
                },

                { "Rotcher", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Herald", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                },

                { "Druid", new Dictionary<string, string[]>()
                    {
                        {"HIGH DAMAGE", new string[]{ "", "" }},
                        {"MEDIUM DAMAGE", new string[] {" ", " " }},
                        {"LOW DAMAGE", new string[] {" ", " "}}
                    }
                }
            };

        //Constructor
        public Druid() { }

    }

    /*

      A class that switches between priest and vampire stances. To be implemented at a later date.

        public class Split : CharClass
        {

            //Stance switcher

            public Split() : base(75, 10, 9, 15, 5, 9, 4, 2)
            {
                this.ClassCards = new Card[] { };
            }

            private void Switch()
            {
                // Switch to Vampire stance

                PhysicalResistance = 15;
                FireResistance = 5;
                FrostResistance = 9;
                PoisonResistance = 20;
                SummonSlots = 3;
            }
        }
    */
}
