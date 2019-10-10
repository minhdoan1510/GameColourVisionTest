using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTH3
{
    public class fMain : Form
    {
        static public Cons cons = new Cons();
        static public Panel Map = new Panel() { Size = new Size(335, 335), Location = new Point(461, 110) };
        static public ControlGame controlGame = new ControlGame(Map);
        static public Panel ResultGame = new Panel()
        {
            Size = new Size(200, 400),
            Location = new Point(Map.Location.X + Map.Size.Width, Map.Location.Y)
        };
        public static PictureBox ptbDoctor = new PictureBox()
        {
            Size = new Size(461, 452),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\doctor.png")
        };
        public static PictureBox ptbVongTime = new PictureBox()
        {
            Location = new Point(1000 - 150, 10),
            Size = new Size(100, 100),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\VongTime.png")
        };
        static Label lbTime = new Label()
        {
            Text = "15",
            Location = new Point(1000 - 130, 30),
            Size = new Size(60, 60),
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold),
            ForeColor = Color.FromArgb(161, 224, 61),


        };
        static int MaxResult = 0;
        public static Panel PlayGame = new Panel()
        {
            Size = new Size(1000, 504),
            Location = new Point(0, 0),
            Visible = true
        };
        static public Panel pnlDisplayResult = new Panel()
        {
            Size = new Size(1000, 504),
            Location = new Point(0, 0),
            Visible = false,
            BackColor = Color.FromArgb(111, 193, 177)
        };
        public fMain()
        {
            this.Size = new Size(1000, 504);
            startGame();
            LoadScoreBoard();


            PlayGame.Controls.Add(Map);
            PlayGame.Controls.Add(ptbDoctor);
            PlayGame.Controls.Add(ResultGame);
            PlayGame.Controls.Add(lbTitle);
            PlayGame.Controls.Add(lbTime);
            PlayGame.Controls.Add(ptbVongTime);

            pnlDisplayResult.Controls.Add(lbKQCB);
            pnlDisplayResult.Controls.Add(lbKQCN);
            pnlDisplayResult.Controls.Add(ptbReload);
            pnlDisplayResult.Controls.Add(ptbExit);
            pnlDisplayResult.Controls.Add(ptbOver);


            Controls.Add(pnlDisplayResult);
            Controls.Add(PlayGame);

            controlGame.OnChangedValue += Handle_ResultChange;
            controlGame.OnGameover += Handle_Over;
            controlGame.OnPassLever += Handle_PassLever;
            controlGame.OnMistake += Handle_MistakeChange;
            //btnReload.Click += BtnReload_Click;
            //btnExit.Click += BtnExit_Click;
            ptbReload.Click += BtnReload_Click;
            ptbExit.Click += BtnExit_Click;
            tThoigian.Tick += TThoigian_Tick;
        }
        private void Handle_MistakeChange(object sender, EventArgs e)
        {
            lbMistake_change.Text = controlGame.Mistake.ToString();
            Thoigian += 30;
        }
        private void Handle_PassLever(object sender, EventArgs e)
        {
            Thoigian = 0;
            lbTime.Text = "15";
        }
        static int Thoigian = 0;
        private void TThoigian_Tick(object sender, EventArgs e)
        {
            if ((float)(150 - Thoigian) / 10 + 1 <= 0.0)
            {
                lbTime.Text = "0.0";
                tThoigian.Stop();
                controlGame.GameOver();
            }
            else
            {
                Thoigian += 1;

                if ((15 - Thoigian / 10) >= 5)
                    lbTime.Text = (15 - Thoigian / 10).ToString();
                else
                {
                    if ((150 - Thoigian) % 10 != 0)
                        lbTime.Text = ((float)(150 - Thoigian) / 10 + 1).ToString();
                    else
                        lbTime.Text = ((float)(150 - Thoigian) / 10 + 1).ToString() + ".0";
                }
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void BtnReload_Click(object sender, EventArgs e)
        {
            startGame();
        }
        private void startGame()
        {
            pnlDisplayResult.Visible = false;
            controlGame.Start();
            tThoigian.Start();
            lbTime.Text = "15";
            Thoigian = 0;
        }
        private void Handle_Over(object sender, EventArgs e)
        {
            Map.Enabled = false;
            tThoigian.Stop();
            if (Convert.ToInt32(lbResult_change.Text) > MaxResult)
                MaxResult = Convert.ToInt32(lbResult_change.Text);
            lbKQCB.Text = "ĐIỂM CỦA BẠN LÀ: " + lbResult_change.Text;
            lbKQCN.Text = "ĐIỂM CAO NHẤT LÀ: " + MaxResult;
            pnlDisplayResult.Visible = true; 
        }
        private void Handle_ResultChange(object sender, EventArgs e)
        {
            lbResult_change.Text = controlGame.Lever.ToString();
        }
        #region object in Controlgame
        static Label lbResult = new Label()
        {
            Location = new Point(0, 30),
            Text = "ĐIỂM: ",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold),
            Size = new Size(ResultGame.Width, 25)
        };
        static Label lbResult_change = new Label()
        {
            Location = new Point(ResultGame.Width / 2 - 128 / 2, lbResult.Location.Y + lbResult.Height + 5),
            Size = new Size(128, 40),
            Text = "0",
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(61, 159, 231),
            Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
            BorderStyle = BorderStyle.FixedSingle
        };
        static Label lbMistake = new Label()
        {
            Location = new Point(0, lbResult_change.Location.Y + lbResult_change.Height + 10),
            Text = "CÁC LỖI: ",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold),
            Size = new Size(ResultGame.Width, 25)
        };
        static Label lbMistake_change = new Label()
        {
            Location = new Point(ResultGame.Width / 2 - 128 / 2, lbMistake.Location.Y + lbMistake.Height + 5),
            Text = "0",
            Size = new Size(128, 40),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(61, 159, 231),
            Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
            BorderStyle = BorderStyle.FixedSingle
        };
        //static Button btnReload = new Button() {
        //    Text = "New Game",
        //    Location = new Point(lbMistake.Location.X, lbMistake.Size.Height + lbMistake.Location.Y),
        //    Size = new Size(50, 50)
        //};
        //static Button btnExit = new Button()
        //{
        //    Text = "Exit",
        //    Location = new Point(lbMistake.Location.X + 50, lbMistake.Size.Height + lbMistake.Location.Y),
        //    Size = new Size(50, 50)
        //};
        //static ProgressBar Thoigian = new ProgressBar() { Location = new Point(lbMistake.Location.X, btnExit.Size.Height + btnExit.Location.Y), Size = new Size(lbMistake.Location.X + 50 * 2, 20), Maximum = cons.pcbMax, Step = cons.pcbStep, Style = ProgressBarStyle.Continuous , };
        static Timer tThoigian = new Timer() { Interval = 100 };
        static Label lbTitle = new Label()
        {
            Text = "HỘP NÀO CÓ MÀU" + "\n" + "SẮC BẤT THƯỜNG ?",
            Location = new Point(460, 10),
            AutoSize = true,
            Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter
        };

        #endregion
        #region object in Result
        static Label lbKQCB = new Label()
        {
            Text = "ĐIỂM CỦA BẠN LÀ: ",
            Location = new Point(pnlDisplayResult.Width / 2 - 100, pnlDisplayResult.Height / 2),
            Size = new Size(pnlDisplayResult.Width, 30),
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold)
        };
        static Label lbKQCN = new Label()
        {
            Text = "ĐIỂM CAO NHẤT LÀ: ",
            Location = new Point(pnlDisplayResult.Width / 2 - 100, lbKQCB.Height + lbKQCB.Location.Y),
            Size = new Size(pnlDisplayResult.Width, 30),
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold)
        };
        static PictureBox ptbReload = new PictureBox()
        {
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\Reload.png"),
            Size = new Size(100, 100),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(pnlDisplayResult.Width / 2 - 105, lbKQCN.Height + lbKQCN.Location.Y + 5),
        };
        static PictureBox ptbExit = new PictureBox()
        {
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\Exit.png"),
            Size = new Size(100, 100),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(pnlDisplayResult.Width / 2 + 5, lbKQCN.Height + lbKQCN.Location.Y + 5),
        };
        static PictureBox ptbOver = new PictureBox()
        {
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\Over.png"),
            Size = new Size(pnlDisplayResult.Height / 2, pnlDisplayResult.Height / 2),

            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(pnlDisplayResult.Width / 2 - pnlDisplayResult.Height / 4, 4),
        };
        #endregion
        private void LoadScoreBoard()
        {
            ResultGame.Controls.Add(lbMistake);
            ResultGame.Controls.Add(lbMistake_change);
            ResultGame.Controls.Add(lbResult);
            ResultGame.Controls.Add(lbResult_change);
            //ResultGame.Controls.Add(btnReload);
            //ResultGame.Controls.Add(btnExit);

        }
    }
}