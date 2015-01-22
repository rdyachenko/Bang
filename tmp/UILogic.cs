using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tmp
{
    public class UILogic: Panel
    {
        public event EventHandler MouseClick;

        public UI UI
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void UserClick(string ObjectID, string ObjectType)
        {
            throw new System.NotImplementedException();
        }
    }
}
