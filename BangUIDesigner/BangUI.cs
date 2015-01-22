using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace BangUIDesigner
{
    public class BangUI : GraphicDriver
    {
        private Size _uiSize;
        private System.Windows.Forms.Timer timer;
        private Thread newThread;

        Table tlb;

        public bool Enabled
        {
            get { return timer.Enabled; }
            set { timer.Enabled = value; }
        }

        public bool IsInitialised
        {
            get;
            set;
        }

        public BangUI()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 50;
            IsInitialised = false;
        }

        public override void Init(Graphics gr, Size winSize)
        {
            base.Init(gr, Screen.PrimaryScreen.Bounds.Size);
            tlb = new Table(_graphic);
            tlb.Init(winSize);
            timer.Start();
            IsInitialised = true;
        }

        public void ChangeSize(Size size)
        {
            if (tlb != null)
            {
                _uiSize = size;

                if (newThread != null && newThread.IsAlive)
                    newThread.Abort();

                newThread = new Thread(new ParameterizedThreadStart(ThreadProc));
                newThread.Start(size);
            }
        }

        private void ThreadProc(object size)
        {
            tlb.ChangeSize((Size)size);
            tlb.Reinit();
        }

        public void CreateTable(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<int> cardIDs = new List<int>();
            int ID;
            Roles role;

            tlb.Clear();
            xmlDoc.LoadXml(xml);
            foreach (XmlNode plNode in xmlDoc.DocumentElement.ChildNodes)
            {
                foreach (XmlNode crsNode in plNode.FirstChild.ChildNodes)
                {
                    ID = Convert.ToInt32(crsNode.Attributes["id"].Value);
                    cardIDs.Add(ID);
                }
                role = GetRoleFromStr(plNode.Attributes["role"].Value);
                tlb.AddPlayer(cardIDs.ToArray(),
                              plNode.Attributes["id"].Value,
                              role,
                              Convert.ToInt32(plNode.Attributes["lifes"].Value));
                cardIDs.Clear();
            }
            if(tlb.PlayerCount > 0)
                tlb.CalculatePlace();
            tlb.Draw();
        }

        public void DestroyTable()
        {
            tlb.Clear();
        }

        public ObjectData GetObjectData(Point p)
        {
            return tlb.GetObjectData(p);
        }

        public void DoAction(BangActions act, ObjectData data)
        {
            tlb.DoAction(act, data);
        }

        private new void Draw()
        {
            tlb.Draw();
            base.Draw();
        }

        public void Terminate()
        {
            tlb.Clear();
            timer.Stop();
        }

        private Roles GetRoleFromStr(string role)
        {
            Roles retRole = Roles.None;
            switch (role)
            {
                case "Sherif":
                    retRole = Roles.Sherif;
                    break;
                case "Deputy":
                    retRole = Roles.Deputy;
                    break;
                case "Renegade":
                    retRole = Roles.Renegade;
                    break;
                case "Outlaw":
                    retRole = Roles.Outlaw;
                    break;
            }
            return retRole;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
