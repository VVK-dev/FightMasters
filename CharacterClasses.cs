using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        {"HIGH DAMAGE", new string[]{ "I thought I was the only one of my kind left. I'd like to keep it that way.",
                            "You call yourself one of my kind, eh? Well, let's see if you're telling the truth." }},
                        {"MEDIUM DAMAGE", new string[] {"We are not the same. You slay mere men - My axe has tasted the blood of gods.",
                            " " }},
                        {"LOW DAMAGE", new string[] {" I wonder if you'll break with just this much, copycat.",
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

        //Phys/lightning damage based mage summoner

        public string ClassName { get; } = "Druid";
        public int HealthPoints { get; } = 80;
        public int MaxStamina { get; } = 10;
        public int PhysicalResistance { get; } = 12;
        public int FireResistance { get; } = 10;
        public int FrostResistance { get; } = 10;
        public int LightningResistance { get; } = 10;
        public int PoisonResistance { get; } = 8;
        public int SummonSlots { get; } = 4;
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
