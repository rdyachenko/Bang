using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BangLogic
{
    public class Players: ArrayList
    {
        public delegate void TableFullHandler();

        public int OpponentsWithDistance(Player player, int distance)
        {
            int count = 0;
            foreach (Player p in base.ToArray())
            {
                if (CalculateDistance(player, p) <= distance)
                    count++;
            }
            return count;
        }

        public Player GetNext(Player player)
        {
            int index = base.IndexOf(player);
            if (index == (base.Count-1))
                return (Player)base[0];
            else
                return (Player)base[index+1];
        }

        public Player[] GetAccesibleOpponents(Player player)
        {
            List<Player> pls = new List<Player>();
            foreach (Player p in base.ToArray())
            {
                if (!p.Equals(player))
                {
                    if (CalculateDistance(player, p) <= player.CardsInTable.GunDistance)
                        pls.Add(p);
                }
            }
            return pls.ToArray();
        }
        
        public Player[] GetOpponentsWithoutJail(Player player)
        {
            List<Player> pls = new List<Player>();
            foreach (Player p in base.ToArray())
            {
                if (!p.Equals(player))
                {
                    if (!p.CardsInTable.JailExist)
                        pls.Add(p);
                }
            }
            return pls.ToArray();
        }

        public Player PlayerByName(string name)
        {
            if (name.IndexOf('_') > 0) name = name.Replace('_', ' ');
            foreach (Player p in base.ToArray())
                if (p.Name == name || p.Character.Name == name)
                    return p;
            return null;
        }

        public int CalculateDistance(Player PlayerFrom, Player PlayerTo)
        {
            int diff = Math.Abs(base.IndexOf(PlayerTo) - base.IndexOf(PlayerFrom));
            diff = diff > (base.Count / 2) ? base.Count - diff : diff;
            if (PlayerFrom.Character.Ability == eAbilities.SightToOpponents)
                diff--;
            if (PlayerFrom.CardsInTable.ScopeExist)
                diff--;
            if (PlayerTo.Character.Ability == eAbilities.IncreasyDistance)
                diff++;
            if (PlayerTo.CardsInTable.MustangExist)
                diff++;
            return diff < 1 ? 1 : diff; //can't be less then 1
        }

    }
}
