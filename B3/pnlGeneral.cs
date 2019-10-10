using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B3
{
    public class pnlGeneral:Panel
    {

        static Panel pnlPicture;
        static Panel pnlDisplayResult;

        static Label lbTitle = new Label()
        {
            Text = "HỘP NÀO CÓ MÀU" + "\n" + "SẮC BẤT THƯỜNG ?",
            Location = new Point(460, 10),
            AutoSize = true,
            Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter
        };
        #region obj_pnlPicture
        public static PictureBox ptbDoctor = new PictureBox()
        {
            Size = new Size(461, 452),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\doctor.png")
        };
        #endregion

        

        public pnlGeneral()
        {
            LoadDisplay();
        }

        public void GameStart()
        {
            LbMistake_change.Text = "0";
            LbResult_change.Text = "0";
            LbTime.Text = "15";
        }

        void LoadDisplay()
        {
            this.Size = new Size(1000, 504);
            this.BackColor = Color.AntiqueWhite;

            pnlPicture = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(461, 504),
                BackColor = Color.AntiqueWhite
            };
            pnlDisplayResult = new Panel()
            {
                Location = new Point(461 + 335, 0),
                Size = new Size(200, 504),
                BackColor = Color.AntiqueWhite

            };

            pnlDisplayResult.Controls.Add(lbMistake);
            pnlDisplayResult.Controls.Add(lbMistake_change);
            pnlDisplayResult.Controls.Add(lbResult);
            pnlDisplayResult.Controls.Add(lbResult_change);
            pnlDisplayResult.Controls.Add(lbTime);
            pnlDisplayResult.Controls.Add(ptbVongTime);

            pnlPicture.Controls.Add(ptbDoctor);
            this.Controls.Add(pnlDisplayResult);
            this.Controls.Add(pnlPicture);
            this.Controls.Add(lbTitle);
        }

        #region obj_pnlDisplayResult

        public static PictureBox ptbVongTime = new PictureBox()
        {
            Location = new Point(50, 10),
            Size = new Size(100, 100),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Bitmap.FromFile(Application.StartupPath + @"\Picture\VongTime.png")
        };
        static Label lbTime = new Label()
        {
            Text = "15",
            Location = new Point(70, 30),
            Size = new Size(60, 60),
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold),
            ForeColor = Color.FromArgb(161, 224, 61),
        };
        static Label lbResult = new Label()
        {
            Location = new Point(0, 150),
            Text = "ĐIỂM",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold),
            Size = new Size(200, 25)
        };
        static Label lbResult_change = new Label()
        {
            Location = new Point(200 / 2 - 128 / 2, lbResult.Location.Y + lbResult.Height + 5),
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
            Size = new Size(200, 25)
        };
        static public Label lbMistake_change = new Label()
        {
            Location = new Point(200 / 2 - 128 / 2, lbMistake.Location.Y + lbMistake.Height + 5),
            Text = "0",
            Size = new Size(128, 40),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(61, 159, 231),
            Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
            BorderStyle = BorderStyle.FixedSingle
        };

        public Label LbTime
        {
            get { return lbTime; }
            set { lbTime = value; }
        }

        public Label LbResult_change
        {
            get { return lbResult_change; }
            set { lbResult_change = value; }
        }

        public Label LbMistake_change
        {
            get { return lbMistake_change; }
            set { lbResult_change = value; }
        }
        #endregion
    }
}
