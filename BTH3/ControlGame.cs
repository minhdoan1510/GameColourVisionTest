using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTH3
{
    public class ControlGame:EventArgs
    {
        private Color key;
        private Color color;
        private int lever;
        private int mistake;
        private Panel Map;
        public Color Key { set { key = value; } }
        public Color Color { set { color = value; } }

        #region Event
        public EventHandler OnChangedValue;
        public EventHandler OnPassLever;
        public EventHandler OnGameover;
        public EventHandler OnMistake;
        #endregion

        public int Lever
        {
            get { return lever; }
            set
            {
                if (value != Lever)
                {
                    lever = value;
                    if (OnChangedValue != null)
                    {
                        OnChangedValue(this, EventArgs.Empty);
                    }
                }
            }
        }
        public int Mistake
        {
            get { return mistake; }
            set
            {
                if (value != mistake)
                {
                    mistake = value;
                    if (OnChangedValue != null)
                    {
                        OnMistake(this, EventArgs.Empty);
                    }
                }
            }
        }
        public ControlGame(Panel _Map)
        {
            Lever = 1;
            Mistake = 0;
            Map = _Map;
        }
        public void Start()
        {
            Lever = 1;
            Mistake = 0;
            LoadGame();
        }
        public void CheckButton(Color _color)
        {
            if (_color == key)
            {
                if (OnPassLever != null)
                    OnPassLever(this, EventArgs.Empty);
                Lever++;
                LoadGame();
            }
            else
            {
                Mistake++;
                //if (Mistake == 3)
                //    GameOver();
            }
        }
        public void GameOver()
        {
            if(MessageBox.Show("Game over!!!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information)==DialogResult.OK)
            if (OnGameover != null)
            {
                    OnGameover(this, EventArgs.Empty);
            }
        }
        private void LoadGame()
        {
            Map.Controls.Clear();
            Map.Enabled = true;
            int n;
            if (1 == lever)
            {
                n = 2;
            }
            else if (2 <= lever && lever <= 4)
            {
                n = 3;
            }
            else if (5 <= lever && lever <= 8)
            {
                n = 4;
            }
            else if (9 <= lever && lever <= 15)
            {
                n = 5;
            }
            else
            {
                n = 6;
            }
            int sizeDV = Map.Width / n;
            Random random = new Random();
            int dfr = random.Next(1, n * n);
            color = Color.FromArgb(random.Next(0, 148), random.Next(0, 148), random.Next(0, 148));
            key = Color.FromArgb(color.R + (int)(-(n * n) - (15 * n) + 131), color.G + (int)(-(n * n) - (15 * n) + 131), color.B + (int)(-(n * n) - (15 * n) + 131));
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pnlButton button = new pnlButton(j * sizeDV, i * sizeDV, sizeDV, sizeDV, (dfr == (j + 1 + i * n)) ? key : color, this);
                    Map.Controls.Add(button);
                }
            }
        }
    }
}
