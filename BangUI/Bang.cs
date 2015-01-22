using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BangUIDesigner;
using System.Xml;

namespace BangUI
{
    public partial class frmBang : Form
    {
        BangUIDesigner.BangUI ui;
        Graphics _graphics;

        public frmBang()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            ui = new BangUIDesigner.BangUI();
            _graphics = Graphics.FromHwnd(Handle);
            ui.Init(_graphics, ClientSize);
        }

        void ui_CardMoveDone()
        {
            ObjectData od = ObjectData.Empty;
            Random rnd = new Random();
            od.DestinationObject.CardDetailes.CardID = rnd.Next(81);
            od.DestinationObject.PlayerDetailes.PlayerID = "Kit_Carlson";
            ui.DoAction(BangActions.AddCard, od);
        }

        private void frmBang_Load(object sender, EventArgs e)
        {
            //ui.CreateTable();D:\Work\BangUI\PlayersData.xml
            string file = @"..\..\..\Game.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(file);
            ui.CreateTable(xml.InnerXml);
        }

        bool cardSelect = false;
        ObjectDetails saveOD;
        PlayerDetailes plDet;
        CardDetailes crDet;

        private void frmBang_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ObjectData od = ui.GetObjectData(e.Location);

                switch (od.SourceObject.Type)
                {
                    case ObjectType.Table:
                        if (cardSelect)
                        {
                            od.DestinationObject = od.SourceObject;
                            od.DestinationObject.PlayerDetailes = plDet;
                            od.DestinationObject.CardDetailes = crDet;
                            ui.DoAction(BangActions.MoveToTable, od);
                            cardSelect = false;
                        }
                        break;
                    case ObjectType.Player:
                        if (cardSelect)
                        {
                            od.DestinationObject = od.SourceObject;
                            od.SourceObject = saveOD;
                            ui.DoAction(BangActions.MoveToPlayer, od);
                            cardSelect = false;
                        }
                        else
                        {
                            plDet = od.SourceObject.PlayerDetailes;
                        }
                        break;
                    case ObjectType.Card:
                        saveOD = od.SourceObject;
                        od.DestinationObject = od.SourceObject;
                        ui.DoAction(BangActions.SelectCard, od);
                        plDet = od.SourceObject.PlayerDetailes;
                        crDet = od.SourceObject.CardDetailes;
                        cardSelect = true;
                        break;
                    case ObjectType.Peal:
                        if (plDet != PlayerDetailes.Empty)
                        {
                            od.DestinationObject.PlayerDetailes = plDet;
                            od.DestinationObject.CardDetailes = crDet;
                            od.DestinationObject.CardDetailes.CardID = (new Random()).Next(0, 81);
                            ui.DoAction(BangActions.AddCard, od);
                        }
                        break;
                    case ObjectType.None:
                        break;
                    case ObjectType.Clear:
                        if (cardSelect)
                        {
                            od = ObjectData.Empty;
                            od.DestinationObject.PlayerDetailes = plDet;
                            ui.DoAction(BangActions.MoveToClear, od);
                        }
                        else
                            ui.DoAction(BangActions.TableCartToClear, ObjectData.Empty);
                        break;
                }
            }else if (e.Button == MouseButtons.Right)
            {
                ObjectData od = ui.GetObjectData(e.Location);

                switch (od.SourceObject.Type)
                {
                    case ObjectType.Table:
                        break;
                    case ObjectType.Player:
                        od.DestinationObject = od.SourceObject;
                        od.DestinationObject.PlayerDetailes.Tag = --od.SourceObject.PlayerDetailes.Tag;
                        ui.DoAction(BangActions.Lifes, od);
                        break;
                    case ObjectType.Card:
                    case ObjectType.None:
                        break;
                    case ObjectType.Clear:
                        od.DestinationObject = od.SourceObject;
                        if (od.SourceObject.PackState == PackState.Full)
                            od.DestinationObject.PackState = PackState.Empty;
                        else
                            od.DestinationObject.PackState = od.SourceObject.PackState++;
                        ui.DoAction(BangActions.SetPackState, od);
                        break;
                    case ObjectType.Peal:
                        od.DestinationObject = od.SourceObject;
                        if (od.SourceObject.PackState == PackState.Full)
                            od.DestinationObject.PackState = PackState.Empty;
                        else
                            od.DestinationObject.PackState = od.SourceObject.PackState++;
                        ui.DoAction(BangActions.SetPackState, od);
                        break;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                ObjectData od = ui.GetObjectData(e.Location);

                switch (od.SourceObject.Type)
                {
                    case ObjectType.Table:
                        break;
                    case ObjectType.Player:
                        od.DestinationObject = od.SourceObject;
                        od.DestinationObject.PlayerDetailes.Tag = ++od.SourceObject.PlayerDetailes.Tag;
                        ui.DoAction(BangActions.Lifes, od);
                        break;
                    case ObjectType.Card:
                    case ObjectType.None:
                        break;
                }
            }
        }

        private void frmBang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                ObjectData od = ui.GetObjectData(e.Location);

                switch (od.SourceObject.Type)
                {
                    case ObjectType.Table:
                        break;
                    case ObjectType.Player:
                        break;
                    case ObjectType.Card:
                        od.DestinationObject = od.SourceObject;
                        ui.DoAction(BangActions.MoveToMyTable, od);
                        break;
                    case ObjectType.None:
                        break;
                }
            }
        }

        private void frmBang_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ObjectData od = ui.GetObjectData(e.Location);

                switch (od.SourceObject.Type)
                {
                    case ObjectType.Table:
                        break;
                    case ObjectType.Player:
                        break;
                    case ObjectType.Card:
                        od.DestinationObject = od.SourceObject;
                        od.DestinationObject.Tag = 1;
                        ui.DoAction(BangActions.ShowCard, od);
                        break;
                    case ObjectType.None:
                        break;
                }
            }
        }

        private void frmBang_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ObjectData od = ui.GetObjectData(e.Location);

                if (e.Button == MouseButtons.Right)
                {
                    switch (od.SourceObject.Type)
                    {
                        case ObjectType.Table:
                            break;
                        case ObjectType.Player:
                            break;
                        case ObjectType.Card:
                            od.DestinationObject = od.SourceObject;
                            od.DestinationObject.Tag = 0;
                            ui.DoAction(BangActions.ShowCard, od);
                            break;
                        case ObjectType.None:
                            break;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ui.DestroyTable();
            frmBang_Load(sender, e);
        }

        ObjectDetails tmpOd = ObjectDetails.Empty;
        private void frmBang_MouseMove(object sender, MouseEventArgs e)
        {
            ObjectData od = ui.GetObjectData(e.Location);

            if (tmpOd != ObjectDetails.Empty)
            {
                od.DestinationObject = tmpOd;
                ui.DoAction(BangActions.HideLigth, od);
            }

            tmpOd = od.SourceObject;
            od.DestinationObject = tmpOd;
            ui.DoAction(BangActions.Ligth, od);
        }

        int mult = 100;
        private void frmBang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.LButton | Keys.Space))
            {
                ObjectData od = ObjectData.Empty;
                mult += 10;
                od.DestinationObject.Tag = mult;
                ui.DoAction(BangActions.Multiplicate, od);
            }
            if (e.KeyCode == (Keys.RButton | Keys.Space))
            {
                ObjectData od = ObjectData.Empty;
                mult -= 10;
                od.DestinationObject.Tag = mult;
                ui.DoAction(BangActions.Multiplicate, od);
            }
        }

        private void frmBang_Resize(object sender, EventArgs e)
        {
            ui.ChangeSize(this.ClientSize);
        }
    }
}