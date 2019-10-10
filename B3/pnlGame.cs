using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B3
{
    class pnlGame:Panel
    {
        public delegate void ChangedLever(int lv);
        public event ChangedLever Onchangedlever;

        public delegate void ChangedMistake(int mt);
        public event ChangedMistake OnChangedMistake;

        Cons cons = new Cons();
        Square s;
        int size,sizeDV,CellonRow, mistake, lever;
        public int Lever
        {
            get { return lever; }
            set
            {
                if (lever != value)
                {
                    lever = value;
                    if (Onchangedlever != null)
                        Onchangedlever(value);
                }
            }
        }
        public int Mistake
        {
            get { return mistake; }
            set
            {
                if (mistake != value)
                {
                    mistake = value;
                    if (OnChangedMistake != null)
                        OnChangedMistake(value);
                }
            }
        }

        Pen p;
        string str;
        public pnlGame(int x,int y,int w)
        {
            this.Location = new Point(x, y);
            this.Size = new Size(w, w);
            this.BackColor = Color.Blue;
            this.MouseClick += PnlGame_MouseClick;
            this.Paint += PnlGame_Paint;
            size = w;
            s = new Square(Width / 2, Width / 2, Width / 3, Color.FromArgb(255, 255, 255));
            p = new Pen(s.Color,5);
            Mistake = 0;
            Lever = 1;
            s = LoadMap();
        }

        public void GameStart()
        {
            lever = 1;
            mistake = 0;
            s = LoadMap();
            this.Invalidate();
        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Random rand = new Random();
            int distance = ((-(CellonRow * CellonRow) - (15 * CellonRow) + 145)>=13)? (-(CellonRow * CellonRow) - (15 * CellonRow) + 145):(Lever > 50)?12:13;
            Color temp = Color.FromArgb(rand.Next(0, 255 - distance), rand.Next(0, 255 - distance), rand.Next(0, 255 - distance));
            g.Clear(temp);
            this.Size = new Size(CellonRow * sizeDV+1, CellonRow  * sizeDV+1);
            for (int i = 1; i <= CellonRow*CellonRow; i++)
            {
                g.DrawRectangle(p, new Rectangle(((i % CellonRow) + ((i % CellonRow == 0) ? CellonRow- 1 : -1)) * sizeDV, ((i / CellonRow) + ((i % CellonRow == 0) ? -1 : 0)) * sizeDV, sizeDV, sizeDV));
            }
            SolidBrush brush = new SolidBrush(Color.FromArgb(temp.R+distance,temp.G+distance,temp.B+distance));
            g.FillRectangle(brush, s.Rectangle());
            g.DrawRectangle(p, s.Rectangle());

            //g.DrawString(str, new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold), Brushes.Red, 10,10);
        }

        private void PnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if ((e.X >= s.X && e.Y >= s.Y) && (e.X <= s.X + s.Width && e.Y <= s.Y + s.Height)) 
            {
                Lever++;
                s = LoadMap();
                this.Invalidate();
            }
            else
            {
                Mistake++;
            }
            //s.Location(e.X, e.Y);
            
            str = string.Format("X={0},Y={1}", e.X, e.Y);
        }

        private Square LoadMap()
        {
            if (Lever < 30)
            {
                CellonRow = (Lever / 5) + 2;
            }
            else
            {
                CellonRow = 8;
            }
            sizeDV = cons.Weight_pnlGame / CellonRow;
            int rand = new Random().Next(1, CellonRow * CellonRow);
            return new Square(((rand % CellonRow) + ((rand % CellonRow == 0) ? CellonRow - 1 : -1)) * sizeDV, ((rand / CellonRow) + ((rand % CellonRow == 0) ? -1 : 0)) * sizeDV, sizeDV, Color.White);
        }


    }
}
