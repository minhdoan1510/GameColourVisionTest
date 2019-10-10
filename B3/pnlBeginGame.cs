using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B3
{
    class pnlBeginGame:Panel
    {
        #region properties
        Panel pnlLoadGame;
        Panel pnlMainGame;
        Timer timer;
        #endregion
        #region Processing function
        public pnlBeginGame()
        {
            Load();
            timer = new Timer() { Interval = 1};
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        #region obj_wait
        int IsBegin = 0;
        static Panel pnlFarmeLoading = new Panel()
        {
            Location = new Point(1000 / 2 - 1 - 250, 504 / 2),
            Size = new Size(502, 30),
            BackColor = Color.Aquamarine
        };
        Panel pnlLoadding = new Panel()
        {
            Location = new Point(1000 / 2 - 250, 1 + (504 / 2)),
            Size = new Size(0, 30 - 2),
            BackColor = Color.FromArgb(83, 83, 83)
        };
        #endregion
        #region Event
        public delegate void ClickExit();
        public event ClickExit OnClickExit;
        private void PtbExitMain_Click(object sender, EventArgs e)
        {
            OnClickExit();
        }

        public delegate void ClickInfor();
        public event ClickInfor OnClickInfor;
        private void PtbInfor_Click(object sender, EventArgs e)
        {
            OnClickInfor();
        }

        public delegate void ClickNewGame();
        public event ClickNewGame OnClickNewGame;
        private void PtbNewGame_Click(object sender, EventArgs e)
        {
            OnClickNewGame();
        }


        private void PnlSelectionMouseLeave(object sender, EventArgs e)
        {
            PictureBox obj = sender as PictureBox;
            obj.Location = new Point(obj.Location.X + 5, obj.Location.Y + 5);
            obj.Size = new Size(obj.Width - 10, obj.Height - 10);
        }
        private void PnlSelectionMouseHover(object sender, EventArgs e)
        {
            PictureBox obj = sender as PictureBox;
            obj.Location = new Point(obj.Location.X - 5, obj.Location.Y - 5);
            obj.Size = new Size(obj.Width + 10, obj.Height + 10);
        }

        #endregion

        #region LoadGame
        private void Load()
        {
            this.Size = new Size(1000, 504);

            pnlLoadGame = new Panel()
            {
                Size = this.Size,
                Visible = true,
                BackColor = Color.FromArgb(60, 60, 64),
            };

            pnlMainGame = new Panel()
            {
                Size = this.Size,
                Visible = false,
                BackColor = Color.White,
            };

            LoadpnlMainGame();
            LoadpnlLoadGame();
            this.Controls.Add(pnlMainGame);
            this.Controls.Add(pnlLoadGame);
        }
        private void LoadpnlLoadGame()
        {

            pnlLoadGame.Controls.Add(pnlLoadding);
            pnlLoadGame.Controls.Add(pnlFarmeLoading);
            //pnlLoadGame.Controls.Add(Button);
        }

        private void LoadpnlMainGame()
        {

            Panel pnlSelection = new Panel()
            {
                Location = new Point(1000 / 2 - 250, 504 / 2 - 100),
                Size = new Size(500, 504 / 2),
                BackColor = Color.Transparent,
            };
            PictureBox ptbNewGame = new PictureBox()
            {
                Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\PlayGame.png"),
                Location = new Point(50, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(100, 100),
                BackColor = Color.Transparent
            };
            PictureBox ptbInfor = new PictureBox()
            {
                Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\Infor.png"),
                Location = new Point(200, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(100, 100),
            }; PictureBox ptbExitMain = new PictureBox()
            {
                Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\ExitMain.png"),
                Location = new Point(350, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(100, 100),
            };
            pnlSelection.Controls.Add(ptbExitMain);
            pnlSelection.Controls.Add(ptbNewGame);
            pnlSelection.Controls.Add(ptbInfor);

            ptbExitMain.MouseEnter += PnlSelectionMouseHover;
            ptbInfor.MouseEnter += PnlSelectionMouseHover;
            ptbNewGame.MouseEnter += PnlSelectionMouseHover;

            ptbNewGame.MouseLeave += PnlSelectionMouseLeave;
            ptbExitMain.MouseLeave += PnlSelectionMouseLeave;
            ptbInfor.MouseLeave += PnlSelectionMouseLeave;

            ptbExitMain.Click += PtbExitMain_Click;
            ptbNewGame.Click += PtbNewGame_Click;
            ptbInfor.Click += PtbInfor_Click;

            pnlMainGame.Controls.Add(pnlSelection);
        }
        void LoadWaiting()
        {
            
            for (; pnlLoadding.Size.Width < 500;)
            {
                System.Threading.Thread.Sleep(100);
                int rand = new Random().Next(1, 10);
                if (rand == 1)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                pnlLoadding.Size = new Size((((pnlLoadding.Width + rand*5) >= 500) ? 500 : (pnlLoadding.Width + rand*5)), pnlLoadding.Height);
            }
            pnlLoadGame.Visible = false;
            pnlMainGame.Visible = true;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsBegin == 0)
            {
                IsBegin = 1;
                LoadWaiting();
            }
        }
        #endregion
        #endregion
    }
}
