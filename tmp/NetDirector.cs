using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tmp
{
    public class NetDirector
    {
        private static NetDirector _nd;
        private  NetDirector()
        {
        }

        public static NetDirector GetObject()
        {
            if (_nd == null)
                _nd = new NetDirector();
            return _nd;
        }
    }
}
