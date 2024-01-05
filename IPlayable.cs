using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightMasters
{
    public interface IPlayable
    {

        string Name { get; }

        Damage[]? DamageDealt { get; } //List of damage done by this Item, as a single playable item (either
                                       //minion or card) can do multiple types of damage with varying values.

        int Heal { get; } //How much the playable item heals a player for

        Dictionary<string, List<IToken>>? TokensAppliedCaster { get; }
        //Key = type of token applied as string, Value = tokens applied 
        Dictionary<string, List<IToken>>? TokensAppliedOpponent { get; }
        //Key = type of token applied as string, Value = tokens applied 

    }
}
