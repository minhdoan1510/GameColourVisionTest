using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B3
{
    public class fMain: Form
    {
        #region Propertion
        pnlGame game;
        pnlGeneral general;
        pnlBeginGame BeginGame;
        Cons cons = new Cons();
        static Panel pnlGameOver;
        int MaxScore;

        #region obj_control
        static int Thoigian = 0;
        static Timer timer = new Timer() { Interval = 100 };
        #endregion
        #region obj_Gameover
        static Label lbKQCB = new Label()
        {
            Text = "ĐIỂM CỦA BẠN LÀ: ",
            Location = new Point(1000 / 2 - 100, 504 / 2),
            Size = new Size(1000, 30),
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold),
            BackColor = Color.Transparent
        };
        static Label lbKQCN = new Label()
        {
            Text = "ĐIỂM CAO NHẤT LÀ: ",
            Location = new Point(1000 / 2 - 100, lbKQCB.Height + lbKQCB.Location.Y),
            Size = new Size(1000, 30),
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold)
        };
        static PictureBox ptbReload = new PictureBox()
        {
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\Reload.png"),
            Size = new Size(100, 100),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(1000 / 2 - 105, lbKQCN.Height + lbKQCN.Location.Y + 5),
        };
        static PictureBox ptbExit = new PictureBox()
        {
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\Exit.png"),
            Size = new Size(100, 100),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(1000 / 2 + 5, lbKQCN.Height + lbKQCN.Location.Y + 5),
        };
        static PictureBox ptbOver = new PictureBox()
        {
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\Over.png"),
            Size = new Size(504 / 2, 504 / 2),

            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(1000 / 2 - 504 / 4, 4),
        };
        #endregion
        #endregion
        #region Processing function
        public fMain()
        {
            LoadDisplay();
        }

        void Start()
        {
            Thoigian = 0;
            timer.Start();
            game.GameStart();
            general.GameStart();
            BeginGame.Visible = false;
            pnlGameOver.Visible = false;
        }
        void LoadDisplay()
        {
            //Setting this Pannel
            this.BackColor = Color.White;
            this.Size = new Size(1000, 504);
            StartPosition = FormStartPosition.CenterScreen;

            //Setup elements panel
            game = new pnlGame(461, 110, cons.Weight_pnlGame);
            pnlGameOver = new Panel()
            {
                Size = new Size(1000, 504),
                BackColor = Color.FromArgb(111, 193, 177),
                Visible = false
            };
            BeginGame = new pnlBeginGame();
            general = new pnlGeneral();
            LoadpnlGameOver();
            // Setup control
            MaxScore = 0;

            timer.Tick += Timer_Tick;

            BeginGame.OnClickExit += BeginGame_OnClickExit;
            BeginGame.OnClickInfor += BeginGame_OnClickInfor;
            BeginGame.OnClickNewGame += BeginGame_OnClickNewGame;

            game.Onchangedlever += Game_Onchangedlever;
            game.OnChangedMistake += Game_OnChangedMistake;

            Controls.Add(BeginGame);
            Controls.Add(pnlGameOver);
            Controls.Add(game);
            Controls.Add(general);   
            
        }
        #region Handle Event
        private void BeginGame_OnClickNewGame()
        {
            Start();
            

        }

        private void BeginGame_OnClickInfor()
        {
            MessageBox.Show("HAHA");
        }

        private void BeginGame_OnClickExit()
        {
            Close();
        }

        private void Game_OnChangedMistake(int mt)
        {
            general.LbMistake_change.Text = mt.ToString();
            Thoigian += 30;
        }

        private void Game_Onchangedlever(int lv)
        {
            general.LbResult_change.Text = (lv-1).ToString();
            Thoigian = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((float)(150 - Thoigian) / 10 + 1 <= 0.0)
            {
                general.LbTime.Text = "0.0";
                timer.Stop();
                GameOver();
            }
            else
            {
                Thoigian += 1;

                if ((15 - Thoigian / 10) >= 5)
                    general.LbTime.Text = (15 - Thoigian / 10).ToString();
                else
                {
                    if ((150 - Thoigian) % 10 != 0)
                        general.LbTime.Text = ((float)(150 - Thoigian) / 10 + 1).ToString();
                    else
                        general.LbTime.Text = ((float)(150 - Thoigian) / 10 + 1).ToString() + ".0";
                }
            }
        }

        private void PtbReload_Click(object sender, EventArgs e)
        {
            pnlGameOver.Visible = false;
            Thoigian = 0;
            Start();
            timer.Start();
        }
        private void PtbExit_Click(object sender, EventArgs e)
        {
            BeginGame.Visible = true;
        }
        #endregion
        private void GameOver()
        {
            timer.Stop();
            pnlGameOver.Visible = true;
            lbKQCB.Text = "ĐIỂM CỦA BẠN LÀ:" + general.LbResult_change.Text;
            if ((Convert.ToInt32(general.LbResult_change.Text) > MaxScore))
                MaxScore = Convert.ToInt32(general.LbResult_change.Text);
            lbKQCN.Text = "ĐIỂM CAO NHẤT LÀ: " + ((Convert.ToInt32(general.LbResult_change.Text) > MaxScore) ? general.LbResult_change.Text : MaxScore.ToString());
        }
        void LoadpnlGameOver()
        {
            ptbExit.Click += PtbExit_Click;
            ptbReload.Click += PtbReload_Click;
            pnlGameOver.Controls.Add(lbKQCB);
            pnlGameOver.Controls.Add(lbKQCN);
            pnlGameOver.Controls.Add(ptbReload);
            pnlGameOver.Controls.Add(ptbExit);
            pnlGameOver.Controls.Add(ptbOver);
        }
        #endregion
    }
}
