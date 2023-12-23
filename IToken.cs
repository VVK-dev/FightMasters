using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FightMasters
{
    public interface IToken
    {

        public Damage? Damage { get; }

        public static int Capacity { get; } //Number of max tokens of this type that can be applied on a character

    }

    //TOKENS

    //1 - DEBUFFS

    public class BurnToken : IToken
    {

        //Deals 2 damage per round. Lasts 1 round (2 turns). More tokens = more damage. Max 5 stacks.

        public Damage Damage { get; } = new Damage ("Fire", 2);

        public static int Capacity { get; } = 5;


        public BurnToken() {}

        public override string? ToString()
        {
            return "<B>";
        }

    }

    class ShockToken : IToken
    {

        //Reduces lightning resist by 20%. Lasts until tokens are over. Stacks with more tokens. Max 2 stacks.

        public Damage? Damage { get; } = null;

        public static int Capacity { get; } = 3;

        public bool On { get; set; } = true;

        public ShockToken() { }

        public override string? ToString()
        {
            return "<S>";
        }


    }
    
    class ChillToken : IToken
    {

        //Reduces stamina next turn by 1. Lasts 1 round. Stacks with more tokens. Max 2 stacks.

        public Damage? Damage { get; } = null;

        public static int Capacity { get; } = 2;


        public ChillToken() { }

        public override string? ToString()
        {
            return "<C>";
        }

    }

    class PoisonToken : IToken
    {
        //Deals 1 damage everytime that player tries to do damage and reduces their damage output by 10%.
        //Lasts 1 round. More tokens = stacking effects. Max 3 stacks.

        public Damage Damage { get; } = new Damage("Physical", 1);

        public static int Capacity { get; } = 3;


        public PoisonToken() {}

        public override string? ToString()
        {
            return "<P>";
        }

    }

    //Token damage pierces resistances.

    //2- BUFFS

    class BlockToken : IToken
    {
        //Reduces incoming damage by 50%. Lasts until player blocks. More tokens = more rounds. Max 2 stacks.

        public Damage? Damage { get; } = null;

        public static int Capacity { get; } = 2;


        public BlockToken() { }

        public override string? ToString()
        {
            return "<+>";
        }


    }
    class DodgeToken : IToken
    {

        //Decreases chance to hit player by 50%. Lasts until player dodges. More tokens = more rounds. Max 2 stacks.

        public Damage? Damage { get; } = null;

        public static int Capacity { get; } = 2;


        public DodgeToken() { }

        public override string? ToString()
        {
            return "</>";
        }

    }

}
