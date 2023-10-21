using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FightMasters
{
    public interface ICharClass
    {
        //PROPERTIES

        //Important
        public string ClassName { get; }
        public int HealthPoints { get; }
        public int MaxStamina { get; }

        //Resistances
        public int PhysicalResistance { get; }
        public int FireResistance { get; }
        public int FrostResistance { get; }
        public int LightningResistance { get; }
        public int PoisonResistance { get; }

        //Misc
        public int SummonSlots { get; }
        public ICard[] ClassCards{ get; }

        public Dictionary<string, Dictionary<string, string[]>> OnDealDamageVoiceLines { get; }
        
        //KEY - deal damage to which opponent class, KEY - big/low/medium damage, VAL - list of corresponding
        //voicelines

        public Dictionary<string, Dictionary<string, string[]>> OnTakeDamageVoiceLines { get; }

    }

}
