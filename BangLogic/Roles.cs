using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BangLogic
{
    class Roles: ArrayList
    {
        public Roles() : this(7) { }
        public Roles(int count)
        {
            switch (count)
            {
                case 1: 
                    // for test
                    base.Add(ePlayerRoles.Sherif);
                    break;
                case 3:
                    base.Add(ePlayerRoles.Deputy);
                    base.Add(ePlayerRoles.Renegade);
                    base.Add(ePlayerRoles.Outlaw);
                    break;
                case 4:
                    base.Add(ePlayerRoles.Sherif);
                    base.Add(ePlayerRoles.Renegade);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    break;
                case 5:
                    base.Add(ePlayerRoles.Sherif);
                    base.Add(ePlayerRoles.Deputy);
                    base.Add(ePlayerRoles.Renegade);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    break;
                case 6:
                    base.Add(ePlayerRoles.Sherif);
                    base.Add(ePlayerRoles.Deputy);
                    base.Add(ePlayerRoles.Renegade);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    break;
                case 7:
                    base.Add(ePlayerRoles.Sherif);
                    base.Add(ePlayerRoles.Deputy);
                    base.Add(ePlayerRoles.Deputy);
                    base.Add(ePlayerRoles.Renegade);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    break;
                case 8:
                    base.Add(ePlayerRoles.Sherif);
                    base.Add(ePlayerRoles.Deputy);
                    base.Add(ePlayerRoles.Deputy);
                    base.Add(ePlayerRoles.Renegade);
                    base.Add(ePlayerRoles.Renegade);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    base.Add(ePlayerRoles.Outlaw);
                    break;
                default:
                    throw new System.NotImplementedException("Bad players count!");
            }
        }

        public ePlayerRoles GetRandom()
        {
            ePlayerRoles ret = ePlayerRoles.None;
            Random r = new Random();
            ret = (ePlayerRoles)base[r.Next(base.Count)];
            base.Remove(ret);
            return ret;
        }
    }
}
