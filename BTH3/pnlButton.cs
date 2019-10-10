using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH3
{
    public class pnlButton : Panel
    {
        private ControlGame control;
        public pnlButton(int _x, int _y, int _height,int _weight,Color _c, ControlGame control)
        {
            this.BackColor = _c;
            this.Location = new Point(_x, _y);
            this.Size = new Size(_weight, _height);
            this.Click += PnlButton_Click;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.ForeColor = Color.White;
            this.control = control;
        }
        public delegate void KT(Color _c);
        private void PnlButton_Click(object sender, EventArgs e)
        {
            Panel panelClicked = sender as Panel;
            KT check = new KT(control.CheckButton);
            check(panelClicked.BackColor);
        }
    }
}
