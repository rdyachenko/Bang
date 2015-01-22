using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bang_
{
    public class Client
    {
        public String IP { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }

        public Client(string name, string ipAddr)
        {
            this.Name = name;
            this.IP = ipAddr;
        }

        public override bool Equals(object obj)
        {
            if (obj is Client)
            {
                Client cs = (Client)obj;
                if (cs.Name == Name & cs.IP == IP)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Name + IP).GetHashCode();
        }

    }
}
