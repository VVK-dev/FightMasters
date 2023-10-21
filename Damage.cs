using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightMasters
{
    public class Damage
    {

        public string DamageType { get; set; }

        public double DamageValue { get; set; }

        public Damage(string type, double damage)
        {

            this.DamageType = type; 
            this.DamageValue = damage;

        }

        public Damage()
        {

            this.DamageType = "Physical";
            this.DamageValue = 1;

        }


        public override string ToString()
        {

            return $"{this.DamageValue} {this.DamageType}";

        }

    }
}
