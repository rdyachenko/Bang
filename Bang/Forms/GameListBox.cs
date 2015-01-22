using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Bang_
{
    class GameListBox: ListBox
    {
        private ImageList _myImageList;
        public ImageList ImageList
        {
            get { return _myImageList; }
            set { _myImageList = value; }
        }
        public GameListBox()
        {
            // Set owner draw mode
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            Game item;
            Rectangle bounds = e.Bounds;
            Size imageSize;
            if (_myImageList!=null)
                imageSize = _myImageList.ImageSize;
            else
                imageSize = new Size(32,32);
            try
            {
                item = (Game)base.Items[e.Index];
                if (item.isStarted)
                    _myImageList.Draw(e.Graphics, bounds.Left , bounds.Top+1, 1);
                else
                    _myImageList.Draw(e.Graphics, bounds.Left , bounds.Top + 1, 0);
                if (item.localConnected)
                    _myImageList.Draw(e.Graphics, bounds.Left + imageSize.Width, bounds.Top+1, 2);
                else
                    _myImageList.Draw(e.Graphics, bounds.Left + imageSize.Width, bounds.Top+1, 3);


                e.Graphics.DrawString(item.ToString(), e.Font, new SolidBrush(e.ForeColor), bounds.Left + (imageSize.Width*2), bounds.Top);
            }
            catch
            {
                    e.Graphics.DrawString("GameListBox", e.Font, new SolidBrush(e.ForeColor),
                        bounds.Left, bounds.Top);
            }
            base.OnDrawItem(e);
        }
    }//End of GListBox class}
}